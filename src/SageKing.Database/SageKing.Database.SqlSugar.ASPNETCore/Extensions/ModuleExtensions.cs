using StackExchange.Profiling.Storage;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseSageKingSqlSugarAspNetCore(this IModule module, Action<SageKingSqlSugarAspNetCoreFeature>? configure = default)
    {     
        module.Configure<SageKingSqlSugarAspNetCoreFeature>(feature =>
        {
            configure?.Invoke(feature);
        });

        //添加工作单元
        module.AddUnitOfWork<SqlSugarUnitOfWork>();

        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(typeof(SageKingSqlSugarAspNetCoreFeature).Assembly);
        }); 

        return module;
    }

    public static IModule AddUnitOfWork<TUnitOfWork>(this IModule module)
        where TUnitOfWork : class, IUnitOfWork
    {
        module.Services.AddTransient<IUnitOfWork, TUnitOfWork>();        
        return module;
    }
}
