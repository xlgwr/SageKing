using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Service;

/// <summary>
/// Newtonsoft.Json
/// </summary>
public class JsonSerializerDefaultProvider : IJsonSerializerProvider
{
    private JsonSerializerSettings _jsonSerializerOptions ;


    public T Deserialize<T>(string json, object jsonSerializerOptions = null)
    {
        if (jsonSerializerOptions is JsonSerializerSettings dd && dd != null)
        {
            _jsonSerializerOptions ??= dd;
            return JsonConvert.DeserializeObject<T>(json, dd);
        }
        return JsonConvert.DeserializeObject<T>(json);
    }

    public object Deserialize(string json, Type returnType, object jsonSerializerOptions = null)
    {
        if (jsonSerializerOptions is JsonSerializerSettings dd && dd != null)
        {
            _jsonSerializerOptions ??= dd;
            return JsonConvert.DeserializeObject(json, dd);
        }
        return JsonConvert.DeserializeObject(json);
    }

    public object GetSerializerOptions()
    {
        return _jsonSerializerOptions;
    }

    public string Serialize(object value, object jsonSerializerOptions = null)
    {
        if (jsonSerializerOptions is JsonSerializerSettings dd && dd != null)
        {
            return JsonConvert.SerializeObject(value, dd);
        }
        return JsonConvert.SerializeObject(value);
    }
}
