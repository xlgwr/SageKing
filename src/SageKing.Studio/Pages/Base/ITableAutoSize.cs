using BlazorPro.BlazorSize;
using IceRpc.Transports;
using Microsoft.AspNetCore.Components;
using SageKing.Studio.Shared;

namespace SageKing.Studio.Pages.Base;


public interface ITableAutoSize : IDisposable
{
    public Action StateHasChangedAction {  get; set; }
    public IResizeListener listener { get; set; }

    //table height
    public string ScrollY { get; set; }
    public string ScrollY1080 { get; set; }
    public int browserHeight { get; set; }
    public int browserHeight1080 { get; set; }
    public int ScrollYOffset { get; set; }
    public bool IsSmallMedia { get; set; }
    // We can also capture the browser's width / height if needed. We hold the value here.
    public BrowserWindowSize browser { get; set; }

    public Error? Error { get; set; }

    public bool Islarge1080_800 { get; set; }
    public string large1080_800 { get; set; }

    /// <summary>
    /// 首次加载无效
    /// </summary>
    /// <returns></returns>
    public virtual async Task InitTableHeight()
    {
        try
        {
            if (Islarge1080_800)
            {
                return;
            }
            Islarge1080_800 = await listener.MatchMedia(large1080_800);
            ScrollY = Islarge1080_800 ? ScrollY1080 : ScrollY;
            browserHeight = Islarge1080_800 ? browserHeight1080 : browserHeight;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    void IDisposable.Dispose()
    {
        // Always use IDisposable in your component to unsubscribe from the event.
        // Be a good citizen and leave things how you found them.
        // This way event handlers aren't called when nobody is listening.
        listener.OnResized -= WindowResized;
    }

    // This method will be called when the window resizes.
    // It is ONLY called when the user stops dragging the window's edge. (It is already throttled to protect your app from perf. nightmares)
    public virtual async void WindowResized(object _, BrowserWindowSize window)
    {
        // Get the browsers's width / height
        browser = window;

        // Check a media query to see if it was matched. We can do this at any time, but it's best to check on each resize
        IsSmallMedia = await listener.MatchMedia(Breakpoints.SmallDown);

        browserHeight = browser.Height <= 0 ? browserHeight : browser.Height;
        ScrollY = $"{(browserHeight - ScrollYOffset)}px";

        // We're outside of the component's lifecycle, be sure to let it know it has to re-render.
        StateHasChangedAction?.Invoke();
    }
}
