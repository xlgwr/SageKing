using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 启动优先级
/// </summary>
public enum HostedServicePriorityEnum
{
    None = 0,
    IceRPCServer = 1,
    IceRPCClient = 2
}
