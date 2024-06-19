using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SageKing.Database.Features;
using SageKing.Database.SqlSugar.Features;

namespace SageKing.Extensions;

[DependsOn(typeof(SageKingDatabaseFeature))]
public static class ModuleExtensions
{
    public static IModule UseSageKingDatabaseSqlSugar(this IModule module, Action<SageKingDatabaseSqlSugarFeature>? configure = default)
    {
        module.Configure<SageKingDatabaseSqlSugarFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(typeof(SageKingDatabaseSqlSugarFeature).Assembly);
        });
        return module;
    }
}
