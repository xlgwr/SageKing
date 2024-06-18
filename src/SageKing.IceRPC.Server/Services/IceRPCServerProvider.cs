using IceRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Server.Services
{
    public class IceRPCServerProvider(IceRPCServer iceRPCServer) : IServerProvider<ServerAddress, StreamPackage>
    {
        public IServer<ServerAddress, StreamPackage> GetServer()
        {
            return iceRPCServer;
        }
    }
}
