using Nethereum.XUnitEthereumClients;
using ProtocolContracts.Contracts.OrderValidatorTest.ContractDefinition;
using Nethereum.Hex.HexConvertors.Extensions;
using Xunit;
using Nethereum.ABI.EIP712;
using Microsoft.VisualBasic;
using Nethereum.Signer.EIP712;
using Nethereum.Signer;
using ProtocolContracts.Contracts.LibOrderTest;
using ProtocolContracts.Contracts.LibOrderTest.ContractDefinition;
using ProtocolContracts.Contracts.TransferProxyTest;
using ProtocolContracts.Contracts.TransferProxyTest.ContractDefinition;
using ProtocolContracts.Contracts.ERC20TransferProxyTest;
using ProtocolContracts.Contracts.ERC20TransferProxyTest.ContractDefinition;
using ProtocolContracts.Contracts.ExchangeSimpleV2;
using ProtocolContracts.Contracts.ExchangeSimpleV2.ContractDefinition;
using ProtocolContracts.Contracts.TestERC20;

namespace ProtocolContracts.Testing
{
    [Collection(EthereumClientIntegrationFixture.ETHEREUM_CLIENT_COLLECTION_DEFAULT)]
    public class ExchangeV2SimpleTest
    {
        private readonly EthereumClientIntegrationFixture _ethereumClientIntegrationFixture;
        public string ZERO_ADDRESS = "0x0000000000000000000000000000000000000000";

        public ExchangeV2SimpleTest(EthereumClientIntegrationFixture ethereumClientIntegrationFixture)
        {
            _ethereumClientIntegrationFixture = ethereumClientIntegrationFixture;
        }


        [Fact]
        public async void SimplestPossibleExchangeWorks()
        {

            var web3 = _ethereumClientIntegrationFixture.GetWeb3();

            var orderTest = await LibOrderTestService.DeployContractAndGetServiceAsync(web3, new LibOrderTestDeployment());
            var transferProxy = await TransferProxyTestService.DeployContractAndGetServiceAsync(web3, new TransferProxyTestDeployment());
            var erc20TransferProxy = await ERC20TransferProxyTestService.DeployContractAndGetServiceAsync(web3, new ERC20TransferProxyTestDeployment());
            var exchangeSimpleV2 = await ExchangeSimpleV2Service.DeployContractAndGetServiceAsync(web3, new ExchangeSimpleV2Deployment());

            await exchangeSimpleV2.Exchangesimplev2InitRequestAndWaitForReceiptAsync(transferProxy.ContractHandler.ContractAddress, erc20TransferProxy.ContractHandler.ContractAddress);
            var token1Service = await TestERC20Service.DeployContractAndGetServiceAsync(web3, new Contracts.TestERC20.ContractDefinition.TestERC20Deployment());
            var token2Service = await TestERC20Service.DeployContractAndGetServiceAsync(web3, new Contracts.TestERC20.ContractDefinition.TestERC20Deployment());

           

            var makerAddress = TestAccounts.Account1Address;
            var makerPrivateKey = TestAccounts.Account1PrivateKey;

            var takerAddress = TestAccounts.Account2Address;
            var takerPrivateKey = TestAccounts.Account2PrivateKey;
            
            var web3Maker = TestAccounts.GetAccount1Web3();
            var token1ServiceMaker = new TestERC20Service(web3Maker, token1Service.ContractHandler.ContractAddress);

            await token1Service.MintRequestAndWaitForReceiptAsync(makerAddress, 100);
            await token1ServiceMaker.ApproveRequestAndWaitForReceiptAsync(erc20TransferProxy.ContractHandler.ContractAddress, 10000000);

            var web3Taker = TestAccounts.GetAccount2Web3();
            var token2ServiceTaker = new TestERC20Service(web3Taker, token2Service.ContractHandler.ContractAddress);

            await token2Service.MintRequestAndWaitForReceiptAsync(takerAddress, 200);
            await token2ServiceTaker.ApproveRequestAndWaitForReceiptAsync(erc20TransferProxy.ContractHandler.ContractAddress, 10000000);


            var order1 = new Order
            {
                Maker = makerAddress,
                MakeAsset = new Asset()
                {
                    AssetType = new AssetType()
                    {
                        AssetClass = AssetTypeEncoder.ERC20.HexToByteArray(),
                        Data = AssetTypeEncoder.Encode(token1Service.ContractHandler.ContractAddress),
                    },
                    Value = 100
                },
                TakeAsset = new Asset()
                {
                    AssetType = new AssetType()
                    {
                        AssetClass = AssetTypeEncoder.ERC20.HexToByteArray(),
                        Data = AssetTypeEncoder.Encode(token2Service.ContractHandler.ContractAddress),
                    },
                    Value = 200,
                },
                Taker = ZERO_ADDRESS,
                Salt = 1,
                Start = 0,
                End = 0,
                Data = "0x".HexToByteArray(),
                DataType = "0xffffffff".HexToByteArray(),

            };

            var order2 = new Order
            {
                Maker = takerAddress,
                MakeAsset = new Asset()
                {
                    AssetType = new AssetType()
                    {
                        AssetClass = AssetTypeEncoder.ERC20.HexToByteArray(),
                        Data = AssetTypeEncoder.Encode(token2Service.ContractHandler.ContractAddress),
                    },
                    Value = 200,
                },
                TakeAsset = new Asset()
                {
                    AssetType = new AssetType()
                    {
                        AssetClass = AssetTypeEncoder.ERC20.HexToByteArray(),
                        Data = AssetTypeEncoder.Encode(token1Service.ContractHandler.ContractAddress),
                    },
                    Value = 100
                },
                Taker = ZERO_ADDRESS,
                Salt = 1,
                Start = 0,
                End = 0,
                Data = "0x".HexToByteArray(),
                DataType = "0xffffffff".HexToByteArray(),

            };

            var signer = new Eip712TypedDataSigner();
            var encoder = new Eip712TypedDataEncoder();
            var chainId = await web3.Eth.ChainId.SendRequestAsync();
            var typedData = GetOrderTypedDefinition(exchangeSimpleV2.ContractHandler.ContractAddress);
            typedData.Domain.ChainId = chainId;
            typedData.SetMessage(order1);
            var hashOrder1 = encoder.EncodeTypedData(order1, typedData);
            var signatureOrder1 = signer.SignTypedDataV4(order1, typedData, new EthECKey(makerPrivateKey));
            typedData.SetMessage(order2);
            var hashOrder2 = encoder.EncodeTypedData(order2, typedData);
            var signatureOrder2 = signer.SignTypedDataV4(order2, typedData, new EthECKey(takerPrivateKey));

            await exchangeSimpleV2.MatchOrdersRequestAndWaitForReceiptAsync(order1, signatureOrder1.HexToByteArray(), order2, signatureOrder2.HexToByteArray());

            var hashKeyChainOrder1 = await orderTest.HashKeyQueryAsync(order1);
            //This can be an internal function too.
            var hashKeyChainOrder2 = await orderTest.HashKeyQueryAsync(order2);
            

            Assert.Equal(200, await exchangeSimpleV2.FillsQueryAsync(hashKeyChainOrder1));
            Assert.Equal(100, await exchangeSimpleV2.FillsQueryAsync(hashKeyChainOrder2));

            Assert.Equal(0, await token1Service.BalanceOfQueryAsync(makerAddress));
            Assert.Equal(100, await token1Service.BalanceOfQueryAsync(takerAddress));
            Assert.Equal(200, await token2Service.BalanceOfQueryAsync(makerAddress));
            Assert.Equal(0, await token2Service.BalanceOfQueryAsync(takerAddress));
        }


        public static TypedData<Domain> GetOrderTypedDefinition(string verifyingContract)
        {
            return new TypedData<Domain>
            {
                Domain = new Domain
                {
                    Name = "Exchange",
                    Version = "2",
                    ChainId = 1,
                    VerifyingContract = verifyingContract
                },
                Types = MemberDescriptionFactory.GetTypesMemberDescription(typeof(Domain), typeof(Order), typeof(Asset), typeof(AssetType)),
                PrimaryType = "Order",
            };
        }

    }



    }

    
