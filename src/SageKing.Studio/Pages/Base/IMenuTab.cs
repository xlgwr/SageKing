using Microsoft.AspNetCore.Components;
using SageKing.Application.AspNetCore.SqlSugar.Service;

namespace SageKing.Studio.Pages.Base;

public interface IMenuTab
{
    public SysMenuService MenuService { get; }

    public long menuid { get; set; }

    public string TabTitle { get; set; }

    public string TabIcon { get; set; }

    public RenderFragment GetPageTitle();

    public async Task InitTabTitle()
    {
        if (menuid.IsNullOrEmpty())
        {
            return;
        }
        var getmenu = await MenuService.GetDetailCache(menuid);
        if (getmenu == null)
        {
            return;
        }
        TabIcon = getmenu.Icon.NullOrWhiteSpaceToDefault("home");
        TabTitle = getmenu.Title;
    }
}
