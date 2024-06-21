using Microsoft.Extensions.Configuration;
using SageKing.Core.Abstractions;
using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Server.Options;

public record class IceRPCServerOption : IceBaseOptions, IOptionsBase
{
    public IceRPCServerOption()
    {
        this.ServerAddress = "icerpc://[::0]:4062";
        this.CertificateFileName = "certs/server.p12";
    }

    /// <summary>
    /// 服务端类型
    /// </summary>
    public int ServerType { get; set; }

   public string SectionName => "IceRPCServer";

    public void BindFromConfig(IConfigurationManager configurationManager)
    {
        configurationManager.GetSection(SectionName).Bind(this);
    }
}
