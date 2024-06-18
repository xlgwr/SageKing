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

    #region 数组类

    #region string[]
    static object _lockPipeArr=new object();
    static Pipe PipeArr;
    public static Pipe GetPipeArr()
    {
        if (PipeArr != null)
        {
            PipeArr.Reset();
            return PipeArr;
        }
        // Arrange
        // now with a custom memory pool with a tiny max buffer size
        var customPool = (1024).GetMemoryPool();

        // minimumSegmentSize is not the same as the sizeHint given to GetMemory/GetSpan; it refers to the
        // minBufferSize given to Rent

        PipeArr = new Pipe(new PipeOptions(pool: customPool, minimumSegmentSize: 5));

        return PipeArr;
    }
    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this string[] strings)
    {
        lock (_lockPipeArr)
        {
            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(strings, (ref SliceEncoder encoder, string value) => encoder.EncodeString(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;
        }
    }

    public static string[] GetString(this byte[] bytes)
    {
        var sliceDecoder = new SliceDecoder(bytes, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeString());
    }
    #endregion

    #region sbyte[]
    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="sbytes"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this sbyte[] sbytes)
    {
        lock (_lockPipeArr)
        {
            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(sbytes, (ref SliceEncoder encoder, sbyte value) => encoder.EncodeInt8(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;

        }
    }

    public static sbyte[] Getsbyte(this byte[] bytes)
    {
        var sliceDecoder = new SliceDecoder(bytes, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeInt8());
    }
    #endregion

    #region byte[]

    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this byte[] bytes)
    {
        lock (_lockPipeArr)
        {
            var pipe = GetPipeArr();

            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(bytes, (ref SliceEncoder encoder, byte value) => encoder.EncodeUInt8(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;

        }
    }

    public static byte[] Getbyte(this byte[] bytes)
    {
        var sliceDecoder = new SliceDecoder(bytes, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeUInt8());
    }
    #endregion

    #region short[]

    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="shorts"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this short[] shorts)
    {
        lock (_lockPipeArr)
        {
            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(shorts, (ref SliceEncoder encoder, short value) => encoder.EncodeInt16(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;

        }
    }

    public static short[] Getshort(this byte[] shorts)
    {
        var sliceDecoder = new SliceDecoder(shorts, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeInt16());
    }
    #endregion

    #region ushort[]

    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="ushorts"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this ushort[] ushorts)
    {
        lock (_lockPipeArr)
        {
            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(ushorts, (ref SliceEncoder encoder, ushort value) => encoder.EncodeUInt16(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;

        }
    }

    public static ushort[] Getushort(this byte[] ushorts)
    {
        var sliceDecoder = new SliceDecoder(ushorts, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeUInt16());
    }
    #endregion

    #region int[]

    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="ints"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this int[] ints)
    {
        lock (_lockPipeArr)
        {

            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(ints, (ref SliceEncoder encoder, int value) => encoder.EncodeInt32(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;
        }
    }

    public static int[] Getint(this byte[] ints)
    {
        var sliceDecoder = new SliceDecoder(ints, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeInt32());
    }
    #endregion

    #region uint[]

    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="uints"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this uint[] uints)
    {
        lock (_lockPipeArr)
        {

            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(uints, (ref SliceEncoder encoder, uint value) => encoder.EncodeUInt32(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;
        }
    }

    public static uint[] Getuint(this byte[] uints)
    {
        var sliceDecoder = new SliceDecoder(uints, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeUInt32());
    }
    #endregion

    #region long[]

    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="longs"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this long[] longs)
    {
        lock (_lockPipeArr)
        {
            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(longs, (ref SliceEncoder encoder, long value) => encoder.EncodeInt64(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;

        }
    }

    public static long[] Getlong(this byte[] longs)
    {
        var sliceDecoder = new SliceDecoder(longs, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeInt64());
    }
    #endregion

    #region ulong[]

    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="ulongs"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this ulong[] ulongs)
    {
        lock (_lockPipeArr)
        {

            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(ulongs, (ref SliceEncoder encoder, ulong value) => encoder.EncodeUInt64(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;
        }
    }

    public static ulong[] Getulong(this byte[] ulongs)
    {
        var sliceDecoder = new SliceDecoder(ulongs, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeUInt64());
    }
    #endregion

    #region float[]

    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="floats"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this float[] floats)
    {
        lock (_lockPipeArr)
        {
            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(floats, (ref SliceEncoder encoder, float value) => encoder.EncodeFloat32(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;

        }
    }

    public static float[] Getfloat(this byte[] floats)
    {
        var sliceDecoder = new SliceDecoder(floats, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeFloat32());
    }
    #endregion

    #region double[]

    /// <summary>
    /// 生成ICE字节流
    /// </summary>
    /// <param name="doubles"></param>
    /// <returns></returns>
    public static byte[] ToIceByte(this double[] doubles)
    {
        lock (_lockPipeArr)
        {
            var pipe = GetPipeArr();
            var encoder = new SliceEncoder(pipe.Writer, SliceEncoding.Slice2);
            encoder.EncodeSequence(doubles, (ref SliceEncoder encoder, double value) => encoder.EncodeFloat64(value));

            pipe.Writer.Complete();
            pipe.Reader.TryRead(out ReadResult readResult);

            var result = readResult.Buffer.ToArray();

            // Cleanup pipe
            pipe.Reader.Complete();

            return result;

        }
    }

    public static double[] Getdouble(this byte[] doubles)
    {
        var sliceDecoder = new SliceDecoder(doubles, SliceEncoding.Slice2);
        return sliceDecoder.DecodeSequence((ref SliceDecoder decoder) => decoder.DecodeFloat64());
    }
    #endregion

    #endregion
}
