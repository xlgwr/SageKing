global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;

global using System.Collections.Concurrent;

global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;

global using JetBrains.Annotations;
global using SageKing.Extensions;
global using SageKing.Features.Attributes;
global using SageKing.Features.Contracts;
global using SageKing.Features.Models;
global using SageKing.Features.Implementations;
global using SageKing.Features.Abstractions;

global using SageKing.Core.Contracts;
global using SageKing.Core.EventMessage;


global using IceRpc;
global using MediatR;

global using SageKingIceRpc;
global using SageKing.IceRPC;
global using SageKing.IceRPC.Contracts;
global using SageKing.IceRPC.Extensions;

global using SageKing.IceRPC.EventMessage;

global using SageKing.IceRPC.Client.Options;
global using SageKing.IceRPC.Client.Services;
global using SageKing.IceRPC.Client.HostedServices;

global using SageKing.IceRPC.Server.Options;
global using SageKing.IceRPC.Server.Services;
global using SageKing.IceRPC.Server.Services.SliceService;
