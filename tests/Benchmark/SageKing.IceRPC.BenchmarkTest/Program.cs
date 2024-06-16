// See https://aka.ms/new-console-template for more information
// See https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
 
Console.WriteLine("Hello, World! SageKing.IceRPC.BenchmarkTest"); 

var config = DefaultConfig.Instance;

BenchmarkSwitcher.FromAssembly(typeof(Tests).Assembly).Run(args, config);

BenchmarkSwitcher.FromAssembly(typeof(Tests2).Assembly).Run(args, config);