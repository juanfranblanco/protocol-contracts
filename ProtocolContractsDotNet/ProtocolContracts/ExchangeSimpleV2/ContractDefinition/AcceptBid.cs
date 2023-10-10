using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ProtocolContracts.Contracts.ExchangeSimpleV2.ContractDefinition
{
    public partial class AcceptBid : AcceptBidBase { }

    public class AcceptBidBase 
    {
        [Parameter("address", "bidMaker", 1)]
        public virtual string BidMaker { get; set; }
        [Parameter("uint256", "bidNftAmount", 2)]
        public virtual BigInteger BidNftAmount { get; set; }
        [Parameter("bytes4", "nftAssetClass", 3)]
        public virtual byte[] NftAssetClass { get; set; }
        [Parameter("bytes", "nftData", 4)]
        public virtual byte[] NftData { get; set; }
        [Parameter("uint256", "bidPaymentAmount", 5)]
        public virtual BigInteger BidPaymentAmount { get; set; }
        [Parameter("address", "paymentToken", 6)]
        public virtual string PaymentToken { get; set; }
        [Parameter("uint256", "bidSalt", 7)]
        public virtual BigInteger BidSalt { get; set; }
        [Parameter("uint256", "bidStart", 8)]
        public virtual BigInteger BidStart { get; set; }
        [Parameter("uint256", "bidEnd", 9)]
        public virtual BigInteger BidEnd { get; set; }
        [Parameter("bytes4", "bidDataType", 10)]
        public virtual byte[] BidDataType { get; set; }
        [Parameter("bytes", "bidData", 11)]
        public virtual byte[] BidData { get; set; }
        [Parameter("bytes", "bidSignature", 12)]
        public virtual byte[] BidSignature { get; set; }
        [Parameter("uint256", "sellOrderPaymentAmount", 13)]
        public virtual BigInteger SellOrderPaymentAmount { get; set; }
        [Parameter("uint256", "sellOrderNftAmount", 14)]
        public virtual BigInteger SellOrderNftAmount { get; set; }
        [Parameter("bytes", "sellOrderData", 15)]
        public virtual byte[] SellOrderData { get; set; }
    }
}
