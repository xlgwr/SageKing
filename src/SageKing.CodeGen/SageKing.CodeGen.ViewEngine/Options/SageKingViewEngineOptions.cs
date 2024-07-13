using Microsoft.CodeAnalysis;
using SageKing.CodeGen.SageKingViewEngine;
using SageKing.Core.Abstractions;
using System.Reflection;

namespace SageKing.CodeGen.SageKingViewEngine;

/// <summary>
/// 配置
/// </summary>
public sealed class SageKingViewEngineOptions : IOptionsBase
{

    public string SectionName => "SageKingViewEngine";

    /// <summary>
    /// 构造函数
    /// </summary>
    public SageKingViewEngineOptions()
    {
        ReferencedAssemblies = new HashSet<Assembly>()
        {
            typeof(object).Assembly,
            typeof(ViewEngineModel).Assembly,
            typeof(System.Collections.IList).Assembly,
            typeof(IEnumerable<>).Assembly,
            Reflect.GetAssembly("Microsoft.CSharp"),
            Reflect.GetAssembly("System.Runtime"),
            Reflect.GetAssembly("System.Linq"),
            Reflect.GetAssembly("System.Linq.Expressions"),
            Reflect.GetAssembly("System.Collections")
        };
    }

    /// <summary>
    /// 引用程序集
    /// </summary>
    public HashSet<Assembly> ReferencedAssemblies { get; set; }

    /// <summary>
    /// 元数据引用
    /// </summary>
    public HashSet<MetadataReference> MetadataReferences { get; set; } = new HashSet<MetadataReference>();

    /// <summary>
    /// 模板命名空间
    /// </summary>
    public string TemplateNamespace { get; set; } = "SageKing.CodeGen.SageKingViewEngine";

    /// <summary>
    /// 继承
    /// </summary>
    public string Inherits { get; set; } = "SageKing.CodeGen.SageKingViewEngine.Template.Models";

    /// <summary>
    /// 默认 Using
    /// </summary>
    public HashSet<string> DefaultUsings { get; set; } = new HashSet<string>()
        {
            "System.Linq",
            "System.Collections",
            "System.Collections.Generic"
        };
}
