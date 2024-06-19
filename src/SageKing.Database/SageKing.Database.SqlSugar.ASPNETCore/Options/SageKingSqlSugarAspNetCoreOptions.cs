namespace SageKing.Database.SqlSugar.AspNetCore;

/// <summary>
/// 配置
/// </summary>
public sealed class SageKingSqlSugarAspNetCoreOptions
{
    public const string SectionName = "SageKingSqlSugarAspNetCore";

    /// <summary>
    /// 是否注入 MiniProfiler
    /// </summary>
    public bool InjectMiniProfiler { get; set; }
}
