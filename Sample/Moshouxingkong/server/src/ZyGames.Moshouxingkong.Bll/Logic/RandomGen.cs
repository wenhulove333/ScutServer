using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ZyGames.Moshouxingkong.Bll.Logic
{
    /*生成随机数的单例实现*/
    class RandomGen
    {
        private string _usermagic;
        private RNGCryptoServiceProvider _gen;

        public string UserMagic
        {
            get 
            { 
                byte[] randomseries = new byte[64];
                /*获取随机数*/
                _gen.GetBytes(randomseries);

                /*清空字符串*/
                _usermagic = string.Empty;

                /*将二进制流转换成十六机制字符串*/
                for (int i = 0; i < randomseries.Length; i++)
                {
                    _usermagic += Convert.ToString(randomseries[i], 16);
                }

                return _usermagic; 
            }
        }

        private RandomGen()
        {
            _gen = new RNGCryptoServiceProvider();
        }

        public static readonly RandomGen Instance = new RandomGen();
    }
}
