global using System.Collections.Concurrent;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.Extensions.DependencyInjection;

global using SageKing.Core.Contracts;
global using SageKing.Core.Attributes;
global using SageKing.Core.Extensions;
global using SageKing.Core.EventMessage;

global using IceRpc;
global using MediatR;
global using SageKing.IceRPC;
global using SageKing.Extensions;
global using SageKing.Studio.Data;
global using SageKing.Studio.Validations;
global using SageKing.IceRPC.Client.Options;
global using SageKing.IceRPC.Server.Options;

global using SageKing.Application.AspNetCore.SqlSugar.Contracts;
global using SageKing.Application.AspNetCore.SqlSugar.Contracts.Entity;
global using SageKing.Application.AspNetCore.SqlSugar.Service;