using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroC.Slice;
using System.Buffers;
using SageKing.Core.Contracts;
using SageKingIceRpc;

namespace SageKing.IceRPC.Extensions;

public static partial class StreamPackageExt
{

    /// <summary>
    /// ice字节流返回StreamPackage.
    /// </summary>
    /// <param name="bytes">ice字节流.</param>
    /// <param name="typeEnum">ice字节流对应解码类型.</param>
    /// <returns>StreamPackage.</returns>
    public static StreamPackage GetStreamPackage(this byte[] bytes, DataStreamTypeEnum typeEnum)
    {
        return new StreamPackage()
        {
            ServiceNo = 0,
            ClientId = string.Empty,
            MsgId = string.Empty,
            Uuid = string.Empty,
            ErrorNo = 0,
            ErrorInfo = string.Empty,
            HeadDic = new Dictionary<string, string>(),
            DataStreamBody = new[] { bytes },
            DataStreamRowType = new byte[] { (byte)typeEnum },
        };
    }

    /// <summary>
    /// 检测字段是否为Null,并初始化为空
    /// </summary>
    /// <param name="s"></param>
    public static StreamPackage GetStreamPackage(this string errInfo, int serverNo, int errno = 0)
    {
        return new StreamPackage()
        {
            ServiceNo = serverNo,
            ErrorNo = errno,
            ErrorInfo = errInfo,
            ClientId = string.Empty,
            MsgId = string.Empty,
            Uuid = string.Empty,
            HeadDic = new Dictionary<string, string>(),
            DataStreamRowType = Array.Empty<byte>(),
            DataStreamBody = Array.Empty<byte[]>()
        };
    }
    public static StreamPackage GetStreamPackage(this int serverNo, int errNo, string errInfo = "")
    {
        return errInfo.GetStreamPackage(serverNo, errNo);
    }
    /// <summary>
    /// 不显示byte[][]
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string toJsonNoByteStr(this StreamPackage s, int serverno = 0)
    {
        if (s == null)
        {
            return $"{serverno}:StreamPackage为null";
        }
        string datadic = s.HeadDic.DicCopy();
        return $"ServiceNo:{s.ServiceNo},ClientId:{s.ClientId},UUID:{s.Uuid},MsgId:{s.MsgId},ErrorNo:{s.ErrorNo},ErrorInfo:{s.ErrorInfo},DatasDic:{datadic},DataStreamBody.Len:{s.DataStreamBody.GetRowLengStr()};";
    }
    /// <summary>
    /// row:byte[].length
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string GetRowLengStr(this IList<IList<byte>> s)
    {
        StringBuilder DataStreamBodyString = new StringBuilder();
        if (s.Any())
        {
            int count = 0;
            foreach (var item in s)
            {
                DataStreamBodyString.Append($"[{count}:{item.Count}]");
                count++;
            }
        }
        else
        {
            DataStreamBodyString.Append("-1");
        }
        return DataStreamBodyString.ToString();
    }
    /// <summary>
    /// 不显示byte[][]
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string toJsonNoByteStr(this StreamPackage[] s)
    {
        StringBuilder result = new StringBuilder();
        foreach (var item in s)
        {
            result.Append(item.toJsonNoByteStr());
        }
        return result.ToString();
    }
    public static string DicCopy(this IDictionary<string, string> dic)
    {
        string datadic = "[";
        try
        {
            if (!dic.Any())
            {
                return "[]";
            }
            var keys = dic.Keys.ToList();
            foreach (var item in keys)
            {
                datadic += $"{item}:{dic[item]},";
            }
        }
        catch /*(Exception ex)*/
        {

        }
        return datadic.TrimEnd(',') + "]";
    }
}
