using Microsoft.Extensions.Configuration;
using SageKing.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ConfigurationManagerExts
{
    public static void Bind(this IConfigurationManager configurationManager, IOptionsBase options)
    {
        configurationManager.GetSection(options.SectionName).Bind(options);
    }
}
