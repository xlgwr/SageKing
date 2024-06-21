using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Abstractions;

public interface IOptionsBase
{
    public string SectionName { get; }

    /// <summary>
    ///  configurationManager.GetSection(SectionName).Bind(this);
    /// </summary>
    /// <param name="configurationManager"></param>
    public void BindFromConfig(IConfigurationManager configurationManager);
}
