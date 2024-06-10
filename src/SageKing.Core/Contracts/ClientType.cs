﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

public enum ClientType
{
    UNKNOW = 0,
    /// <summary>
    /// 交易客户端
    /// </summary>
    [Description("交易客户端")]
    TRADE_CLIENT = 1,
    /// <summary>
    /// 风控客户端
    /// </summary>
    [Description("风控客户端")]
    RISK_CLIENT = 2,
    /// <summary>
    /// 柜台客户端
    /// </summary>
    [Description("柜台客户端")]
    CONTER_CLIENT = 3,
    /// <summary>
    /// 交易api
    /// </summary>
    [Description("交易api")]
    TRADE_API = 4,
    /// <summary>
    /// 交易前置
    /// </summary>
    [Description("交易前置")]
    TRADE_FRONT = 5,
    /// <summary>
    /// 交易服务器
    /// </summary>
    [Description("交易服务器")]
    TRADE_SERVER = 6,
    /// <summary>
    /// 交易网关
    /// </summary>
    [Description("交易网关")]
    TRADE_GATEWAY = 7,
    /// <summary>
    /// 行情服务器
    /// </summary>
    [Description("行情服务器")]
    QUOT_SERVER = 8,
    /// <summary>
    /// 行情汇聚
    /// </summary>
    [Description("行情汇聚")]
    QUOT_CONVERGE = 9,
    /// <summary>
    /// 行情网关
    /// </summary>
    [Description("行情网关")]
    QUOT_GATEWAY = 10,
    /// <summary>
    /// 管理前置
    /// </summary>
    [Description("管理前置")]

    CONTER_FRONT = 11,
    /// <summary>
    /// 结算前置
    /// </summary>
    [Description("结算前置")]
    SETTLE_FRONT = 12,
    /// <summary>
    /// 强平客户端
    /// </summary>
    [Description("强平客户端")]
    FORCE_CLOSE_CLIENT = 13,
    /// <summary>
    /// 结算客户端
    /// </summary>
    [Description("结算客户端")]
    SETTLE_CLIENT = 14,
    /// <summary>
    /// 监控服务
    /// </summary>
    [Description("监控服务")]
    MONITOR = 15,
    /// <summary>
    /// 认证服务
    /// </summary>
    [Description("认证服务")]
    CONFIRM_SERVER = 16,
    /// <summary>
    /// 消息服务
    /// </summary>
    [Description("消息服务")]
    NEWS_SERVER = 17,
    /// <summary>
    /// 历史查询服务
    /// </summary>
    [Description("历史查询服务")]
    HISTORY_SERVER = 18,
    /// <summary>
    /// 短信服务
    /// </summary>
    [Description("短信服务")]
    SMS_SERVER = 19,
    /// <summary>
    /// 风控参数服务
    /// </summary>
    [Description("风控参数服务")]
    RISK_PARAM_SERVER = 20,
    /// <summary>
    /// 风控数据服务
    /// </summary>
    [Description("风控数据服务")]
    RISK_DATA_SERVER = 21,
    /// <summary>
    /// 强平计算服务
    /// </summary>
    [Description("强平计算服务")]
    FORCE_CALC_SERVER = 22,
    /// <summary>
    /// 风控触发服务
    /// </summary>
    [Description("风控触发服务")]
    REIS_TRIGGER_SERVER = 23,
    /// <summary>
    /// 强平查询
    /// </summary>
    [Description("强平查询")]
    FORCE_QUERY_SERVER = 24,
    /// <summary>
    /// 中转服务
    /// </summary>
    [Description("中转服务")]
    TRANSFER_SERVER = 25,
    /// <summary>
    /// 持久服务
    /// </summary>
    [Description("持久服务")]
    Persistent_Service = 26,
    /// <summary>
    /// 路由服务
    /// </summary>
    [Description("路由服务")]
    Route_Service = 27,
    /// <summary>
    /// 仲裁服务
    /// </summary>
    [Description("仲裁服务")]
    ARBITRATION_SERVER = 28,
    /// <summary>
    /// 内存服务
    /// </summary>
    [Description("内存服务")]
    CACHE_SERVER = 29,

    /// <summary>
    /// 日志检测服务
    /// </summary>
    [Description("日志检测服务")]
    LOG_CHECK_SERVER = 30,
    /// <summary>
    /// 监控客户端
    /// </summary>
    [Description("监控客户端")]
    MONITOR_CLIENT = 31,
    /// <summary>
    /// 日志汇聚服务
    /// </summary>
    [Description("日志汇聚服务")]
    LOG_CONVERGE_SERVER = 32,
    /// <summary>
    /// 策略服务
    /// </summary>
    [Description("策略服务")]
    STRATEGY_SERVER = 33,
    /// <summary>
    /// 交易后台
    /// </summary>
    [Description("交易后台")]
    TRADE_BACKSTAGE = 34,
}
