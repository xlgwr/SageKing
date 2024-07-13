using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Extensions;

public static class JsonExts
{
    public static string toJsonStr(this object obj)
    {
        return JsonConvert.SerializeObject(obj);

    }

    public static T? JsonTo<T>(this string obj, object settings = null)
    {
        if (obj.IsNullOrEmpty())
        {
            return default(T);
        }
        return JsonConvert.DeserializeObject<T>(obj, settings as JsonSerializerSettings);
    }


    public static bool HasItem<TSource>(this IEnumerable<TSource> source)
    {

        if (source == null)
        {
            return false;
        }
        else
        {
            return source.Any();
        }
    }
}
