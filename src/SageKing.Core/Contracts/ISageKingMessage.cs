using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 消息结构定义
/// </summary>
public interface ISageKingMessage
{
    public string Id { get; set; }

    public string Varsion { get; set; }

    public string Name { get; set; }

    public int Type { get; set; }

    public string Description { get; set; }

    #region Add Or Update
    /// <summary>
    /// 新增 属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="attributeName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<string> value);
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<sbyte> value);
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<byte> value);
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<short> value);
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<int> value);
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<uint> value);
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<long> value);
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<ulong> value);
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<float> value);
    public bool AddOrUpdate(string attributeName, DataStreamTypValue<double> value);

    #endregion

    #region Get
    /// <summary>
    /// 获取 属性值
    /// </summary>
    /// <param name="attributeName"></param>
    /// <returns></returns>
    public string Get(string attributeName);
    public sbyte Getsbyte(string attributeName);
    public byte Getbyte(string attributeName);
    public short Getshort(string attributeName);
    public ushort Getushort(string attributeName);
    public int Getint(string attributeName);
    public uint Getuint(string attributeName);
    public long Getlong(string attributeName);
    public ulong Getulong(string attributeName);
    public float Getfloat(string attributeName);
    public double Getdouble(string attributeName);

    #endregion

    /// <summary>
    /// 移除 属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="attributeName"></param>
    /// <param name="type">应为arr集合类型>=50</param>
    /// <returns></returns>
    public bool Remove(string attributeName, DataStreamTypeEnum type);

    /// <summary>
    /// 初始化属性及位置
    /// </summary>
    /// <param name="posDic">DataStreamTypeEnum arr集合类型>=50</param>
    /// <returns></returns>
    public bool InitAttribytePos(Dictionary<DataStreamTypeEnum, Dictionary<string, byte>> posDic);

    /// <summary>
    /// 获取属性及位置
    /// </summary>
    /// <returns></returns>
    public Dictionary<DataStreamTypeEnum, Dictionary<string, byte>> GetPosData();

    /// <summary>
    /// 收到消息， 加载数据，并初始化
    /// </summary>
    /// <param name="packageData"></param>
    public void LoadData(StreamPackageData packageData);

    /// <summary>
    /// 转换数据 用于 发送
    /// </summary>
    /// <returns></returns>
    public StreamPackageData ToData(bool force = false);

    /// <summary>
    /// 清理数据,不清理位置信息
    /// </summary>
    public void ClearData();
}
