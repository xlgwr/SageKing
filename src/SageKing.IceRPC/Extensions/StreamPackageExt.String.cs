using Newtonsoft.Json.Linq;
using SageKing.Core.Contracts;
using SageKingIceRpc;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroC.Slice;

namespace SageKing.IceRPC.Extensions
{
    /// <summary>
    /// StreamPackage 的扩展方法.
    /// </summary>
    public static partial class StreamPackageExt
    {
        public static string GetString(this StreamPackage packages)
        {
            return packages.DataStreamBody.DecodeString();
        }

        public static List<string> GetString(this IEnumerable<StreamPackage> packages)
        {
            var result = new List<string>();

            foreach (var package in packages)
            {
                result.Add(package.GetString());
            }

            return result;
        }

        public static IEnumerable<StreamPackage> GetDataStreamBody(this string msg)
        {
            return new StreamPackage[] { msg.GetStreamPackage(DataStreamTypeEnum.String) };
        }

        public static StreamPackage GetStreamPackage(this string msg, DataStreamTypeEnum typeEnum)
        {
            var bytes = msg.EncodeString();
            return new StreamPackage()
            {
                DataStreamBody = new[] { bytes },
                DataStreamRowType = new byte[] { (byte)typeEnum },
                ClientId = string.Empty,
                ErrorInfo = string.Empty,
                ErrorNo = 0,
                HeadDic = new Dictionary<string, string>(),
                MsgId = string.Empty,
                ServiceNo = 0,
                Uuid = string.Empty,
            };
        }
    }
}
