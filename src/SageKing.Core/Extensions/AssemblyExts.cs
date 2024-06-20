using SageKing.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class AssemblyExts
{
    /// <summary>
    /// 加载程序集中的类型
    /// 排除SuppressSniffer指定类型
    /// </summary>
    /// <param name="ass"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetTypesSuppressSniffer(this Assembly ass, Type suppressSnifferType)
    {
        var types = Array.Empty<Type>();

        try
        {
            types = ass.GetTypes();
        }
        catch
        {
            Console.WriteLine($"Error load `{ass.FullName}` assembly.");
        }

        return types.Where(u => u.IsPublic && !u.IsDefined(suppressSnifferType, false));
    }
}