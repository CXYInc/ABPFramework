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

        public static int ToInt(this string value)
        {
            try
            {
                int returnValue = 0;
                value = CheckNum(value);
                int dot = value.IndexOf(".");
                if (dot < 0)
                {
                    int.TryParse(value, out returnValue);
                    return returnValue;
                }
                else
                {
                    dot = value.IndexOf(".");
                    int.TryParse(value.Substring(0, dot), out returnValue);
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Check whether the string is valid value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string CheckNum(string value, string defaultValue = "0")
        {
            string intStringList = "+-0123456789.";
            int a = 0, dotcount = 0;
            if (value == "")
                return defaultValue;
            else
            {
                foreach (char t in value)
                {
                    a = intStringList.IndexOf(t);
                    if (a < 0)
                        return defaultValue;
                    else if (a == 12)
                    {
                        dotcount++;
                        if (dotcount > 1)
                            return defaultValue;
                    }
                    else if (a == 0)
                    {
                        a = value.IndexOf("+");
                        if (a != 0)
                            return defaultValue;
                    }
                    else if (a == 1)
                    {
                        a = value.IndexOf("-");
                        if (a != 0)
                            return defaultValue;
                    }
                }
                return value;
            }
        }
    }
}
