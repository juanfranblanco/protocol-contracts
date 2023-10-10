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
using ProtocolContracts.Contracts.LibOrderTest.ContractDefinition;
using ProtocolContracts.Contracts.OrderValidatorTest.ContractDefinition;

namespace ProtocolContracts.Contracts.LibOrderTest
{
    public partial class LibOrderTestService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, LibOrderTestDeployment libOrderTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<LibOrderTestDeployment>().SendRequestAndWaitForReceiptAsync(libOrderTestDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, LibOrderTestDeployment libOrderTestDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<LibOrderTestDeployment>().SendRequestAsync(libOrderTestDeployment);
        }

        public static async Task<LibOrderTestService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, LibOrderTestDeployment libOrderTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, libOrderTestDeployment, cancellationTokenSource);
            return new LibOrderTestService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public LibOrderTestService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public LibOrderTestService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<CalculateRemainingOutputDTO> CalculateRemainingQueryAsync(CalculateRemainingFunction calculateRemainingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<CalculateRemainingFunction, CalculateRemainingOutputDTO>(calculateRemainingFunction, blockParameter);
        }

        public Task<CalculateRemainingOutputDTO> CalculateRemainingQueryAsync(Order order, BigInteger fill, bool isMakeFill, BlockParameter blockParameter = null)
        {
            var calculateRemainingFunction = new CalculateRemainingFunction();
                calculateRemainingFunction.Order = order;
                calculateRemainingFunction.Fill = fill;
                calculateRemainingFunction.IsMakeFill = isMakeFill;
            
            return ContractHandler.QueryDeserializingToObjectAsync<CalculateRemainingFunction, CalculateRemainingOutputDTO>(calculateRemainingFunction, blockParameter);
        }

        public Task<byte[]> HashQueryAsync(HashFunction hashFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HashFunction, byte[]>(hashFunction, blockParameter);
        }

        
        public Task<byte[]> HashQueryAsync(Order order, BlockParameter blockParameter = null)
        {
            var hashFunction = new HashFunction();
                hashFunction.Order = order;
            
            return ContractHandler.QueryAsync<HashFunction, byte[]>(hashFunction, blockParameter);
        }

        public Task<byte[]> HashKeyQueryAsync(HashKeyFunction hashKeyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HashKeyFunction, byte[]>(hashKeyFunction, blockParameter);
        }

        
        public Task<byte[]> HashKeyQueryAsync(Order order, BlockParameter blockParameter = null)
        {
            var hashKeyFunction = new HashKeyFunction();
                hashKeyFunction.Order = order;
            
            return ContractHandler.QueryAsync<HashKeyFunction, byte[]>(hashKeyFunction, blockParameter);
        }

        public Task<byte[]> HashKeyOnChainQueryAsync(HashKeyOnChainFunction hashKeyOnChainFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HashKeyOnChainFunction, byte[]>(hashKeyOnChainFunction, blockParameter);
        }

        
        public Task<byte[]> HashKeyOnChainQueryAsync(Order order, BlockParameter blockParameter = null)
        {
            var hashKeyOnChainFunction = new HashKeyOnChainFunction();
                hashKeyOnChainFunction.Order = order;
            
            return ContractHandler.QueryAsync<HashKeyOnChainFunction, byte[]>(hashKeyOnChainFunction, blockParameter);
        }

        public Task<byte[]> HashV1QueryAsync(HashV1Function hashV1Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HashV1Function, byte[]>(hashV1Function, blockParameter);
        }

        
        public Task<byte[]> HashV1QueryAsync(string maker, Asset makeAsset, Asset takeAsset, BigInteger salt, BlockParameter blockParameter = null)
        {
            var hashV1Function = new HashV1Function();
                hashV1Function.Maker = maker;
                hashV1Function.MakeAsset = makeAsset;
                hashV1Function.TakeAsset = takeAsset;
                hashV1Function.Salt = salt;
            
            return ContractHandler.QueryAsync<HashV1Function, byte[]>(hashV1Function, blockParameter);
        }

        public Task<byte[]> HashV2QueryAsync(HashV2Function hashV2Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HashV2Function, byte[]>(hashV2Function, blockParameter);
        }

        
        public Task<byte[]> HashV2QueryAsync(string maker, Asset makeAsset, Asset takeAsset, BigInteger salt, byte[] data, BlockParameter blockParameter = null)
        {
            var hashV2Function = new HashV2Function();
                hashV2Function.Maker = maker;
                hashV2Function.MakeAsset = makeAsset;
                hashV2Function.TakeAsset = takeAsset;
                hashV2Function.Salt = salt;
                hashV2Function.Data = data;
            
            return ContractHandler.QueryAsync<HashV2Function, byte[]>(hashV2Function, blockParameter);
        }
    }
}
