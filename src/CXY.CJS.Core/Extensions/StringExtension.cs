using System;
using System.Text.RegularExpressions;

namespace CXY.CJS.Core.Extensions
{
    public static class StringExtension
    {
        private static readonly Regex RegNumber = new Regex("^[0-9]+$");

        private static readonly Regex RegDigitOrNumber = new Regex(@"(?i)^[0-9a-z]+$");

        /// <summary>
        /// 判断字符串是否是日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDate(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            return DateTime.TryParse(str, out DateTime time);
        }

        /// <summary>
        /// 判断字符串是否为Number
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            var m = RegNumber.Match(str);
            return m.Success;
        }

        /// <summary>
        /// 判断字符串是否为数字和字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDigitOrNumber(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            var m = RegDigitOrNumber.Match(str);
            return m.Success;
        }
    }
}
