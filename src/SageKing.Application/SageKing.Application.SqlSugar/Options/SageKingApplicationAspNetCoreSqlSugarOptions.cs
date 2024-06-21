using SageKing.Core.Abstractions;

namespace SageKing.Application.AspNetCore.SqlSugar;

/// <summary>
/// 缓存配置
/// </summary>
public sealed class SageKingApplicationAspNetCoreSqlSugarOptions : IOptionsBase
{

    public string SectionName => "SageKingApplicationAspNetCoreSqlSugar";

    public void BindFromConfig(IConfigurationManager configurationManager)
    {
        configurationManager.GetSection(SectionName).Bind(this);
    }
}
