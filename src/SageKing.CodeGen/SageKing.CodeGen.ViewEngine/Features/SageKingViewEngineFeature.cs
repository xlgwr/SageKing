
using SageKing.CodeGen.SageKingViewEngine;

namespace SageKing.CodeGen.SageKingViewEngine.Features;

public class SageKingViewEngineFeature : FeatureBase
{
    public SageKingViewEngineFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// AntDesignOptions
    /// </summary>
    public Action<SageKingViewEngineOptions> SageKingViewEngineConfiguration { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(SageKingViewEngineConfiguration)
             .AddTransient<IViewEngine, ViewEngine>();
        ;
    }
}
