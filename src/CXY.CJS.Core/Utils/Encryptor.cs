using System.Security.Cryptography;
using System.Text;

namespace CXY.CJS.Core.Utils
{
    public class Encryptor
    {
        /// <summary>
        /// 将明文加密成md5格式的密文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Entry(string str)
        {
            StringBuilder sb = new StringBuilder();
            MD5 m = MD5.Create();
            byte[] buffer = m.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (byte b in buffer)
            {
                if (b.ToString("x").Length < 2)
                {
                    sb.Append("0" + b.ToString("x"));
                }
                else
                {
                    sb.Append(b.ToString("x"));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将明文加密成md5格式的密文(带中文的)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5EntryCN(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(str));
            StringBuilder tmp = new StringBuilder();
            foreach (byte b in hashedDataBytes)
            {
                if (b.ToString("x").Length < 2)
                {
                    tmp.Append("0" + b.ToString("x"));
                }
                else
                {
                    tmp.Append(b.ToString("x"));
                }
            }
            return tmp.ToString();
        }
    }
}