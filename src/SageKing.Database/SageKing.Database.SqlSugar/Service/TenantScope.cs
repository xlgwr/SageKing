using Mapster;
using Microsoft.Extensions.Options;
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
    private SageKingDatabaseSqlSugarOptions _options;
    public TenantScope(IOptions<SageKingDatabaseSqlSugarOptions> options) : base(options.Value.DBConnection.ConnectionConfigs.Adapt<List<ConnectionConfig>>(), options.Value.SqlSugarClientConfigAction)
    {
        _options = options.Value;
        InitDB();
    } 
    /// <summary>
    /// 初始化DB
    /// </summary>
    public void InitDB()
    {
        _options.DBConnection.ConnectionConfigs.ForEach(config =>
        {
            _options.InitDatabaseAction?.Invoke(this, config);
        });
    }
}
