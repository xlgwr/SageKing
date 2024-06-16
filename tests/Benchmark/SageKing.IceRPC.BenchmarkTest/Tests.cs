// dotnet run -c Release -f net8.0 --filter "*" --runtimes net8.0

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running; 

[HideColumns("Error", "StdDev", "Median", "RatioSD")]
[DisassemblyDiagnoser(maxDepth: 0)]
public class Tests
{
    internal interface IValueProducer
    {
        int GetValue();
    }

    class Producer42 : IValueProducer
    {
        public int GetValue() => 42;
    }

    private IValueProducer _valueProducer;
    private int _factor = 2;

    [GlobalSetup]
    public void Setup() => _valueProducer = new Producer42();

    [Benchmark]
    public int GetValue() => _valueProducer.GetValue() * _factor;
}