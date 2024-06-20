using SageKing.Cache.Features;
using SageKing.Database.SqlSugar.AspNetCore.Features;
using SageKing.Database.SqlSugar.Features;

namespace SageKing.Application.SqlSugar.Features;


[DependsOn(typeof(SageKingCacheFeature))]
[DependsOn(typeof(SageKingSqlSugarAspNetCoreFeature))]
public class SageKingApplicationSqlSugarFeature : FeatureBase
{
    public SageKingApplicationSqlSugarFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// Represents the options for SageKingApplicationSqlSugars feature.
    /// </summary>
    public Action<SageKingApplicationSqlSugarOptions> DatabaseOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(DatabaseOptions);
    }
}
