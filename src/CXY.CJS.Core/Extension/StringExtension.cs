using System;

namespace CXY.CJS.Core.Extension
{
    public static class StringExtension
    {
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