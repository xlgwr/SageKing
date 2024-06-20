﻿using SageKing.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace SageKing.Extensions;


/// <summary>
/// 对象拓展
/// </summary>
[SuppressSniffer]
public static partial class ObjectExtension
{
    /// <summary>
    /// 判断类型是否实现某个泛型
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="generic">泛型类型</param>
    /// <returns>bool</returns>
    public static bool HasImplementedRawGeneric(this Type type, Type generic)
    {
        // 检查接口类型
        var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
        if (isTheRawGenericType) return true;

        // 检查类型
        while (type != null && type != typeof(object))
        {
            isTheRawGenericType = IsTheRawGenericType(type);
            if (isTheRawGenericType) return true;
            type = type.BaseType;
        }

        return false;

        // 判断逻辑
        bool IsTheRawGenericType(Type type) => generic == (type.IsGenericType ? type.GetGenericTypeDefinition() : type);
    }

    /// <summary>
    /// 将字典转化为QueryString格式
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="urlEncode"></param>
    /// <returns></returns>
    public static string ToQueryString(this Dictionary<string, string> dict, bool urlEncode = true)
    {
        return string.Join("&", dict.Select(p => $"{(urlEncode ? p.Key?.UrlEncode() : "")}={(urlEncode ? p.Value?.UrlEncode() : "")}"));
    }

    /// <summary>
    /// 将字符串URL编码
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string UrlEncode(this string str)
    {
        return string.IsNullOrEmpty(str) ? "" : System.Uri.EscapeDataString(str);
    } 

    /// <summary>
    /// 将object转换为long，若失败则返回0
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static long ParseToLong(this object obj)
    {
        try
        {
            return long.Parse(obj.ToString());
        }
        catch
        {
            return 0L;
        }
    }

    /// <summary>
    /// 将object转换为long，若失败则返回指定值
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static long ParseToLong(this string str, long defaultValue)
    {
        try
        {
            return long.Parse(str);
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// 将object转换为double，若失败则返回0
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static double ParseToDouble(this object obj)
    {
        try
        {
            return double.Parse(obj.ToString());
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 将object转换为double，若失败则返回指定值
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static double ParseToDouble(this object str, double defaultValue)
    {
        try
        {
            return double.Parse(str.ToString());
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// 将string转换为DateTime，若失败则返回日期最小值
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime ParseToDateTime(this string str)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return DateTime.MinValue;
            }
            if (str.Contains('-') || str.Contains('/'))
            {
                return DateTime.Parse(str);
            }
            else
            {
                int length = str.Length;
                switch (length)
                {
                    case 4:
                        return DateTime.ParseExact(str, "yyyy", System.Globalization.CultureInfo.CurrentCulture);

                    case 6:
                        return DateTime.ParseExact(str, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);

                    case 8:
                        return DateTime.ParseExact(str, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                    case 10:
                        return DateTime.ParseExact(str, "yyyyMMddHH", System.Globalization.CultureInfo.CurrentCulture);

                    case 12:
                        return DateTime.ParseExact(str, "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);

                    case 14:
                        return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                    default:
                        return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                }
            }
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    /// <summary>
    /// 将string转换为DateTime，若失败则返回默认值
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static DateTime ParseToDateTime(this string str, DateTime? defaultValue)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue.GetValueOrDefault();
            }
            if (str.Contains('-') || str.Contains('/'))
            {
                return DateTime.Parse(str);
            }
            else
            {
                int length = str.Length;
                switch (length)
                {
                    case 4:
                        return DateTime.ParseExact(str, "yyyy", System.Globalization.CultureInfo.CurrentCulture);

                    case 6:
                        return DateTime.ParseExact(str, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);

                    case 8:
                        return DateTime.ParseExact(str, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                    case 10:
                        return DateTime.ParseExact(str, "yyyyMMddHH", System.Globalization.CultureInfo.CurrentCulture);

                    case 12:
                        return DateTime.ParseExact(str, "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);

                    case 14:
                        return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                    default:
                        return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                }
            }
        }
        catch
        {
            return defaultValue.GetValueOrDefault();
        }
    }

    /// <summary>
    /// 将 string 时间日期格式转换成字符串 如 {yyyy} => 2024
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ParseToDateTimeForRep(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            str = $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";

        var date = DateTime.Now;
        var reg = new Regex(@"(\{.+?})");
        var match = reg.Matches(str);
        match.ToList().ForEach(u =>
        {
            var temp = date.ToString(u.ToString().Substring(1, u.Length - 2));
            str = str.Replace(u.ToString(), temp);
        });
        return str;
    }

    /// <summary>
    /// 是否有值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this object obj)
    {
        return obj == null || string.IsNullOrEmpty(obj.ToString());
    }

    /// <summary>
    /// 字符串掩码
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="mask">掩码符</param>
    /// <returns></returns>
    public static string Mask(this string str, char mask = '*')
    {
        if (string.IsNullOrWhiteSpace(str?.Trim()))
            return str;

        str = str.Trim();
        var masks = mask.ToString().PadLeft(4, mask);
        return str.Length switch
        {
            >= 11 => Regex.Replace(str, "(.{3}).*(.{4})", $"$1{masks}$2"),
            10 => Regex.Replace(str, "(.{3}).*(.{3})", $"$1{masks}$2"),
            9 => Regex.Replace(str, "(.{2}).*(.{3})", $"$1{masks}$2"),
            8 => Regex.Replace(str, "(.{2}).*(.{2})", $"$1{masks}$2"),
            7 => Regex.Replace(str, "(.{1}).*(.{2})", $"$1{masks}$2"),
            6 => Regex.Replace(str, "(.{1}).*(.{1})", $"$1{masks}$2"),
            _ => Regex.Replace(str, "(.{1}).*", $"$1{masks}")
        };
    }

    /// <summary>
    /// 将字符串转为值类型，若没有得到或者错误返回为空
    /// </summary>
    /// <typeparam name="T">指定值类型</typeparam>
    /// <param name="str">传入字符串</param>
    /// <returns>可空值</returns>
    public static T? ParseTo<T>(this string str) where T : struct
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                MethodInfo method = typeof(T).GetMethod("Parse", new Type[] { typeof(string) });
                if (method != null)
                {
                    T result = (T)method.Invoke(null, new string[] { str });
                    return result;
                }
            }
        }
        catch
        {
        }
        return null;
    }

    /// <summary>
    /// 将字符串转为值类型，若没有得到或者错误返回为空
    /// </summary>
    /// <param name="str">传入字符串</param>
    /// <param name="type">目标类型</param>
    /// <returns>可空值</returns>
    public static object ParseTo(this string str, Type type)
    {
        try
        {
            if (type.Name == "String")
                return str;

            if (!string.IsNullOrWhiteSpace(str))
            {
                var _type = type;
                if (type.Name.StartsWith("Nullable"))
                    _type = type.GetGenericArguments()[0];

                MethodInfo method = _type.GetMethod("Parse", new Type[] { typeof(string) });
                if (method != null)
                    return method.Invoke(null, new string[] { str });
            }
        }
        catch
        {
        }
        return null;
    }

    /// <summary>
    /// 将一个对象属性值赋给另一个指定对象属性, 只复制相同属性的
    /// </summary>
    /// <param name="src">原数据对象</param>
    /// <param name="target">目标数据对象</param>
    /// <param name="changeProperties">属性集，键为原属性，值为目标属性</param>
    /// <param name="unChangeProperties">属性集，目标不修改的属性</param>
    public static void CopyTo(object src, object target, Dictionary<string, string> changeProperties = null, string[] unChangeProperties = null)
    {
        if (src == null || target == null)
            throw new ArgumentException("src == null || target == null ");

        var SourceType = src.GetType();
        var TargetType = target.GetType();

        if (changeProperties == null || changeProperties.Count == 0)
        {
            var fields = TargetType.GetProperties();
            changeProperties = fields.Select(m => m.Name).ToDictionary(m => m);
        }

        if (unChangeProperties == null || unChangeProperties.Length == 0)
        {
            foreach (var item in changeProperties)
            {
                var srcProperty = SourceType.GetProperty(item.Key);
                if (srcProperty != null)
                {
                    var sourceVal = srcProperty.GetValue(src, null);

                    var tarProperty = TargetType.GetProperty(item.Value);
                    tarProperty?.SetValue(target, sourceVal, null);
                }
            }
        }
        else
        {
            foreach (var item in changeProperties)
            {
                if (!unChangeProperties.Any(m => m == item.Value))
                {
                    var srcProperty = SourceType.GetProperty(item.Key);
                    if (srcProperty != null)
                    {
                        var sourceVal = srcProperty.GetValue(src, null);

                        var tarProperty = TargetType.GetProperty(item.Value);
                        tarProperty?.SetValue(target, sourceVal, null);
                    }
                }
            }
        }
    }
}