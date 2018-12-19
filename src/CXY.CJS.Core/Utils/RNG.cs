using System;
using System.Security.Cryptography;

namespace CXY.CJS.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class RNG
    {
        private static RNGCryptoServiceProvider rngp = new RNGCryptoServiceProvider();
        private static byte[] rb = new byte[4];

        /// <summary> 
        /// 产生一个非负数的乱数 
        /// </summary> 
        public static int Next()
        {
            rngp.GetBytes(rb);
            int value = BitConverter.ToInt32(rb, 0);
            if (value < 0) value = -value;
            return value;
        }
        /// <summary> 
        /// 产生一个非负数且最大值在 max 以下的乱数 
        /// </summary> 
        /// <param name="max">最大值</param> 
        public static int Next(int max)
        {
            rngp.GetBytes(rb);
            int value = BitConverter.ToInt32(rb, 0);
            value = value % (max + 1);
            if (value < 0) value = -value;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="max"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int RandomNext(int max, int index = 1)
        {
            Random rd = new Random(index);
            int value = rd.Next(0, max);
            return value;
        }
        /// <summary> 
        /// 产生一个非负数且最小值在 min 以上最大值在 max 以下的乱数 
        /// </summary> 
        /// <param name="min">最小值</param> 
        /// <param name="max">最大值</param> 
        public static int Next(int min, int max)
        {
            int value = Next(max - min) + min;
            return value;
        }
    }
}