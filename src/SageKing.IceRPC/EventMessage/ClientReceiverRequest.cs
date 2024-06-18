using SageKingIceRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.EventMessage;

public record ClientReceiverRequest(StreamPackage[] Packages, string msgType, string ClientName, int ClientType) : IRequest<StreamPackage>;
