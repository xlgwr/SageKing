// dotnet run -c Release -f net8.0 --filter "*" --runtimes net8.0

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SageKing.Core.Contracts;
using SageKing.IceRPC.Contracts;
using System;
using System.Text;
using System.Text.Json;

[HideColumns("Error", "StdDev", "Median", "RatioSD")]
[DisassemblyDiagnoser(maxDepth: 0)]
public class Tests
{

    private SageKingMessage data1;
    private EntityDemo data2;
    private byte[] data2Byte;
    private StreamPackageData data1byte;
    private int _factor = 2;

    [GlobalSetup]
    public void Setup()
    {
        data1 = GetSageKingMessage(3);
        data1byte = data1.ToData();
        data2 = new();
        string json = JsonSerializer.Serialize(data2);
        data2Byte = JsonToByte(json);
    }

    [Benchmark]
    public EntityDemo GetEntityDemo() => new EntityDemo();

    [Benchmark]
    public SageKingMessage GetSageKingMessage() => GetSageKingMessage(3);

    [Benchmark]
    public void GetEntityDemoFromBype()
    {
        var json = ByteToString(data2Byte);
        var result = JsonSerializer.Deserialize<EntityDemo>(json);
    }

    [Benchmark]
    public void GetSageKingMessageFromBype()
    {
        data1.LoadData(data1byte);
    }

    [Benchmark]
    public void GetEntityDemoTextJsonToBype()
    {
        string json = JsonSerializer.Serialize(data2);
        var result = JsonToByte(json);
    }

    [Benchmark]
    public void GetSageKingMessageToBype()
    {
        data1.ToData(true);
    } 

    public byte[] JsonToByte(string json)
    {
        var jsonBytes = Encoding.UTF8.GetBytes(json);
        return jsonBytes;
    }
    public string ByteToString(byte[] bytes)
    {
        var jsonBytes = Encoding.UTF8.GetString(bytes);
        return jsonBytes;
    }
    /// <summary>
    /// 生成 3* 6 = 18 个 属性对象
    /// </summary>
    /// <param name="sumAttr"></param>
    /// <returns></returns>
    public SageKingMessage GetSageKingMessage(int sumAttr = 3)
    {
        var sageKingMessage = new SageKingMessage();
        //Arrange 
        var attributename = "attributename";

        int step = 0;
        for (int i = 0; i < sumAttr; i++)
        {
            var valuestring = new DataStreamTypValue<string>(DataStreamTypeEnum.StringArr, "valuestring");
            var valueInt = new DataStreamTypValue<int>(DataStreamTypeEnum.Int32Arr, 110);
            var valuebyte = new DataStreamTypValue<byte>(DataStreamTypeEnum.Uint8Arr, 13);
            var valuefloat = new DataStreamTypValue<float>(DataStreamTypeEnum.Float32Arr, 110.20f);
            var valuelong = new DataStreamTypValue<long>(DataStreamTypeEnum.Int64Arr, 110110110);
            var valuedouble = new DataStreamTypValue<double>(DataStreamTypeEnum.Float64Arr, 110.110);

            //Act
            sageKingMessage.AddOrUpdate($"{attributename}_{step}", valuestring);
            sageKingMessage.AddOrUpdate($"{attributename}_{step + 1}", valueInt);
            sageKingMessage.AddOrUpdate($"{attributename}_{step + 2}", valuebyte);
            sageKingMessage.AddOrUpdate($"{attributename}_{step + 3}", valuefloat);
            sageKingMessage.AddOrUpdate($"{attributename}_{step + 4}", valuelong);
            sageKingMessage.AddOrUpdate($"{attributename}_{step + 5}", valuedouble);

            step += 6;
        }
        return sageKingMessage;
    }

    public class EntityDemo
    {
        public string name1 { get; set; } = "valuestring";
        public string name2 { get; set; } = "valuestring";
        public string name3 { get; set; } = "valuestring";

        public int name4 { get; set; } = 110;
        public int name5 { get; set; } = 110;
        public int name6 { get; set; } = 11;

        public byte name7 { get; set; } = 13;
        public byte name8 { get; set; } = 13;
        public byte name9 { get; set; } = 1;

        public float name10 { get; set; } = 110.20f;
        public float name11 { get; set; } = 110.20f;
        public float name12 { get; set; } = 110.20f;

        public long name13 { get; set; } = 110110110;
        public long name14 { get; set; } = 110110110;
        public long name15 { get; set; } = 11011011;

        public double name16 { get; set; } = 110.110;
        public double name17 { get; set; } = 110.110;
        public double name18 { get; set; } = 110.11;


        private bool _isChange = false;
        private StreamPackageData _packageData;

        #region 数据 AttributeValue

        /// <summary>
        /// 属性值 
        /// </summary>
        private Dictionary<string, string> _string0;
        private Dictionary<string, sbyte> _int8Arr_sbyte1;
        private Dictionary<string, byte> _uint8Arr_byte2;
        private Dictionary<string, short> _int16_short3;
        private Dictionary<string, ushort> _uint16_ushort4;
        private Dictionary<string, int> _int32_int5;
        private Dictionary<string, uint> _uint32_uint6;
        private Dictionary<string, long> _int64_long7;
        private Dictionary<string, ulong> _uint64_ulong8;
        private Dictionary<string, float> _float32_float9;
        private Dictionary<string, double> _float64_double10;
        #endregion
        /// <summary>
        /// 属性位置
        /// 用于解包定位
        /// </summary>
        private readonly Dictionary<DataStreamTypeEnum, Dictionary<string, int>> _attributePosition;

        public EntityDemo()
        {
            _isChange = false;
            _attributePosition = new();
        }

        public string Id { get; set; }

        public string Varsion { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public string Description { get; set; }

    }


}