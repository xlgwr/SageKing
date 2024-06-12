using SageKing.IceRPC.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseIceRPC(this IModule module, Action<IceRPCFeature>? configure = default)
    {
        module.Configure<IceRPCFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(typeof(IceRPCFeature).Assembly);
        });
        return module;
    }
}
