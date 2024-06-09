using SageKing.MediatR.Features;

namespace SageKing.Features;

/// <summary>
/// Represents SageKing as a feature of the system.
/// </summary>

[DependsOn(typeof(MediatRFeature))]
public class SageKingFeature : FeatureBase
{

    /// <inheritdoc />
    public SageKingFeature(IModule module) : base(module)
    {
    }
}