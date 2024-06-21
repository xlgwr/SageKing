global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;

global using Microsoft.Extensions.Options;

global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Http;

global using JetBrains.Annotations;
global using SageKing.Extensions;
global using SageKing.Features.Attributes;
global using SageKing.Features.Contracts;
global using SageKing.Features.Models;
global using SageKing.Features.Implementations;
global using SageKing.Features.Abstractions;

global using SageKing.Core.Contracts;
global using SageKing.Core.Options;

global using System.Collections.Concurrent;

global using MediatR;
global using SqlSugar;

global using SageKing.Database.SqlSugar.AspNetCore.Contracts;
global using SageKing.Database.SqlSugar.AspNetCore.Features;

global using SageKing.Database.SqlSugar.AspNetCore.Service;
global using SageKing.Database.SqlSugar.Features;