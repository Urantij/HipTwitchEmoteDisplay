using System.Net;

namespace HipTwitchEmoteDisplay.Utils;

public class LilHelp
{
    public static IWebProxy? MakeProxy(string? line)
    {
        if (line == null)
            return null;

        return new WebProxy(line);
    }
}