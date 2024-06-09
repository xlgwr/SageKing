namespace SageKing.Features;

/// <summary>
/// A wrapper for invoking application-specific configuration, ensuring it is invoked lastly.
/// </summary>
[DependsOn(typeof(SageKingFeature))]
public class AppFeature : FeatureBase
{
    /// <inheritdoc />
    public AppFeature(IModule module) : base(module)
    {
    }
    
    /// <summary>
    /// The configurator to invoke.
    /// </summary>
    public Action<IModule>? Configurator { get; set; }

    /// <inheritdoc />
    public override void Configure()
    {
        Configurator?.Invoke(Module);
    }
}