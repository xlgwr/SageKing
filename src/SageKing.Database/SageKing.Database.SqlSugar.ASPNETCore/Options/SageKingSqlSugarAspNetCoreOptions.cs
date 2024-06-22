﻿using SageKing.Core.Abstractions;

namespace SageKing.Database.SqlSugar.AspNetCore;

/// <summary>
/// 配置
/// </summary>
public sealed class SageKingSqlSugarAspNetCoreOptions:IOptionsBase
{ 

    public string SectionName => "SageKingSqlSugarAspNetCore";

    public void BindFromConfig(IConfigurationManager configurationManager)
    {
        configurationManager.GetSection(SectionName).Bind(this);
    }
}