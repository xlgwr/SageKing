using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace SageKing.Extensions;

/// <summary>
/// Provides extension methods to <see cref="IServiceCollection"/>. 
/// </summary>
public static class SageKingModuleExtensions
{
    public static readonly IDictionary<IServiceCollection, IModule> Modules = new ConcurrentDictionary<IServiceCollection, IModule>();
    
    /// <summary>
    /// Creates a new SageKing module and adds the <see cref="SageKingFeature"/> to it.
    /// </summary>
    public static IModule AddSageKing(this IServiceCollection services, Action<IModule>? configure = default)
    {
        var module = services.GetOrCreateModule();
        module.Configure<AppFeature>(app => app.Configurator = configure);
        module.Apply();
        
        return module;
    }

    /// <summary>
    /// Configures the SageKing module.
    /// </summary>
    public static IModule ConfigureSageKing(this IServiceCollection services, Action<IModule>? configure = default)
    {
        var module = services.GetOrCreateModule();
        
        if(configure != null)
            module.Configure<AppFeature>(app => app.Configurator += configure);
        
        return module;
    }
    
    private static IModule GetOrCreateModule(this IServiceCollection services)
    {
        if(Modules.TryGetValue(services, out var module))
            return module;
        
        module = services.CreateModule();
        
        Modules[services] = module;
        return module;
    }

}