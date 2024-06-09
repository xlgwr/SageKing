using IceRpc.Features;
using IceRpc.Slice;
using MyClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Server.Services.SliceService;

/// <summary>A Chatbot is an IceRPC service that implements Slice interface 'Greeter'.</summary>
[SliceService]
internal partial class Chatbot : IGreeterService
{
    public ValueTask<string> GreetAsync(string name, IFeatureCollection features, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Dispatching greet request {{ name = '{name}' }}");
        return new($"Hello, {name}!");
    }
}
