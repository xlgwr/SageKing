using SageKing.UI.Blazor.SageKingAntDesign.Features;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseSageKingAntDesign(this IModule module, Action<SageKingAntDesignFeature>? configure = default)
    {
        module.Configure<SageKingAntDesignFeature>(feature =>
        {
            configure?.Invoke(feature);
        });
        return module;
    }
}
