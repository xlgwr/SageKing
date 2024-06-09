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
    #region C# 字符串 类型 转为 ICE字节流

    /// <summary>
    /// now with a custom memory pool with a tiny max buffer size
    /// </summary>
    /// <param name="tinyMaxBufferSize"></param>
    /// <returns></returns>
    public static MemoryPoolExts GetMemoryPool(this int tinyMaxBufferSize)
    {
        return new MemoryPoolExts(tinyMaxBufferSize);
    }

    /// <summary>
    /// 字符转ice字节流.
    /// </summary>
    public static byte[] EncodeString(this string msg)
    {
        // Arrange
        // now with a custom memory pool with a tiny max buffer size
        using var customPool = 7.GetMemoryPool();

        // minimumSegmentSize is not the same as the sizeHint given to GetMemory/GetSpan; it refers to the
        // minBufferSize given to Rent
        var pipe = new Pipe(new PipeOptions(pool: customPool, minimumSegmentSize: 5));
        var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
        encoder.EncodeString(msg);

        pipe.Writer.Complete();
        pipe.Reader.TryRead(out ReadResult readResult);

        var result = readResult.Buffer.ToArray();

        // Cleanup pipe
        pipe.Reader.Complete();

        return result;
    }

    /// <summary>
    /// ice字节流转字符.
    /// </summary>
    public static string DecodeString(this byte[] bytes)
    {
        var sliceDecoder = new SliceDecoder(bytes, SliceEncoding.Slice2);
        return sliceDecoder.DecodeString();
    }

    /// <summary>
    /// ice字节流转字符.
    /// </summary>
    public static string DecodeString(this IList<byte> bytes)
    {
        return bytes.ToArray().DecodeString();
    }

    /// <summary>
    /// ice字节流转字符.
    /// </summary>
    public static string DecodeString(this IList<IList<byte>> bytes)
    {
        var first = bytes.FirstOrDefault();
        if (first != null)
        {
            return first.DecodeString();
        }
        return string.Empty;
    }
    #endregion
}
