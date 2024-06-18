using SageKing.Cache.Service;

namespace SageKing.Cache.Features;

public class SageKingCacheFeature : FeatureBase
{
    public SageKingCacheFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// Represents the options for SageKingCaches feature.
    /// </summary>
    public Action<SageKingCacheOptions> DatabaseOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(DatabaseOptions)
            .AddSingleton<SageKingCacheService>();
    }
}
