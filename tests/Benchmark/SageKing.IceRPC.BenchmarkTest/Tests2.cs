// dotnet run -c Release -f net8.0 --filter "*"

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;

[HideColumns("Error", "StdDev", "Median", "RatioSD")]
[DisassemblyDiagnoser]
public class Tests2
{
    private readonly Func<int, int> _func = i => i + 1;

    [Benchmark]
    public int Sum() => Sum(_func);

    private static int Sum(Func<int, int> func)
    {
        int sum = 0;
        for (int i = 0; i < 10_000; i++)
        {
            sum += func(i);
        }

        return sum;
    }
}