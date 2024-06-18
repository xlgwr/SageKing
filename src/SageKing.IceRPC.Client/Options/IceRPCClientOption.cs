using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Client.Options;

public record class IceRPCClientOption : IceBaseOptions
{
    public const string SectionName = "IceRPCClients";

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
