using SageKing.Application.SqlSugar.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseSageKingApplicationSqlSugar(this IModule module, Action<SageKingApplicationSqlSugarFeature>? configure = default)
    {
        module.Configure<SageKingApplicationSqlSugarFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(typeof(SageKingApplicationSqlSugarFeature).Assembly);
        });
        return module;
    }
}
