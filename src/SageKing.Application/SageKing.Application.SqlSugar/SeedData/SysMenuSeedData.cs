namespace SageKing.Application.AspNetCore.SqlSugar.SeedData;
/// <summary>
/// 系统菜单种子数据
/// </summary>
public class SysMenuSeedData : ISqlSugarEntitySeedData<SysMenu>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SysMenu> HasData()
    {
        int sept = 100;
        long septDir = 10000000000;
        var defaultStartId = SeedDataConst.DefaultMenuId + sept;
        return new[]
        {
            //Dir
             new SysMenu{ Id=SeedDataConst.DefaultMenuId, Pid=0, Title="工作台", Path="/dashboard", Name="dashboard", Component="Layout", Icon="home", Type=MenuTypeEnum.Dir, OrderNo=0 },
            //menu
            new SysMenu{ Id=defaultStartId, Pid=SeedDataConst.DefaultMenuId, Title="消息中心", Path="/PackageList", Name="PackageList", Component="/PackageList/index",  Icon="home", Type=MenuTypeEnum.Menu,  OrderNo=sept },

            //Dir
             new SysMenu{ Id=defaultStartId+septDir, Pid=0, Title="消息定义", Path="/message", Name="message", Component="Layout", Icon="message", Type=MenuTypeEnum.Dir, OrderNo=sept*10 },
            //menu
             new SysMenu{ Id=defaultStartId+septDir+sept+2, Pid=defaultStartId+septDir, Title="客户端管理", Path="/ClientList", Name="ClientList", Component="/ClientList/index",  Icon="message", Type=MenuTypeEnum.Menu,  OrderNo=sept*10+2 },
             new SysMenu{ Id=defaultStartId+septDir+sept+3, Pid=defaultStartId+septDir, Title="消息结构定义", Path="/MessageDefine", Name="MessageDefine", Component="/MessageDefine/index",  Icon="message", Type=MenuTypeEnum.Menu,  OrderNo=sept*10+3 },

             //Dir
             new SysMenu{ Id=defaultStartId+septDir*2, Pid=0, Title="系统管理", Path="/system", Name="system", Component="system", Icon="setting", Type=MenuTypeEnum.Dir, OrderNo=sept*10*2 },
             //menu
             new SysMenu{ Id=defaultStartId+septDir*2+sept+1, Pid=defaultStartId+septDir*2, Title="菜单管理", Path="/System/SysMenu", Name="menu", Component="/System/SysMenu/index",  Icon="menu", Type=MenuTypeEnum.Menu,  OrderNo=sept*10*2+1 },
             new SysMenu{ Id=defaultStartId+septDir*2+sept+2, Pid=defaultStartId+septDir*2, Title="参数配置", Path="/System/SysConfigPage", Name="config", Component="/System/SysConfigPage/index",  Icon="control", Type=MenuTypeEnum.Menu,  OrderNo=sept*10*2+2 },
             new SysMenu{ Id=defaultStartId+septDir*2+sept+3, Pid=defaultStartId+septDir*2, Title="字典管理", Path="/System/SysDictType", Name="dict", Component="/System/SysDictType/index",  Icon="aim", Type=MenuTypeEnum.Menu,  OrderNo=sept*10*2+3 },

        };
    }
}