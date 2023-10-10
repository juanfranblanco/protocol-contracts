using Nethereum.Contracts.Standards.ERC20.TokenList;
using Nethereum.Contracts;
using Nethereum.Web3;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.BlockchainProcessing.BlockStorage.Entities.Mapping;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.ABI;

namespace ProtocolContracts
{
    public class AssetTypeEncoder
    {
        public static string Id(string value)
        {
            return  Nethereum.Util.Sha3Keccack.Current.CalculateHash(value).Substring(0, 8).EnsureHexPrefix();
        }

        public static byte[] Encode(string tokenAddress, long? tokenId = null)
        {
            if (tokenId != null)
            {
                return new ABIEncode().GetABIEncoded(new ABIValue("address", tokenAddress), new ABIValue("uint256", tokenId));
            }
            else
            {
                return new ABIEncode().GetABIEncoded(new ABIValue("address", tokenAddress));

            }
        }

        public static readonly string ETH = Id("ETH");
        public static readonly string ERC20 = Id("ERC20");
        public static readonly string ERC721 = Id("ERC721");
        public static readonly string ERC721_LAZY = Id("ERC721_LAZY");
        public static readonly string ERC1155 = Id("ERC1155");
        public static readonly string ERC1155_LAZY = Id("ERC1155_LAZY");
        public static readonly string COLLECTION = Id("COLLECTION");
        public static readonly string CRYPTO_PUNKS = Id("CRYPTO_PUNKS");
        public static readonly string ORDER_DATA_V1 = Id("V1");
        public static readonly string ORDER_DATA_V2 = Id("V2");
        public static readonly string ORDER_DATA_V3_BUY = Id("V3_BUY");
        public static readonly string ORDER_DATA_V3_SELL = Id("V3_SELL");
        public static readonly string TO_MAKER = Id("TO_MAKER");
        public static readonly string TO_TAKER = Id("TO_TAKER");
        public static readonly string PROTOCOL = Id("PROTOCOL");
        public static readonly string ROYALTY = Id("ROYALTY");
        public static readonly string ORIGIN = Id("ORIGIN");
        public static readonly string PAYOUT = Id("PAYOUT");
        public static readonly string LOCK = Id("LOCK");
        public static readonly string UNLOCK = Id("UNLOCK");
        public static readonly string TO_LOCK = Id("TO_LOCK");
    }
}
