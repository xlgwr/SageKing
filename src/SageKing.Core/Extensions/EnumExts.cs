using SageKing.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Extensions;

public static class EnumExts
{
    /// <summary>
    /// 获取枚举值的Description
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDescription<T>(this T value) where T : struct
    {
        string result = string.Empty;
        try
        {
            result = value.ToString();
            Type type = typeof(T);
            FieldInfo info = type.GetField(value.ToString());
            var attributes = info.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attributes != null && attributes.FirstOrDefault() != null)
            {
                result = (attributes.First() as DescriptionAttribute).Description;
            }
        }
        catch { }
        return result;

    }

    /// <summary>
    /// 根据Description获取枚举值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T GetValueByDescription<T>(this string description) where T : struct
    {
        Type type = typeof(T);
        foreach (var field in type.GetFields())
        {
            if (field.Name == description)
            {
                return (T)field.GetValue(null);
            }

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attributes != null && attributes.FirstOrDefault() != null)
            {
                if (attributes.First().Description == description)
                {
                    return (T)field.GetValue(null);
                }
            }
        }

        throw new ArgumentException($"{description} 未能找到对应的枚举.", description);
    }

    /// <summary>
    /// 获取string获取枚举值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T GetValue<T>(this string value, string defaultValue) where T : struct
    {
        if (!Enum.IsDefined(typeof(T), value))
        {
            value = defaultValue;
        }
        return (T)Enum.ToObject(typeof(T), value);

    }

    /// <summary>
    /// 获取int获取枚举值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T GetValue<T>(this int value, int defaultValue = 0) where T : struct
    {
        if (!Enum.IsDefined(typeof(T), value))
        {
            value = defaultValue;
        }
        return (T)Enum.ToObject(typeof(T), value);
    }
}
