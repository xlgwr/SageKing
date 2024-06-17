using SageKing.Cache.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseSageKingCache(this IModule module, Action<SageKingCacheFeature>? configure = default)
    {
        module.Configure<SageKingCacheFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(typeof(SageKingCacheFeature).Assembly);
        });
        return module;
    }
}
