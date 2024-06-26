﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 
/// </summary>
/// <typeparam name="Tclientconn">ClientConnection</typeparam>
/// <typeparam name="Toption">IceRPCClientOption</typeparam>
/// <typeparam name="Tpackage">StreamPackage</typeparam>
/// <typeparam name="Tpipe">Pipeline</typeparam>
public interface IClientConnection<Tclientconn, Toption, Tpackage, Tpipe, TIdentity> : IDisposable
{
    public Tclientconn Connection { get; }

    public Tpipe Pipeline { get; }

    public Toption Options { get; }

    /// <summary>
    /// 客户端认证信息
    /// </summary>
    public TIdentity Identity { get; }

    /// <summary>
    /// 客户端类型 <ref>ClientType</ref>
    /// </summary>
    public int ClientType { get; }

    /// <summary>
    /// 服务端类型 <ref>ClientType</ref>
    /// </summary>
    public int ServerType { get; }

    public void InitClient(Toption options);

    public Task ConnectAsync(CancellationToken cancellationToken = default(CancellationToken));

    public Task ShutdownAsync(CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="param"></param>
    /// <param name="msg"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Tpackage> SendStreamPackageListAsync(IEnumerable<Tpackage> param, string msg, CancellationToken cancellationToken = default(CancellationToken));
}
