using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SageKing.Extensions;
using AutoFixture;

namespace SageKing.IceRPC.Test
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder)
        {

        }

        public void Configure()
        {

        }

        public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
        {
            services.AddSageKing(o =>
            {
                o.UseIceRPC();
            });

            services.AddScoped<IFixture, Fixture>();
        }


    }
}
