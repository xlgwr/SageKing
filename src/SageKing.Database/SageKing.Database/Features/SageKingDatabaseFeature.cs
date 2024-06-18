using Microsoft.Extensions.DependencyInjection;
using SageKing.Database.Contracts;
using SageKing.Database.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.Features;

public class SageKingDatabaseFeature : FeatureBase
{
    public SageKingDatabaseFeature(IModule module) : base(module)
    {
    } 

    /// <summary>
    /// Represents the options for SageKingDatabases feature.
    /// </summary>
    public Action<SageKingDatabaseOptions> DatabaseOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(DatabaseOptions)
            .AddSingleton<IIdGenerator, SnowIdGenerator>();
    }
}
