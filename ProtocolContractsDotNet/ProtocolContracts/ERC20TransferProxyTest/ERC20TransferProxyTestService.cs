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
using ProtocolContracts.Contracts.ERC20TransferProxyTest.ContractDefinition;

namespace ProtocolContracts.Contracts.ERC20TransferProxyTest
{
    public partial class ERC20TransferProxyTestService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ERC20TransferProxyTestDeployment eRC20TransferProxyTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ERC20TransferProxyTestDeployment>().SendRequestAndWaitForReceiptAsync(eRC20TransferProxyTestDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ERC20TransferProxyTestDeployment eRC20TransferProxyTestDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ERC20TransferProxyTestDeployment>().SendRequestAsync(eRC20TransferProxyTestDeployment);
        }

        public static async Task<ERC20TransferProxyTestService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ERC20TransferProxyTestDeployment eRC20TransferProxyTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, eRC20TransferProxyTestDeployment, cancellationTokenSource);
            return new ERC20TransferProxyTestService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ERC20TransferProxyTestService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public ERC20TransferProxyTestService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> Erc20safeTransferFromRequestAsync(Erc20safeTransferFromFunction erc20safeTransferFromFunction)
        {
             return ContractHandler.SendRequestAsync(erc20safeTransferFromFunction);
        }

        public Task<TransactionReceipt> Erc20safeTransferFromRequestAndWaitForReceiptAsync(Erc20safeTransferFromFunction erc20safeTransferFromFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(erc20safeTransferFromFunction, cancellationToken);
        }

        public Task<string> Erc20safeTransferFromRequestAsync(string token, string from, string to, BigInteger value)
        {
            var erc20safeTransferFromFunction = new Erc20safeTransferFromFunction();
                erc20safeTransferFromFunction.Token = token;
                erc20safeTransferFromFunction.From = from;
                erc20safeTransferFromFunction.To = to;
                erc20safeTransferFromFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(erc20safeTransferFromFunction);
        }

        public Task<TransactionReceipt> Erc20safeTransferFromRequestAndWaitForReceiptAsync(string token, string from, string to, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var erc20safeTransferFromFunction = new Erc20safeTransferFromFunction();
                erc20safeTransferFromFunction.Token = token;
                erc20safeTransferFromFunction.From = from;
                erc20safeTransferFromFunction.To = to;
                erc20safeTransferFromFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(erc20safeTransferFromFunction, cancellationToken);
        }
    }
}
