using IceRpc;
using SageKingIceRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Extensions
{
    public static class PipelineExts
    {
        public static ServerReceiverProxy GetServerReceiverProxy(this Pipeline pipeline)
        {
            return new ServerReceiverProxy(pipeline);
        }
    }
}
