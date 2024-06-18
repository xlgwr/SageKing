using SageKing.IceRPC.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Features;

public class IceRPCFeature : FeatureBase
{
    public IceRPCFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// Represents the options for IceRPCs feature.
    /// </summary>
    public Action<ClientTypeDicOptions> ClientTypeDicOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(ClientTypeDicOptions)
            .AddTransient<ISageKingMessage, SageKingMessage>();
    }
}
