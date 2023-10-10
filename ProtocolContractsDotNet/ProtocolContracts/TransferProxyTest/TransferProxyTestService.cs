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
using ProtocolContracts.Contracts.TransferProxyTest.ContractDefinition;

namespace ProtocolContracts.Contracts.TransferProxyTest
{
    public partial class TransferProxyTestService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, TransferProxyTestDeployment transferProxyTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<TransferProxyTestDeployment>().SendRequestAndWaitForReceiptAsync(transferProxyTestDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, TransferProxyTestDeployment transferProxyTestDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<TransferProxyTestDeployment>().SendRequestAsync(transferProxyTestDeployment);
        }

        public static async Task<TransferProxyTestService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, TransferProxyTestDeployment transferProxyTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, transferProxyTestDeployment, cancellationTokenSource);
            return new TransferProxyTestService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public TransferProxyTestService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public TransferProxyTestService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> Erc1155safeTransferFromRequestAsync(Erc1155safeTransferFromFunction erc1155safeTransferFromFunction)
        {
             return ContractHandler.SendRequestAsync(erc1155safeTransferFromFunction);
        }

        public Task<TransactionReceipt> Erc1155safeTransferFromRequestAndWaitForReceiptAsync(Erc1155safeTransferFromFunction erc1155safeTransferFromFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(erc1155safeTransferFromFunction, cancellationToken);
        }

        public Task<string> Erc1155safeTransferFromRequestAsync(string token, string from, string to, BigInteger id, BigInteger value, byte[] data)
        {
            var erc1155safeTransferFromFunction = new Erc1155safeTransferFromFunction();
                erc1155safeTransferFromFunction.Token = token;
                erc1155safeTransferFromFunction.From = from;
                erc1155safeTransferFromFunction.To = to;
                erc1155safeTransferFromFunction.Id = id;
                erc1155safeTransferFromFunction.Value = value;
                erc1155safeTransferFromFunction.Data = data;
            
             return ContractHandler.SendRequestAsync(erc1155safeTransferFromFunction);
        }

        public Task<TransactionReceipt> Erc1155safeTransferFromRequestAndWaitForReceiptAsync(string token, string from, string to, BigInteger id, BigInteger value, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var erc1155safeTransferFromFunction = new Erc1155safeTransferFromFunction();
                erc1155safeTransferFromFunction.Token = token;
                erc1155safeTransferFromFunction.From = from;
                erc1155safeTransferFromFunction.To = to;
                erc1155safeTransferFromFunction.Id = id;
                erc1155safeTransferFromFunction.Value = value;
                erc1155safeTransferFromFunction.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(erc1155safeTransferFromFunction, cancellationToken);
        }

        public Task<string> Erc721safeTransferFromRequestAsync(Erc721safeTransferFromFunction erc721safeTransferFromFunction)
        {
             return ContractHandler.SendRequestAsync(erc721safeTransferFromFunction);
        }

        public Task<TransactionReceipt> Erc721safeTransferFromRequestAndWaitForReceiptAsync(Erc721safeTransferFromFunction erc721safeTransferFromFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(erc721safeTransferFromFunction, cancellationToken);
        }

        public Task<string> Erc721safeTransferFromRequestAsync(string token, string from, string to, BigInteger tokenId)
        {
            var erc721safeTransferFromFunction = new Erc721safeTransferFromFunction();
                erc721safeTransferFromFunction.Token = token;
                erc721safeTransferFromFunction.From = from;
                erc721safeTransferFromFunction.To = to;
                erc721safeTransferFromFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAsync(erc721safeTransferFromFunction);
        }

        public Task<TransactionReceipt> Erc721safeTransferFromRequestAndWaitForReceiptAsync(string token, string from, string to, BigInteger tokenId, CancellationTokenSource cancellationToken = null)
        {
            var erc721safeTransferFromFunction = new Erc721safeTransferFromFunction();
                erc721safeTransferFromFunction.Token = token;
                erc721safeTransferFromFunction.From = from;
                erc721safeTransferFromFunction.To = to;
                erc721safeTransferFromFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(erc721safeTransferFromFunction, cancellationToken);
        }
    }
}
