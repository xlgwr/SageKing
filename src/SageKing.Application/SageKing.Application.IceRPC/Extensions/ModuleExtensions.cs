using SageKing.Application.IceRPC.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseSageKingApplicationIceRPC(this IModule module, Action<SageKingApplicationIceRPCFeature>? configure = default)
    {
        module.Configure<SageKingApplicationIceRPCFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(typeof(SageKingApplicationIceRPCFeature).Assembly);
        });
        return module;
    }
}
