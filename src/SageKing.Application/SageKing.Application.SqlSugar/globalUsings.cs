global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;

global using System.ComponentModel.DataAnnotations;
global using Microsoft.Extensions.Options;

global using JetBrains.Annotations;
global using SageKing.Extensions;
global using SageKing.Features.Attributes;
global using SageKing.Features.Contracts;
global using SageKing.Features.Models;
global using SageKing.Features.Implementations;
global using SageKing.Features.Abstractions;

global using SageKing.Core.Options;
global using SageKing.Core.Contracts;
global using SageKing.Core.Attributes;

global using System.Collections.Concurrent;

global using SqlSugar;
global using MediatR;

global using NewLife.Caching;
global using NewLife.Caching.Models;
global using CacheDefault = NewLife.Caching.Cache;

global using SageKing.Database;
global using SageKing.Database.Contracts;
global using SageKing.Database.SqlSugar.Contracts;
global using SageKing.Database.SqlSugar.Service;

global using SageKing.Application.AspNetCore.SqlSugar.Contracts;
global using SageKing.Application.AspNetCore.SqlSugar.Contracts.Entity;

global using SageKing.Database.SqlSugar.AspNetCore.Extensions;