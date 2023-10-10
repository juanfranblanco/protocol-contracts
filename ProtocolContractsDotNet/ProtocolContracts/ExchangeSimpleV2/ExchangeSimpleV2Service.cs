using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using ProtocolContracts.Contracts.ExchangeSimpleV2.ContractDefinition;
using ProtocolContracts.Contracts.OrderValidatorTest.ContractDefinition;

namespace ProtocolContracts.Contracts.ExchangeSimpleV2
{
    public partial class ExchangeSimpleV2Service
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ExchangeSimpleV2Deployment exchangeSimpleV2Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ExchangeSimpleV2Deployment>().SendRequestAndWaitForReceiptAsync(exchangeSimpleV2Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ExchangeSimpleV2Deployment exchangeSimpleV2Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ExchangeSimpleV2Deployment>().SendRequestAsync(exchangeSimpleV2Deployment);
        }

        public static async Task<ExchangeSimpleV2Service> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ExchangeSimpleV2Deployment exchangeSimpleV2Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, exchangeSimpleV2Deployment, cancellationTokenSource);
            return new ExchangeSimpleV2Service(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ExchangeSimpleV2Service(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public ExchangeSimpleV2Service(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> Exchangesimplev2InitRequestAsync(Exchangesimplev2InitFunction exchangesimplev2InitFunction)
        {
             return ContractHandler.SendRequestAsync(exchangesimplev2InitFunction);
        }

        public Task<TransactionReceipt> Exchangesimplev2InitRequestAndWaitForReceiptAsync(Exchangesimplev2InitFunction exchangesimplev2InitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(exchangesimplev2InitFunction, cancellationToken);
        }

        public Task<string> Exchangesimplev2InitRequestAsync(string transferProxy, string erc20TransferProxy)
        {
            var exchangesimplev2InitFunction = new Exchangesimplev2InitFunction();
                exchangesimplev2InitFunction.TransferProxy = transferProxy;
                exchangesimplev2InitFunction.Erc20TransferProxy = erc20TransferProxy;
            
             return ContractHandler.SendRequestAsync(exchangesimplev2InitFunction);
        }

        public Task<TransactionReceipt> Exchangesimplev2InitRequestAndWaitForReceiptAsync(string transferProxy, string erc20TransferProxy, CancellationTokenSource cancellationToken = null)
        {
            var exchangesimplev2InitFunction = new Exchangesimplev2InitFunction();
                exchangesimplev2InitFunction.TransferProxy = transferProxy;
                exchangesimplev2InitFunction.Erc20TransferProxy = erc20TransferProxy;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(exchangesimplev2InitFunction, cancellationToken);
        }

        public Task<string> CancelRequestAsync(CancelFunction cancelFunction)
        {
             return ContractHandler.SendRequestAsync(cancelFunction);
        }

        public Task<TransactionReceipt> CancelRequestAndWaitForReceiptAsync(CancelFunction cancelFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelFunction, cancellationToken);
        }

        public Task<string> CancelRequestAsync(Order order)
        {
            var cancelFunction = new CancelFunction();
                cancelFunction.Order = order;
            
             return ContractHandler.SendRequestAsync(cancelFunction);
        }

        public Task<TransactionReceipt> CancelRequestAndWaitForReceiptAsync(Order order, CancellationTokenSource cancellationToken = null)
        {
            var cancelFunction = new CancelFunction();
                cancelFunction.Order = order;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelFunction, cancellationToken);
        }

        public Task<string> DirectAcceptBidRequestAsync(DirectAcceptBidFunction directAcceptBidFunction)
        {
             return ContractHandler.SendRequestAsync(directAcceptBidFunction);
        }

        public Task<TransactionReceipt> DirectAcceptBidRequestAndWaitForReceiptAsync(DirectAcceptBidFunction directAcceptBidFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(directAcceptBidFunction, cancellationToken);
        }

        public Task<string> DirectAcceptBidRequestAsync(AcceptBid direct)
        {
            var directAcceptBidFunction = new DirectAcceptBidFunction();
                directAcceptBidFunction.Direct = direct;
            
             return ContractHandler.SendRequestAsync(directAcceptBidFunction);
        }

        public Task<TransactionReceipt> DirectAcceptBidRequestAndWaitForReceiptAsync(AcceptBid direct, CancellationTokenSource cancellationToken = null)
        {
            var directAcceptBidFunction = new DirectAcceptBidFunction();
                directAcceptBidFunction.Direct = direct;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(directAcceptBidFunction, cancellationToken);
        }

        public Task<string> DirectPurchaseRequestAsync(DirectPurchaseFunction directPurchaseFunction)
        {
             return ContractHandler.SendRequestAsync(directPurchaseFunction);
        }

        public Task<TransactionReceipt> DirectPurchaseRequestAndWaitForReceiptAsync(DirectPurchaseFunction directPurchaseFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(directPurchaseFunction, cancellationToken);
        }

        public Task<string> DirectPurchaseRequestAsync(Purchase direct)
        {
            var directPurchaseFunction = new DirectPurchaseFunction();
                directPurchaseFunction.Direct = direct;
            
             return ContractHandler.SendRequestAsync(directPurchaseFunction);
        }

        public Task<TransactionReceipt> DirectPurchaseRequestAndWaitForReceiptAsync(Purchase direct, CancellationTokenSource cancellationToken = null)
        {
            var directPurchaseFunction = new DirectPurchaseFunction();
                directPurchaseFunction.Direct = direct;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(directPurchaseFunction, cancellationToken);
        }

        public Task<BigInteger> FillsQueryAsync(FillsFunction fillsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FillsFunction, BigInteger>(fillsFunction, blockParameter);
        }

        
        public Task<BigInteger> FillsQueryAsync(byte[] returnValue1, BlockParameter blockParameter = null)
        {
            var fillsFunction = new FillsFunction();
                fillsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<FillsFunction, BigInteger>(fillsFunction, blockParameter);
        }

        public Task<string> MatchOrdersRequestAsync(MatchOrdersFunction matchOrdersFunction)
        {
             return ContractHandler.SendRequestAsync(matchOrdersFunction);
        }

        public Task<TransactionReceipt> MatchOrdersRequestAndWaitForReceiptAsync(MatchOrdersFunction matchOrdersFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(matchOrdersFunction, cancellationToken);
        }

        public Task<string> MatchOrdersRequestAsync(Order orderLeft, byte[] signatureLeft, Order orderRight, byte[] signatureRight)
        {
            var matchOrdersFunction = new MatchOrdersFunction();
                matchOrdersFunction.OrderLeft = orderLeft;
                matchOrdersFunction.SignatureLeft = signatureLeft;
                matchOrdersFunction.OrderRight = orderRight;
                matchOrdersFunction.SignatureRight = signatureRight;
            
             return ContractHandler.SendRequestAsync(matchOrdersFunction);
        }

        public Task<TransactionReceipt> MatchOrdersRequestAndWaitForReceiptAsync(Order orderLeft, byte[] signatureLeft, Order orderRight, byte[] signatureRight, CancellationTokenSource cancellationToken = null)
        {
            var matchOrdersFunction = new MatchOrdersFunction();
                matchOrdersFunction.OrderLeft = orderLeft;
                matchOrdersFunction.SignatureLeft = signatureLeft;
                matchOrdersFunction.OrderRight = orderRight;
                matchOrdersFunction.SignatureRight = signatureRight;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(matchOrdersFunction, cancellationToken);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(renounceOwnershipFunction);
        }

        public Task<string> RenounceOwnershipRequestAsync()
        {
             return ContractHandler.SendRequestAsync<RenounceOwnershipFunction>();
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceOwnershipFunction, cancellationToken);
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<RenounceOwnershipFunction>(null, cancellationToken);
        }

        public Task<string> SetAssetMatcherRequestAsync(SetAssetMatcherFunction setAssetMatcherFunction)
        {
             return ContractHandler.SendRequestAsync(setAssetMatcherFunction);
        }

        public Task<TransactionReceipt> SetAssetMatcherRequestAndWaitForReceiptAsync(SetAssetMatcherFunction setAssetMatcherFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAssetMatcherFunction, cancellationToken);
        }

        public Task<string> SetAssetMatcherRequestAsync(byte[] assetType, string matcher)
        {
            var setAssetMatcherFunction = new SetAssetMatcherFunction();
                setAssetMatcherFunction.AssetType = assetType;
                setAssetMatcherFunction.Matcher = matcher;
            
             return ContractHandler.SendRequestAsync(setAssetMatcherFunction);
        }

        public Task<TransactionReceipt> SetAssetMatcherRequestAndWaitForReceiptAsync(byte[] assetType, string matcher, CancellationTokenSource cancellationToken = null)
        {
            var setAssetMatcherFunction = new SetAssetMatcherFunction();
                setAssetMatcherFunction.AssetType = assetType;
                setAssetMatcherFunction.Matcher = matcher;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAssetMatcherFunction, cancellationToken);
        }

        public Task<string> SetTransferProxyRequestAsync(SetTransferProxyFunction setTransferProxyFunction)
        {
             return ContractHandler.SendRequestAsync(setTransferProxyFunction);
        }

        public Task<TransactionReceipt> SetTransferProxyRequestAndWaitForReceiptAsync(SetTransferProxyFunction setTransferProxyFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTransferProxyFunction, cancellationToken);
        }

        public Task<string> SetTransferProxyRequestAsync(byte[] assetType, string proxy)
        {
            var setTransferProxyFunction = new SetTransferProxyFunction();
                setTransferProxyFunction.AssetType = assetType;
                setTransferProxyFunction.Proxy = proxy;
            
             return ContractHandler.SendRequestAsync(setTransferProxyFunction);
        }

        public Task<TransactionReceipt> SetTransferProxyRequestAndWaitForReceiptAsync(byte[] assetType, string proxy, CancellationTokenSource cancellationToken = null)
        {
            var setTransferProxyFunction = new SetTransferProxyFunction();
                setTransferProxyFunction.AssetType = assetType;
                setTransferProxyFunction.Proxy = proxy;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTransferProxyFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(string newOwner)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }
    }
}
