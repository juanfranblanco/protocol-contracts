using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolContracts
{
    public static class TestAccounts
    {
        public const string NethereumDefaultPrivateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
        public const string NethereumDefaultAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
        
        public const string Account1PrivateKey = "0x094b4bea491f4069a5bd5242308ff9435de816aad8b9be88d8203e471fc2a4b6";
        public const string Account1Address = "0x210ba3ddfbcB30f35aB8c1d7De18bc58e0f2710A";

        public const string Account2PrivateKey = "aa5bc10ebf5c73004e990f773608acce451fb919073a6121c2b57043682c1337";
        public const string Account2Address = "0x2b64faDae1485ba8D4590EA50A6a629176fA63B9";

        
        public static Web3 GetAccount1Web3(string url = "http://localhost:8545")
        {
            return new Web3(new Account(Account1PrivateKey), url);
        }

        public static Web3 GetDefaultNethereumAccountWeb3(string url = "http://localhost:8545")
        {
            return new Web3(new Account(NethereumDefaultPrivateKey), url);
        }

        public static Web3 GetAccount2Web3(string url = "http://localhost:8545")
        {
            return new Web3(new Account(Account2PrivateKey), url);
        }

       

    }
}
