using SageKing.Database.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseSageKingDatabase(this IModule module, Action<SageKingDatabaseFeature>? configure = default)
    {
        module.Configure<SageKingDatabaseFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(typeof(SageKingDatabaseFeature).Assembly);
        });
        return module;
    }
}
