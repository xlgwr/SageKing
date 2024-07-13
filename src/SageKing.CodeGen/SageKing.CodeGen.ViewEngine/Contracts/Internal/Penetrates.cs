namespace SageKing.CodeGen.SageKingViewEngine;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
public static class Penetrates
{
    /// <summary>
    /// 获取模板文件名
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetTemplateFileName(this string fileName, string ext = ".dll")
    {
        var templateSaveDir = Path.Combine(AppContext.BaseDirectory, "templates");
        if (!Directory.Exists(templateSaveDir)) Directory.CreateDirectory(templateSaveDir);

        if (!fileName.EndsWith(ext, System.StringComparison.OrdinalIgnoreCase)) fileName += ext;
        var isPrefix = ext == ".dll" ? "~" : "";
        var templatePath = Path.Combine(templateSaveDir, isPrefix + fileName);

        return templatePath;
    }

    /// <summary>
    /// 清理模板文件名
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool ClearTemplateFileName(this string fileName)
    {
        try
        {
            var filedll = GetTemplateFileName(fileName);
            if (File.Exists(filedll))
            {

                File.Delete(filedll);
            }

            var filecs = GetTemplateFileName(fileName, ".cs");
            if (File.Exists(filecs))
            {

                File.Delete(filecs);
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;

    }

    /// <summary>
    /// 清理模板文件 整个目录 templates
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool ClearTemplateFileName()
    {
        var templateSaveDir = Path.Combine(AppContext.BaseDirectory, "templates");
        DirectoryInfo di = new DirectoryInfo(templateSaveDir);
        di.Delete(true);
        return true;
    }
    /// <summary>
    /// 清理模板文件 整个目录 templates
    /// </summary>
    /// <returns></returns>
    public static Task<bool> ClearTemplateFileNameAsync()
    {
        return Task<bool>.Factory.StartNew(() => { return ClearTemplateFileName(); });
    }

}