using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Options;

public record class IceBaseOptions
{

    /// <summary>
    /// 服务器名称
    /// </summary>
    public string ServerName { get; set; } = string.Empty;

    /// <summary>
    /// Server address
    /// protocol://host[:port][?name=value][&name=value...]
    /// default icerpc port (4062)
    /// 例子：icerpc://127.0.0.1:4062
    /// https://docs.icerpc.dev/icerpc/connection/server-address
    /// </summary>
    public string ServerAddress { get; set; }

    /// <summary>
    /// 是否开启 Quic
    /// </summary>
    public bool IsQuic { get; set; } = false;

    /// <summary>
    /// 是否开启 TCP传输 TLS
    /// 与Quic不同，TCP可以不开启TLS
    /// </summary>
    public bool IsTcpTLS { get; set; } = false;

    /// <summary>
    /// 是否开启压缩
    /// </summary>
    public bool UseCompressor { get; set; } = false;

    public bool UseRequestContext { get; set; } = false;

    public bool UseMetrics { get; set; } = false;

    /// <summary>
    /// 客户端证书文件路径
    /// root CA certificate
    /// 开启TLS,或Quic，必需配置当前值
    /// sslServerAuthenticationOptions
    /// </summary>
    public string CertificateFileName { get; set; }
}
