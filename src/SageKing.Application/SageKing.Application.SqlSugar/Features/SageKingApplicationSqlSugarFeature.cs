namespace SageKing.Application.SqlSugar.Features;

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
