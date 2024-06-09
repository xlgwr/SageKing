using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SageKing.IceRPC.Client.Options;
using SageKing.IceRPC.Server.Options;
using SageKing.Studio.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//add SageKing
builder.Services.AddSageKing(sageking =>
{
    sageking.UseIceRPCServer(o => o.IceRPCServerOptions += options =>
    {
        configuration.GetSection(IceRPCServerOption.SectionName).Bind(options);
    });

    sageking.UseIceRPCClient(o => o.IceRPCClientOptions += options =>
    {
        configuration.GetSection(IceRPCClientOption.SectionName).Bind(options);
    });

});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

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
