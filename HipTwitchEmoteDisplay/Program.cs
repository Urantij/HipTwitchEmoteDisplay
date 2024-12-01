using System.Text.Json.Serialization;
using HipTwitchEmoteDisplay;
using HipTwitchEmoteDisplay.Emotes;
using HipTwitchEmoteDisplay.Emotes.Bttv;
using HipTwitchEmoteDisplay.Emotes.Ffz;
using HipTwitchEmoteDisplay.Emotes.SemTv;
using HipTwitchEmoteDisplay.Emotes.Twitch;
using HipTwitchEmoteDisplay.Link;
using HipTwitchEmoteDisplay.Twitch;
using HipTwitchEmoteDisplay.Utils;
using HipTwitchEmoteDisplay.Work;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(c => { c.TimestampFormat = "[HH:mm:ss] "; });

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    });

builder.Services.AddOptions<AppConfig>()
    .BindConfiguration("App")
    .Validate(config => config.TwitchUsername != default && config.TwitchId != default)
    .ValidateOnStart();

builder.Services.AddSingleton<IEmoter, SemTvEmoter>();
builder.Services.AddSingleton<IEmoter, FfzEmoter>();
builder.Services.AddSingleton<IEmoter, BttvEmoter>();
if (builder.Configuration.GetSection(TwitchApiConfig.Key).Exists())
{
    builder.Services.AddOptions<TwitchApiConfig>()
        .BindConfiguration(TwitchApiConfig.Key)
        .Validate(config => config.ClientId != default && config.Secret != default)
        .ValidateOnStart();
    builder.Services.AddSingleton<IEmoter, TwitchEmoter>();
    Console.WriteLine("Твич добавлен.");
}

builder.Services.AddHostedSingleton<EmoteVault>();

builder.Services.AddSingleton<LinkGlobal>();

builder.Services.AddHostedSingleton<TwitchChatter>();

builder.Services.AddHostedSingleton<Retranslator>();
builder.Services.AddHostedSingleton<ItsMyLife>();
builder.Services.AddHostedSingleton<StealthActor>();

if (builder.Environment.IsProduction() && builder.Configuration.GetValue<string>("urls") != null)
{
    builder.Services.AddHostedSingleton<PageOpener>();
}

WebApplication app = builder.Build();

app.MapHub<LinkHub>("/hub");

app.UseStaticFiles();

app.Run();

[JsonSerializable(typeof(LinkMessage))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}