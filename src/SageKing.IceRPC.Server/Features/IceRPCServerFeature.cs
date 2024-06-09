using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SageKing.IceRPC.Server.HostedServices;
using SageKing.MediatR.Features;

namespace SageKing.IceRPC.Server.Features;

public class IceRPCServerFeature : FeatureBase
{
    public IceRPCServerFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// A factory that instantiates an <see cref="IServerProvider"/>.
    /// </summary>
    public Func<IServiceProvider, IServerProvider<ServerAddress>> InstanceServerProvider { get; set; } = sp =>
    {
        return ActivatorUtilities.CreateInstance<IceRPCServerProvider>(sp);
    };

    /// <summary>
    /// Represents the options for IceRPCServer feature.
    /// </summary>
    public Action<IceRPCServerOption> IceRPCServerOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void ConfigureHostedServices()
    {
        Module.ConfigureHostedService<IceRPCServerHostedService>((int)HostedServicePriorityEnum.IceRPCServer);
    }

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(IceRPCServerOptions)
            .AddSingleton(InstanceServerProvider)
            .AddSingleton<ServerReceiver>()
            .AddSingleton<IceRPCServer>();

    }
}
