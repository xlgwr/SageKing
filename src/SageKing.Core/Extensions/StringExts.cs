using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Extensions
{
    public static class StringExts
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {

            return string.IsNullOrWhiteSpace(str);

        }
        public static bool IsNullOrEmpty(this string str)
        {

            return string.IsNullOrEmpty(str);

        }
        public static string NullOrWhiteSpaceToDefault(this string valueSource, string valueDist)
        {
            if (valueSource.IsNullOrWhiteSpace())
            {
                return valueDist;
            }
            return valueSource;
        }

        public static bool IsNotNullOrWhiteSpace(this string str)
        {

            return !string.IsNullOrWhiteSpace(str);

        }

        public static string ToNotNullString(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString();
        }
        public static string ToNotNullString(this object obj, string defaultvalue = "")
        {
            if (obj == null)
            {
                return defaultvalue;
            }
            return obj.ToString();
        }

        /// <summary>
        /// 少于0时,返回0;
        /// a < 0 ? 0 : a
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int CheckToZero(this int a)
        {
            return a < 0 ? 0 : a;
        }
        /// <summary>
        /// 是否判定返回
        /// 1：是
        /// 其它：否
        /// a == 1 ? isOK : isNot
        /// </summary>
        /// <param name="a"></param>
        /// <param name="isOK"></param>
        /// <param name="isNot"></param>
        /// <returns></returns>
        public static string ToIsOkOrNot(this int a, string isOK = "是", string isNot = "否")
        {
            return a == 1 ? isOK : isNot;
        }
        public static string ToIsOkOrNot(this bool a, string isOK = "是", string isNot = "否")
        {
            return a ? isOK : isNot;
        }
        public static string ToIsSuccessOrNot(this bool a, string msg = "", string isOK = "成功", string isNot = "失败")
        {
            return a ? $"{isOK}{msg}" : $"{isNot}{msg}";
        }
        /// <summary>
        /// 取右边几位
        /// 超过长度，取全部
        /// </summary>
        /// <param name="a"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRightStr(this string a, int length = 2)
        {
            if (a.IsNullOrEmpty())
            {
                return a;
            }
            if (a.Length > length)
            {
                return a.Substring(a.Length - length);
            }
            return a;
        }
        /// <summary>
        /// 取左边几位
        /// 超过长度，取全部
        /// </summary>
        /// <param name="a"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetLeftStr(this string a, int length = 2)
        {
            if (a.IsNullOrEmpty())
            {
                return a;
            }
            if (a.Length > length)
            {
                return a.Substring(0, length);
            }
            return a;
        }
        /// <summary>
        /// 去空，去空格
        /// </summary>
        /// <param name="m"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<string> SplitTrim(this string m, params char[] separator)
        {
            if (m == null)
            {
                return null;
            }
            var result = new List<string>();
            if (m.IsNullOrWhiteSpace())
            {
                return result;
            }
            var resultSplit = m.Split(separator);
            foreach (var item in resultSplit)
            {
                if (!item.IsNullOrWhiteSpace())
                {
                    result.Add(item.Trim());
                }
            }
            return result;
        }
        /// <summary>
        /// 去空，去空格
        /// </summary>
        /// <param name="m"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static Dictionary<string, bool> SplitTrimDic(this string m, params char[] separator)
        {
            if (m == null)
            {
                return null;
            }
            var result = new Dictionary<string, bool>();
            var resultSplit = m.Split(separator);
            foreach (var item in resultSplit)
            {
                if (!item.IsNullOrWhiteSpace())
                {
                    result[item.Trim()] = true;
                }
            }
            return result;
        }

        public static List<string> SplitToLowerTrim(this string m, params char[] separator)
        {
            if (m == null)
            {
                return null;
            }
            var result = new List<string>();
            if (m.IsNullOrWhiteSpace())
            {
                return result;
            }
            var resultSplit = m.Split(separator);
            foreach (var item in resultSplit)
            {
                if (!item.IsNullOrWhiteSpace())
                {
                    result.Add(item.ToLower().Trim());
                }
            }
            return result;
        }
        /// <summary>
        /// 去空，去空格
        /// </summary>
        /// <param name="m"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static Dictionary<string, bool> SplitTrimDic(this string[] m)
        {
            if (m == null || m.Length < 1)
            {
                return null;
            }
            var result = new Dictionary<string, bool>();
            foreach (var item in m)
            {
                if (!item.IsNullOrWhiteSpace())
                {
                    result[item.Trim()] = true;
                }
            }
            return result;
        }

    }
}
