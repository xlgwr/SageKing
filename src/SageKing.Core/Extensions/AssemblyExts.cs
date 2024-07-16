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

    /// <summary>
    ///  MemoryStream 写入文件
    /// </summary>
    /// <param name="ms"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static async Task SaveToFileAsync(this MemoryStream ms, string fileName)
    {
        using var fileStream = new FileStream(
            path: fileName,
            mode: FileMode.OpenOrCreate,
            access: FileAccess.Write,
            share: FileShare.None,
            bufferSize: 8192,
            useAsync: true);

        await ms.CopyToAsync(fileStream);
    }

    /// <summary>
    ///  MemoryStream 从文件中读取
    /// </summary>
    /// <param name="ms"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static async Task<MemoryStream> LoadFromFileAsync(this string fileName)
    {
        using var memoryStream = new MemoryStream();

        using (var fileStream = new FileStream(
            path: fileName,
            mode: FileMode.Open,
            access: FileAccess.Read,
            share: FileShare.None,
            bufferSize: 8192,
            useAsync: true))
        {
            await fileStream.CopyToAsync(memoryStream);
        }
        return memoryStream;
    }

}