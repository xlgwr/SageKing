using SageKing.MediatR.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseIceMediatR(this IModule module, Action<MediatRFeature>? configure = default)
    {
        module.Configure<MediatRFeature>(feature =>
        {
            configure?.Invoke(feature);
        });
        return module;
    }
}
