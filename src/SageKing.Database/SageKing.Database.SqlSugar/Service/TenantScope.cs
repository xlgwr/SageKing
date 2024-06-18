using Mapster;
using Microsoft.Extensions.Options;
using NewLife.Serialization;
using SageKing.Database.SqlSugar.Contracts;
using SageKing.Database.SqlSugar.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.Service;

public class TenantScope : SqlSugarScope
{
    public TenantScope(IOptions<SageKingDatabaseSqlSugarOptions> options,
        TenantScope tenantScope,
        ISqlSugarFilter sqlSugarFilter) : base(options.Value.DBConnection.ConnectionConfigs.Adapt<List<ConnectionConfig>>(), tenantScope.configAction)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public Action<SqlSugarClient> configAction;
}
