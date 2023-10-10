using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ProtocolContracts.Contracts.ExchangeSimpleV2.ContractDefinition
{
    public partial class Purchase : PurchaseBase { }

    public class PurchaseBase 
    {
        [Parameter("address", "sellOrderMaker", 1)]
        public virtual string SellOrderMaker { get; set; }
        [Parameter("uint256", "sellOrderNftAmount", 2)]
        public virtual BigInteger SellOrderNftAmount { get; set; }
        [Parameter("bytes4", "nftAssetClass", 3)]
        public virtual byte[] NftAssetClass { get; set; }
        [Parameter("bytes", "nftData", 4)]
        public virtual byte[] NftData { get; set; }
        [Parameter("uint256", "sellOrderPaymentAmount", 5)]
        public virtual BigInteger SellOrderPaymentAmount { get; set; }
        [Parameter("address", "paymentToken", 6)]
        public virtual string PaymentToken { get; set; }
        [Parameter("uint256", "sellOrderSalt", 7)]
        public virtual BigInteger SellOrderSalt { get; set; }
        [Parameter("uint256", "sellOrderStart", 8)]
        public virtual BigInteger SellOrderStart { get; set; }
        [Parameter("uint256", "sellOrderEnd", 9)]
        public virtual BigInteger SellOrderEnd { get; set; }
        [Parameter("bytes4", "sellOrderDataType", 10)]
        public virtual byte[] SellOrderDataType { get; set; }
        [Parameter("bytes", "sellOrderData", 11)]
        public virtual byte[] SellOrderData { get; set; }
        [Parameter("bytes", "sellOrderSignature", 12)]
        public virtual byte[] SellOrderSignature { get; set; }
        [Parameter("uint256", "buyOrderPaymentAmount", 13)]
        public virtual BigInteger BuyOrderPaymentAmount { get; set; }
        [Parameter("uint256", "buyOrderNftAmount", 14)]
        public virtual BigInteger BuyOrderNftAmount { get; set; }
        [Parameter("bytes", "buyOrderData", 15)]
        public virtual byte[] BuyOrderData { get; set; }
    }
}
