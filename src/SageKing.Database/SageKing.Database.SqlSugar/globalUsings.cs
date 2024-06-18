global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using System.Collections;
global using System.Collections.Concurrent;

global using Microsoft.Extensions.Logging;

global using System.Linq.Expressions;

global using JetBrains.Annotations;
global using SageKing.Extensions;
global using SageKing.Features.Attributes;
global using SageKing.Features.Contracts;
global using SageKing.Features.Models;
global using SageKing.Features.Implementations;
global using SageKing.Features.Abstractions;

global using SageKing.Core.Contracts;
global using SageKing.Database.Contracts;
global using SageKing.Database.SqlSugar.Contracts;


global using MediatR;
global using SqlSugar;
global using NewLife.Caching;
global using NewLife.Caching.Models;
global using CacheDefault = NewLife.Caching.Cache;
global using SageKing.Cache.Service;