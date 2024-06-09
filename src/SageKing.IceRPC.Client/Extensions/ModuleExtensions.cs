using SageKing.IceRPC.Client.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseIceRPCClient(this IModule module, Action<IceRPCClientFeature>? configure = default)
    {
        module.Configure<IceRPCClientFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(typeof(IceRPCClientFeature).Assembly);
        });
        return module;
    }
}
