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
    public Action<SageKingCacheOptions> SageKingCacheOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(SageKingCacheOptions)
            .AddSingleton<ICache, SageKingRedisCache>()
            .AddSingleton<SageKingCacheService>();
    }
}
