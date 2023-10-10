using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace ProtocolContracts.Contracts.ERC20TransferProxyTest.ContractDefinition
{


    public partial class ERC20TransferProxyTestDeployment : ERC20TransferProxyTestDeploymentBase
    {
        public ERC20TransferProxyTestDeployment() : base(BYTECODE) { }
        public ERC20TransferProxyTestDeployment(string byteCode) : base(byteCode) { }
    }

    public class ERC20TransferProxyTestDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b5061018f806100206000396000f3fe608060405234801561001057600080fd5b506004361061002b5760003560e01c8063776062c314610030575b600080fd5b61006c6004803603608081101561004657600080fd5b506001600160a01b0381358116916020810135821691604082013516906060013561006e565b005b836001600160a01b03166323b872dd8484846040518463ffffffff1660e01b815260040180846001600160a01b03168152602001836001600160a01b031681526020018281526020019350505050602060405180830381600087803b1580156100d657600080fd5b505af11580156100ea573d6000803e3d6000fd5b505050506040513d602081101561010057600080fd5b5051610153576040805162461bcd60e51b815260206004820152601a60248201527f6661696c757265207768696c65207472616e7366657272696e67000000000000604482015290519081900360640190fd5b5050505056fea2646970667358221220c7421f1df2530b2b81cb41df821465e2c659451fe80a406ae5eb6e79306ea83564736f6c63430007060033";
        public ERC20TransferProxyTestDeploymentBase() : base(BYTECODE) { }
        public ERC20TransferProxyTestDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class Erc20safeTransferFromFunction : Erc20safeTransferFromFunctionBase { }

    [Function("erc20safeTransferFrom")]
    public class Erc20safeTransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "token", 1)]
        public virtual string Token { get; set; }
        [Parameter("address", "from", 2)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 3)]
        public virtual string To { get; set; }
        [Parameter("uint256", "value", 4)]
        public virtual BigInteger Value { get; set; }
    }


}
