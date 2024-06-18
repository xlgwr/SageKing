using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Extensions
{
    public static class SageKingMessageExts
    {
        public static bool AddOrUpdatePost<T>(this Dictionary<string, T> dic, string attributeName, DataStreamTypValue<T> value, Dictionary<DataStreamTypeEnum, Dictionary<string, byte>> posDic)
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
                    var count = pos.Count;
                    pos[attributeName] = (byte)count;
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
        public static bool AddOrUpdatePost(this Dictionary<string, string> dic, string attributeName, DataStreamTypValue<string> value, Dictionary<DataStreamTypeEnum, Dictionary<string, byte>> posDic)
        {
            var type = value.DataStreamType;
            if (!dic.ContainsKey(attributeName))
            {
                if (posDic.TryGetValue(type, out var pos))
                {
                    int count = pos.Count;
                    pos[attributeName] = (byte)count;
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

        public static bool RemovePost<T>(this Dictionary<string, T> dic, string attributeName, DataStreamTypeEnum type, Dictionary<DataStreamTypeEnum, Dictionary<string, byte>> posDic, ref bool isChange)
        {
            if (dic.Remove(attributeName, out _))
            {
                if (posDic.TryGetValue(type, out var pos))
                {
                    pos.Remove(attributeName, out _);
                }
                isChange = true;
            }
            return true;
        }

        public static void GetPostData<T>(this DataStreamTypValue<T[]> value, Dictionary<string, T> dic, Dictionary<DataStreamTypeEnum, Dictionary<string, byte>> posDic)
        {
            dic.Clear();
            var getPost = posDic[value.DataStreamType];
            foreach (var item in getPost)
            {
                dic[item.Key] = value.Value[item.Value];
            }

        }

        public static T[] GetArray<T>(this List<string> sortAttribute, Dictionary<string, T> dataDic)
            where T : struct
        {
            T[] lst = new T[sortAttribute.Count];
            int i = 0;
            foreach (var item in sortAttribute)
            {
                lst[i] = dataDic[item];
                i++;
            }
            return lst;
        }

        public static byte[] GetArray(this List<string> sortAttribute, Dictionary<string, string> dataDic)
        {
            string[] lst = new string[sortAttribute.Count];
            int i = 0;
            foreach (var item in sortAttribute)
            {
                lst[i] = dataDic[item];
                i++;
            }
            return lst.ToIceByte();
        }

        public static T GetDefault<T>(this Dictionary<string, T> dic, string key, T defaultValue = default!)
        {
            if (dic.TryGetValue(key, out var value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 用当前值，初始化目标值
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="d"></param>
        /// <param name="targetDic"></param>
        /// <returns></returns>
        public static bool InitDictionary<A, B, C>(this Dictionary<A, Dictionary<B, C>> d, Dictionary<A, Dictionary<B, C>> targetDic)
        {
            if (!d.Any())
            {
                return false;
            }
            targetDic.Clear();
            foreach (var item in d)
            {
                var posItem = new Dictionary<B, C>();

                foreach (var child in item.Value)
                {
                    posItem[child.Key] = child.Value;
                }
                targetDic[item.Key] = posItem;
            }
            return true;
        }

        public static Dictionary<A, Dictionary<B, C>> GetDictionary<A, B, C>(this Dictionary<A, Dictionary<B, C>> d)
        {
            if (!d.Any())
            {
                return new();
            }
            var toDic = new Dictionary<A, Dictionary<B, C>>();
            foreach (var item in d)
            {
                var posItem = new Dictionary<B, C>();

                foreach (var child in item.Value)
                {
                    posItem[child.Key] = child.Value;
                }
                toDic[item.Key] = posItem;
            }
            return toDic;
        }
    }
}
