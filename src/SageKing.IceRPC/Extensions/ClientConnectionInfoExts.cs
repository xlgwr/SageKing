using IceRpc;
using SageKing.Core.Contracts;
using SageKingIceRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Extensions
{
    public static class ClientConnectionInfoExts
    {
        public static ClientReceiverProxy GetClientReceiverProxy(this ClientConnectionInfo<IConnectionContext> clientConnectionInfo)
        {
            return new ClientReceiverProxy(clientConnectionInfo.Connection.Invoker);
        }
    }
}
