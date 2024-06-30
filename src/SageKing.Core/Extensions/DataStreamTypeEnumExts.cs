using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Extensions;

public static class DataStreamTypeEnumExts
{
    /// <summary>
    /// 获取DataStreamTypeEnum
    /// </summary>
    /// <param name="name"></param>
    /// <param name="attrNames"></param>
    /// <returns></returns>
    public static DataStreamTypeEnum GetDataStreamTypeEnum(this string name, Dictionary<string, int> attrNames)
    {
        foreach (var item in attrNames)
        {
            if (name.Contains(item.Key, StringComparison.OrdinalIgnoreCase))
            {
                return (DataStreamTypeEnum)item.Value;
            }
        }
        return DataStreamTypeEnum.String;
    }
}