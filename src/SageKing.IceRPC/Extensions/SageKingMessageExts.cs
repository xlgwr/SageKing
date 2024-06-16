using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Extensions
{
    public static class SageKingMessageExts
    {
        public static bool AddOrUpdatePost<T>(this ConcurrentDictionary<string, T> dic, string attributeName, DataStreamTypValue<T> value, ConcurrentDictionary<DataStreamTypeEnum, ConcurrentDictionary<string, int>> posDic)
            where T : struct
        {
            if (dic == null)
            {
                dic = new();
            }

            var type = value.DataStreamType;
            if (!dic.ContainsKey(attributeName))
            {
                if (posDic.TryGetValue(type, out var pos))
                {
                    int count = pos.Count;
                    pos[attributeName] = count;
                }
                else
                {
                    posDic[type] = new();
                    posDic[type][attributeName] = 0;
                }
            }
            dic[attributeName] = value.Value;
            return true;
        }
        public static bool AddOrUpdatePost(this ConcurrentDictionary<string, string> dic, string attributeName, DataStreamTypValue<string> value, ConcurrentDictionary<DataStreamTypeEnum, ConcurrentDictionary<string, int>> posDic)
        {


            var type = value.DataStreamType;
            if (!dic.ContainsKey(attributeName))
            {
                if (posDic.TryGetValue(type, out var pos))
                {
                    int count = pos.Count;
                    pos[attributeName] = count;
                }
                else
                {
                    posDic[type] = new();
                    posDic[type][attributeName] = 0;
                }
            }
            dic[attributeName] = value.Value;
            return true;
        }


        public static bool RemovePost<T>(this ConcurrentDictionary<string, T> dic, string attributeName, DataStreamTypeEnum type, ConcurrentDictionary<DataStreamTypeEnum, ConcurrentDictionary<string, int>> posDic, ref bool isChange)
        {
            if (dic.TryRemove(attributeName, out _))
            {
                if (posDic.TryGetValue(type, out var pos))
                {
                    pos.TryRemove(attributeName, out _);
                }
                isChange = true;
            };
            return true;
        }

        public static void GetPostData<T>(this DataStreamTypValue<T[]> value, ConcurrentDictionary<string, T> dic, ConcurrentDictionary<DataStreamTypeEnum, ConcurrentDictionary<string, int>> posDic)
        {
            
            var getPost = posDic[value.DataStreamType];
            dic = new();
            foreach (var item in getPost)
            {
                dic[item.Key] = value.Value[item.Value];
            }

        }
        public static T[] GetArray<T>(this List<string> sortAttribute, ConcurrentDictionary<string, T> dataDic)
            where T : struct
        {

            List<T> lst = new();
            foreach (var item in sortAttribute)
            {
                lst.Add(dataDic[item]);
            }
            return lst.ToArray();
        }

        public static string[] GetArray(this List<string> sortAttribute, ConcurrentDictionary<string, string> dataDic)
        {
            List<string> lst = new();
            foreach (var item in sortAttribute)
            {
                lst.Add(dataDic[item]);
            }
            return lst.ToArray();
        }

        public static T GetDefault<T>(this IDictionary<string, T> dic, string key, T defaultValue = default(T))
        {
            if (dic.TryGetValue(key, out var value))
            {
                return value;
            }
            return defaultValue;
        }
    }
}
