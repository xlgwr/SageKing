using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

public record KeyValue<K, V>(K Key, V Value);

public class KeyValueClass<K, V>()
{
    public K Key { get; set; }
    public V Value { get; set; }
}

public class MessageModelForm
{
    public List<KeyValueClass<string, string>> AttStrLst { get; set; }
    public List<KeyValueClass<string, sbyte>> AttsbyteLst { get; set; }
    public List<KeyValueClass<string, byte>> AttbyteLst { get; set; }
    public List<KeyValueClass<string, short>> AttshortLst { get; set; }
    public List<KeyValueClass<string, ushort>> AttushortLst { get; set; }
    public List<KeyValueClass<string, int>> AttintLst { get; set; }
    public List<KeyValueClass<string, uint>> AttuintLst { get; set; }
    public List<KeyValueClass<string, long>> AttlongLst { get; set; }
    public List<KeyValueClass<string, ulong>> AttulongLst { get; set; }
    public List<KeyValueClass<string, float>> AttfloatLst { get; set; }
    public List<KeyValueClass<string, double>> AttdoubleLst { get; set; }
}
