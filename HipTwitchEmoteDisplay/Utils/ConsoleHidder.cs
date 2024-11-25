using System.Runtime.InteropServices;

namespace HipTwitchEmoteDisplay.Utils;

// https://stackoverflow.com/a/3571628
public static class ConsoleHidder
{
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 0;
    const int SW_SHOW = 5;

    public static void Hide()
    {
        if (!OperatingSystem.IsWindows()) return;

        var handle = GetConsoleWindow();
        ShowWindow(handle, SW_HIDE);
    }
}