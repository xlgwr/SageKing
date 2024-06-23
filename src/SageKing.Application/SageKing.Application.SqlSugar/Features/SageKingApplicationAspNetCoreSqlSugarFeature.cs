using SageKing.Application.AspNetCore.SqlSugar.Service;
using SageKing.Cache.Features;
using SageKing.Database.SqlSugar.AspNetCore;
using SageKing.Database.SqlSugar.AspNetCore.Features;
using SageKing.Database.SqlSugar.Features;

namespace SageKing.Application.AspNetCore.SqlSugar.Features;


[DependsOn(typeof(SageKingCacheFeature))]
[DependsOn(typeof(SageKingSqlSugarAspNetCoreFeature))]
public class SageKingApplicationAspNetCoreSqlSugarFeature : FeatureBase
{
    public SageKingApplicationAspNetCoreSqlSugarFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// Represents the options for SageKingApplicationAspNetCoreSqlSugars feature.
    /// </summary>
    public Action<SageKingApplicationAspNetCoreSqlSugarOptions> ApplicationAspNetCoreSqlSugarOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(ApplicationAspNetCoreSqlSugarOptions)
            .AddTransient<ICacheService, SqlSugarCache>()
            .AddTransient<SysConfigService>()
            .AddTransient<SysMenuService>()
            .AddTransient<SysDictDataService>()
            .AddTransient<SysDictTypeService>()
            .AddTransient<SysSageKingMessageService>()
            .AddTransient<SysSageKingMessageAttributeService>()
            .AddSingleton<ISqlSugarAspNetCoreFilter, SqlSugarAspNetCoreFilter>();
    }

    public override void Init()
    {
        //默认调用InitDB
        var sqlSugar = Services.BuildServiceProvider().GetService<ISqlSugarClient>(); 

    }
}
