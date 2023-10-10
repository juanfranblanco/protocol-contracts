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

namespace ProtocolContracts.Contracts.TransferProxyTest.ContractDefinition
{


    public partial class TransferProxyTestDeployment : TransferProxyTestDeploymentBase
    {
        public TransferProxyTestDeployment() : base(BYTECODE) { }
        public TransferProxyTestDeployment(string byteCode) : base(byteCode) { }
    }

    public class TransferProxyTestDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50610297806100206000396000f3fe608060405234801561001057600080fd5b50600436106100365760003560e01c80639c1c2ee91461003b578063f709b906146100da575b600080fd5b6100d8600480360360c081101561005157600080fd5b6001600160a01b038235811692602081013582169260408201359092169160608201359160808101359181019060c0810160a082013564010000000081111561009957600080fd5b8201836020820111156100ab57600080fd5b803590602001918460018302840111640100000000831117156100cd57600080fd5b509092509050610116565b005b6100d8600480360360808110156100f057600080fd5b506001600160a01b038135811691602081013582169160408201351690606001356101db565b866001600160a01b031663f242432a8787878787876040518763ffffffff1660e01b815260040180876001600160a01b03168152602001866001600160a01b03168152602001858152602001848152602001806020018281038252848482818152602001925080828437600081840152601f19601f820116905080830192505050975050505050505050600060405180830381600087803b1580156101ba57600080fd5b505af11580156101ce573d6000803e3d6000fd5b5050505050505050505050565b836001600160a01b03166342842e0e8484846040518463ffffffff1660e01b815260040180846001600160a01b03168152602001836001600160a01b031681526020018281526020019350505050600060405180830381600087803b15801561024357600080fd5b505af1158015610257573d6000803e3d6000fd5b505050505050505056fea2646970667358221220de906c97c269d1e10d70659212a6028517c2c2891a5e2784e24a567679ea4f2364736f6c63430007060033";
        public TransferProxyTestDeploymentBase() : base(BYTECODE) { }
        public TransferProxyTestDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class Erc1155safeTransferFromFunction : Erc1155safeTransferFromFunctionBase { }

    [Function("erc1155safeTransferFrom")]
    public class Erc1155safeTransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "token", 1)]
        public virtual string Token { get; set; }
        [Parameter("address", "from", 2)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 3)]
        public virtual string To { get; set; }
        [Parameter("uint256", "id", 4)]
        public virtual BigInteger Id { get; set; }
        [Parameter("uint256", "value", 5)]
        public virtual BigInteger Value { get; set; }
        [Parameter("bytes", "data", 6)]
        public virtual byte[] Data { get; set; }
    }

    public partial class Erc721safeTransferFromFunction : Erc721safeTransferFromFunctionBase { }

    [Function("erc721safeTransferFrom")]
    public class Erc721safeTransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "token", 1)]
        public virtual string Token { get; set; }
        [Parameter("address", "from", 2)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 3)]
        public virtual string To { get; set; }
        [Parameter("uint256", "tokenId", 4)]
        public virtual BigInteger TokenId { get; set; }
    }




}
