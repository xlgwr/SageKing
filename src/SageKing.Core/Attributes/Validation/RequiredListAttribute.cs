using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace SageKing.Core.Attributes;

/// <summary>
/// 官方支持的验证特性
/// https://learn.microsoft.com/zh-cn/dotnet/api/system.componentmodel.dataannotations?view=net-8.0
/// </summary>
public class RequiredListAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return false;
        } 

        //list
        var getCount = value.GetType().GetProperty("Count");
        if (getCount != null && (int)getCount.GetValue(value) <= 0)
        {
            return false;
        }

        //array[]
        var getLengthProperty = value.GetType().GetProperty("Length");
        if (getLengthProperty != null && (int)getLengthProperty.GetValue(value) <= 0)
        {
            return false;
        }

        return true;
    }

}
