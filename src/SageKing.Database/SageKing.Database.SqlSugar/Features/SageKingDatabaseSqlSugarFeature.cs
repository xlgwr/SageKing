using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.Features;

public class SageKingDatabaseSqlSugarFeature : FeatureBase
{
    public SageKingDatabaseSqlSugarFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// Represents the options for SageKingDatabaseSqlSugars feature.
    /// </summary>
    public Action<SageKingDatabaseSqlSugarOptions> ClientTypeDicOptions { get; set; } = _ => { };

    /// <inheritdoc />
    public override void Apply()
    {
        Services.Configure(ClientTypeDicOptions);
    }
}
