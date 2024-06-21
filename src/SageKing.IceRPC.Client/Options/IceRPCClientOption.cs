using Microsoft.Extensions.Configuration;
using SageKing.Core.Abstractions;
using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Client.Options;

public record class IceRPCClientOption : IceBaseOptions
{
    public IceRPCClientOption()
    {
        this.ServerAddress = "icerpc://localhost";
        this.CertificateFileName = "certs/cacert.der";
    } 

    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// see <ref>ClientType</ref>
    /// </summary>
    public int ClientType { get; set; } 

    /// <summary>
    /// 超时时间 秒
    /// UseDeadline Timeout
    /// </summary>
    public int Timeout { get; set; } = 33;
}

public class IceRPCClientListOption : List<IceRPCClientOption>, IOptionsBase
{ 
    public string SectionName => "IceRPCClientList";

    public void BindFromConfig(IConfigurationManager configurationManager)
    {
        configurationManager.GetSection(SectionName).Bind(this);
    }
}
