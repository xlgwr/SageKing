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

    public string Name { get; set; }

    public int Type { get; set; }

    public string Description { get; set; }

    /// <summary>
    /// 新增 属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="attributeName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool AddOrUpdate(string attributeName, (object value, DataStreamTypeEnum type) value);

    /// <summary>
    /// 获取 属性值
    /// </summary>
    /// <param name="attributeName"></param>
    /// <returns></returns>
    public (object value, DataStreamTypeEnum type) Get(string attributeName);

    /// <summary>
    /// 移除 属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="attributeName"></param>
    /// <returns></returns>
    public bool Remove<T>(string attributeName);

    /// <summary>
    /// 收到消息， 加载数据，并初始化
    /// </summary>
    /// <param name="packageData"></param>
    public void LoadData(StreamPackageData packageData);

    /// <summary>
    /// 转换数据 用于 发送
    /// </summary>
    /// <returns></returns>
    public StreamPackageData ToData();
}
