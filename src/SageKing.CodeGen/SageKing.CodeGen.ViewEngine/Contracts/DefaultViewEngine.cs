using SageKing.Core.Contracts;

namespace SageKing.CodeGen.SageKingViewEngine;


public class DefaultViewEngine : ViewEngineModel
{

    public DefaultViewEngine()
    {
    }

    public string NameSpace { get; set; }

    /// <summary>
    /// :BaseClassName
    /// </summary>
    public string BaseClassName { get; set; }

    public string ClassName { get; set; }

    public string Description { get; set; }

    public List<KeyValue<string, DataStreamTypeEnum>> ColumnList { get; set; }

    public string LowerClassName
    {
        get
        {
            return ClassName[..1].ToLower() + ClassName[1..]; // 首字母小写
        }
    }
}