﻿using SageKing.IceRPC.Server.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Extensions;

public static class ModuleExtensions
{
    public static IModule UseIceRPCServer(this IModule module, Action<IceRPCServerFeature>? configure = default)
    {
        module.Configure<IceRPCServerFeature>(feature =>
        {
            configure?.Invoke(feature);
        });
        module.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
        {
            a.RegisterServicesFromAssembly(typeof(IceRPCServerFeature).Assembly);
        });

        //module.UseIceRPC(o => o.ClientTypeDicOptions += a =>
        //{
        //    a.TryAdd(110, "测试服务110");
        //});
        return module;
    }

    public static IModule UseIceRPCServer(this IModule module, string serverAddress, bool isQuic = true, string sslFileName = "certs/server.p12")
    {
        module.UseIceRPCServer(ice => ice.IceRPCServerOptions = o =>
        {
            o.ServerAddress = serverAddress;
            o.IsQuic = isQuic;
            o.CertificateFileName = sslFileName;
        });
        return module;
    }
}
