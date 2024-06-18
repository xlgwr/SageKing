using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.MediatR.Features;

public class MediatRFeature : FeatureBase
{
    public MediatRFeature(IModule module) : base(module)
    {
    } 

    /// <summary>
    /// RegisterServicesFromAssemblies
    /// </summary>
    public Action<MediatRServiceConfiguration> MediatRServiceConfiguration { get; set; } = _ => { }; 

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(MediatRServiceConfiguration)
            .AddMediatR(MediatRServiceConfiguration);
    }
}
