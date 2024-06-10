using SageKing.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Server.Options;

public record class IceRPCServerOption : IceBaseOptions
{
    public const string SectionName = "IceRPCServer";

    public IceRPCServerOption()
    {
        this.ServerAddress = "icerpc://[::0]:4062";
        this.CertificateFileName = "certs/server.p12";
    }

}
