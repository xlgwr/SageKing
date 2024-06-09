using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Client.Features;

public class IceRPCClientFeature : FeatureBase
{
    public IceRPCClientFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// A factory that instantiates an <see cref="IClientConnectionProvider"/>.
    /// </summary>
    public Func<IServiceProvider, IClientConnectionProvider<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage>> InstanceClientProvider { get; set; } = sp =>
    {
        return ActivatorUtilities.CreateInstance<IceRPCClientProvider>(sp);
    };

    /// <summary>
    /// Represents the options for IceRPCClients feature.
    /// </summary>
    public Action<List<IceRPCClientOption>> IceRPCClientOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void ConfigureHostedServices()
    {
        Module.ConfigureHostedService<IceRPCClientHostedService>((int)HostedServicePriorityEnum.IceRPCClient);
    }

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(IceRPCClientOptions)
            .AddSingleton(InstanceClientProvider)
            .AddScoped<IceRPCClient>();
    }
}
