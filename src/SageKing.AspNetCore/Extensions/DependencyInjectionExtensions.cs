using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using SageKing.Features.Contracts;
using System.Text.RegularExpressions;

namespace SageKing.Extensions;

/// <summary>
/// Provides extension methods to <see cref="IServiceCollection"/>. 
/// </summary>
public static class ModuleExtensions
{
    /// <summary>
    /// 启用初始化功能
    /// </summary>
    public static WebApplication UseSageKing(this WebApplication app)
    {
        var Modules = SageKingModuleExtensions.Modules;
        foreach (var item in Modules)
        {
            item.Value.Init();
        }
        return app;
    }
}