using BlazorPro.BlazorSize;

namespace SageKing.Studio.Contracts;

public sealed class BrowserConsts
{
    public const string ScrollY = "430px";
    public const string ScrollY1080 = "650px";
    public const int browserHeight = 700;
    public const int browserHeight1080 = 924;
    public const int ScrollYOffset = 280;


    public static string LargeHeight1080_800 = Breakpoints.Between("(max-Height: 1080px)", "(min-Height: 800px)");
}
