using MediatR;
using SageKingIceRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.EventMessage;

public record ServerReceiverRequest(StreamPackage[] Packages, string msgType) : IRequest<StreamPackage>;
