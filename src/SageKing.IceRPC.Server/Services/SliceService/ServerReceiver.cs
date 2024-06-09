﻿using IceRpc.Features;
using IceRpc.Slice;
using Microsoft.Extensions.Logging;
using SageKing.IceRPC.EventMessage;
using SageKing.IceRPC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Server.Services.SliceService;

[SliceService]
public partial class ServerReceiver(IMediator mediator, ILoggerFactory loggerFactory) : IServerReceiverService
{

    ILogger logger = loggerFactory.CreateLogger<ServerReceiver>();

    public async ValueTask<StreamPackage> SendStreamPackageListAsync(StreamPackage[] requestFields, string msgType, IFeatureCollection features, CancellationToken cancellationToken)
    {
        Console.WriteLine($"SendStreamPackageListAsync:msgName:{msgType}:{requestFields.toJsonNoByteStr()}");

        //获取相应的服务
        if (mediator != null)
        {
            try
            {
                return await mediator.Send(new ServerReceiverRequest(requestFields, msgType));
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "mediator Send 方法");
            }
        }
        int serverNo = requestFields.Any() ? requestFields.FirstOrDefault().ServiceNo : 0;
        var rsp = serverNo.GetStreamPackage(-1, "SendStreamPackageListAsync 没有关联注册服务ServerReceiverIRequest处理方法!");
        return rsp;
    }
    public ValueTask<int> RegClientAsync(Identity ident, int type, IFeatureCollection features, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}