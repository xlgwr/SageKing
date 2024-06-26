﻿using SageKing.IceRPC.EventMessage;
using SageKing.IceRPC.Extensions;
using SageKing.Studio.Data;
using SageKingIceRpc;

namespace SageKing.Studio.HandlerMessage;

public class ClientReceiverHandler(PackagesDataService packagesData) : IRequestHandler<ClientReceiverRequest, StreamPackage>
{
    public async Task<StreamPackage> Handle(ClientReceiverRequest request, CancellationToken cancellationToken)
    {
        if (packagesData.DataDic.TryGetValue(request.msgType, out var list))
        {
            list.Add(request.Packages);
        }
        else
        {
            packagesData.DataDic[request.msgType] = new List<StreamPackage[]> { request.Packages };
        }

        string desc = $"【{request.ClientName}:{packagesData.ClientTypeDic.GetDesc(request.ClientType)}】收到##$" +
            $"{request.msgType}:" +
            $"Packages:{request.Packages?.Length}";
        packagesData.NoticeAction?.Invoke($"客户端收到消息", desc, 1);

        await Task.CompletedTask;
        return 1000.GetStreamPackage(0, "ClientReceiverHandler 处理成功");
    }
}
