using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Database.SqlSugar.Options;

/// <summary>
/// 配置字典
/// </summary>
public class SageKingDatabaseSqlSugarOptions
{
    public const string SectionName = "SageKingDatabaseSqlSugar";

    public DbConnectionOptions DBConnection {  get; set; }
}
