using BlazorPro.BlazorSize;
using SageKing.UI.Blazor.SageKingAntDesign.Services;

namespace SageKing.UI.Blazor.SageKingAntDesign.Features;

public class SageKingAntDesignFeature : FeatureBase
{
    public SageKingAntDesignFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// AntDesignOptions
    /// </summary>
    public Action<SageKingAntDesignOptions> AntDesignServiceConfiguration { get; set; } = _ => { };

    /// <summary>
    /// ResizeOptions
    /// </summary>
    public Action<ResizeOptions> ResizeOptionsConfiguration { get; set; } = (options) =>
    {

        options.ReportRate = 300;
        options.EnableLogging = true;
        options.SuppressInitEvent = true;

        
    };
    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(AntDesignServiceConfiguration)
            .AddMediaQueryService()
            .AddResizeListener(ResizeOptionsConfiguration)
            .AddScoped<AntDesignIconListService>()
            .AddAntDesign()
           ;
    }
}
