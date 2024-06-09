using IceRpc.Features;
using IceRpc.Slice;
using Microsoft.Extensions.Logging;
using SageKing.IceRPC.EventMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Client.Services.SliceService;

[SliceService]
public partial class ClientReceiver(IMediator mediator, ILoggerFactory loggerFactory) : IClientReceiverService
{
    ILogger logger = loggerFactory.CreateLogger<ClientReceiver>();
    public async ValueTask<StreamPackage> PushStreamPackageListAsync(StreamPackage[] responseFields, string msgType, IFeatureCollection features, CancellationToken cancellationToken)
    {
        Console.WriteLine($"PushStreamPackageListAsync:msgType:{msgType}:{responseFields.toJsonNoByteStr()}");

        //获取相应的服务
        if (mediator != null)
        {
            try
            {
                return await mediator.Send(new ClientReceiverRequest(responseFields, msgType));
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "mediator Send 方法");
            }
        }
        int serverNo = responseFields.Any() ? responseFields.FirstOrDefault().ServiceNo : 0;
        var rsp = serverNo.GetStreamPackage(-1, "PushStreamPackageListAsync 没有关联注册服务ClientReceiverIRequest处理方法!");
        return rsp;
    }

    public ValueTask<int> RegClientAsync(Identity ident, int type, IFeatureCollection features, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
