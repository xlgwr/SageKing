using IceRpc;
using SageKingIceRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Contracts;

public interface ISageKingPackagesService
{
    /// <summary>
    /// 获取消息列表
    /// </summary>
    public ConcurrentDictionary<string, List<StreamPackage[]>> GetMessagesDic {  get; }


    public ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>> GetClientConnectionDic { get; }

    /// <summary>
    /// 服务端名称列表
    /// </summary>
    public List<string> GetServerNames {  get; }

    /// <summary>
    /// 消息回调
    /// </summary>
    public Action<string, string, int> NoticeAction { get; set; }

    /// <summary>
    /// 发送消息
    /// 客户端 发送消息 到 服务端
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="serverName"></param>
    /// <returns></returns>
    public Task<int> SendMsgAsync(string msg, string serverName);

    /// <summary>
    /// 推送消息
    /// 服务端 向客户端 推送消息
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="connectionid"></param>
    /// <returns></returns>
    public Task<int> PushMsgAsync(string msg, string connectionid);

    /// <summary>
    /// 收到数据
    /// </summary>
    /// <param name="msgType"></param>
    /// <param name="streams"></param>
    /// <returns></returns>
    public Task<int> ReceiverMsgAsync(string msgType, StreamPackage[] streams);

    /// <summary>
    /// 客户端连接变化
    /// </summary>
    /// <param name="isAdd"></param>
    /// <param name="clientConnection"></param>
    /// <returns></returns>
    public Task<int> ClientConnectionChangeAsync(bool isAdd, ClientConnectionInfo<IConnectionContext> clientConnection);
}
