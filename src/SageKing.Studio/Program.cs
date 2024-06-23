using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using SageKing.Studio.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//配置 本地 Configuration 目录下json文件
builder.Configuration.AddConfigurationJsonFiles(builder.Environment);

var configuration = builder.Configuration;

//add SageKing
builder.Services.AddSageKing(sk =>
{
    sk.UseIceMediatR(o => o.MediatRServiceConfiguration += a =>
    {
        a.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    });

    sk.UseIceRPC(o => o.ClientTypeDicOptions += options =>
    {
        configuration.Bind(options);
    });

    sk.UseIceRPCServer(o => o.IceRPCServerOptions += options =>
    {
        configuration.Bind(options);
    });

    sk.UseIceRPCClient(o => o.IceRPCClientListOptions += options =>
    {
        configuration.Bind(options);
    });

    //add sqlsugar and database base
    sk.UseSageKingApplicationAspNetCoreSqlSugar(configuration);

});


//builder.Services
//            .AddTransient<SysConfigService>()
//            .AddTransient<SysDictDataService>()
//            .AddTransient<SysDictTypeService>()
//            .AddTransient<SysSageKingMessageService>()
//            .AddTransient<SysSageKingMessageAttributeService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//other services
builder.Services.AddSingleton<PackagesDataService>();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddAntDesign();

var app = builder.Build();

app.UseSageKing();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
