using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Server.Options;

public class IceRPCServerOption
{
    /// <summary>
    /// Server address
    /// default icerpc port (4062)
    /// 例子：icerpc://192.168.100.10:7520
    /// https://docs.icerpc.dev/icerpc/connection/server-address
    /// </summary>
    public string ServerAddress { get; set; }= "icerpc://[::0]";


    /// <summary>
    /// 是否开启 Quic
    /// </summary>
    public bool IsQuic { get; set; } = true;

    /// <summary>
    /// 是否开启 TCP传输 TLS
    /// 与Quic不同，TCP可以不开启TLS
    /// </summary>
    public bool IsTcpTLS { get; set; } = true;

    /// <summary>
    /// 服务证书文件路径
    /// 开启TLS,或Quic，必需配置当前值
    /// sslServerAuthenticationOptions
    /// </summary>
    public string ServerCertificateFileName { get; set; } = "certs/server.p12";
}
