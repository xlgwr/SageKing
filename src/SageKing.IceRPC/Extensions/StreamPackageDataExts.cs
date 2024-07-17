using IceRpc;
using SageKingIceRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SageKing.IceRPC.Extensions;

public static class StreamPackageDataExts
{
    public static StreamPackage GetStreamPackage(this StreamPackageData data, int serverNo, int errNo = 0, string errInfo = "")
    {
        return new StreamPackage()
        {
            ServiceNo = serverNo,
            ErrorNo = errNo,
            ErrorInfo = errInfo,
            ClientId = string.Empty,
            MsgId = string.Empty,
            Uuid = string.Empty,
            HeadDic = new Dictionary<string, string>(),
            DataStreamRowType = data.RowType,
            DataStreamBody = data.DataBody
        };
    }
}
