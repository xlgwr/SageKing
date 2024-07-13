using SageKing.CodeGen.SageKingViewEngine.Features;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseSageKingViewEngine(this IModule module, Action<SageKingViewEngineFeature>? configure = default)
    {
        module.Configure<SageKingViewEngineFeature>(feature =>
        {
            configure?.Invoke(feature);
        });
        return module;
    }
}
