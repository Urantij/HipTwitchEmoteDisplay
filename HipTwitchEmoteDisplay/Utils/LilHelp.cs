using System.Net;
using System.Text.RegularExpressions;

namespace HipTwitchEmoteDisplay.Utils;

public class LilHelp
{
    // Потому что в микрософт работают талантливые разработчики. 
    // А вы что, не знали?
    private static readonly Regex CancerRegex =
        new(@"^socks5\:\/\/(?<name>\w+)\:(?<pass>.+?)@(?<ip>.+?)\:(?<port>.+?)$");

    public static IWebProxy? MakeProxy(string? line)
    {
        if (line == null)
            return null;

        Match cancerMatch = CancerRegex.Match(line);

        if (!cancerMatch.Success) return new WebProxy(line);

        // он всё равно ест фул строку как надо, даже с логином и пасом.
        // просто сами логин и пас игнорятся
        return new WebProxy(line)
        {
            Credentials = new NetworkCredential(cancerMatch.Groups["name"].Value, cancerMatch.Groups["pass"].Value)
        };
    }
}