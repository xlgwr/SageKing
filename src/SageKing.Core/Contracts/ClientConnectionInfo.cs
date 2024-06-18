using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

#pragma warning disable CA1305 
#pragma warning disable CS8618 // 不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

/// <summary>
/// 连接信息
/// </summary>
public record class ClientConnectionInfo<T>
{
    public T Connection { get; set; }
    public string ClientId { get; set; }
    public string ConnectionId { get; set; }
    public EndPoint RemoteAddress { get; set; }
    public string LoginDateTime { get; set; }
    public bool IsLogin { get; set; }
    public string Ver { get; set; }
    //public IceBaseOptions Option { get; set; }
    public int ClientType { get; set; }
    public string UserType { get; set; }
    /// <summary>
    /// 主用于对比是否同一连接
    /// guid
    /// </summary>
    public string IceProxyId { get; set; }

    private Dictionary<string, string> _userDic = new Dictionary<string, string>();
    public Dictionary<string, string> UserDic
    {
        get { return _userDic; }
    }
    public string ClientTypeDesc { get; set; }

    public string IpPort
    {
        get
        {
            return $"{RemoteAddress}";
        }
    }

    public override string ToString()
    {
        return $"ClientId:{ClientId},ConnectionId:{this.ConnectionId},ClientType:{this.ClientType},{this.ClientTypeDesc},IceProxyId：{this.IceProxyId},IsLogin：{this.IsLogin},RemoteAddress：{this.RemoteAddress},UserType：{this.UserType},Ver：{this.Ver},LoginDateTime：{this.LoginDateTime}";
    }
}
