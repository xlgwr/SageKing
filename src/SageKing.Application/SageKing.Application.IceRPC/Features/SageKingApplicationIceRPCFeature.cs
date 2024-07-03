using SageKing.Application.IceRPC.Service;

namespace SageKing.Application.IceRPC.Features;

public class SageKingApplicationIceRPCFeature : FeatureBase
{
    public SageKingApplicationIceRPCFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// Represents the options for SageKingApplicationIceRPCs feature.
    /// </summary>
    public Action<SageKingApplicationIceRPCOptions> SageKingApplicationIceRPCOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(SageKingApplicationIceRPCOptions)
            .AddSingleton<ISageKingPackagesService, SageKingPackagesService>();
    }
}
