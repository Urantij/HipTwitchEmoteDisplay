using System.Text.Json.Serialization;
using HipTwitchEmoteDisplay;
using HipTwitchEmoteDisplay.Emotes;
using HipTwitchEmoteDisplay.Emotes.Bttv;
using HipTwitchEmoteDisplay.Emotes.Ffz;
using HipTwitchEmoteDisplay.Emotes.SemTv;
using HipTwitchEmoteDisplay.Link;
using HipTwitchEmoteDisplay.Twitch;
using HipTwitchEmoteDisplay.Utils;
using HipTwitchEmoteDisplay.Work;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

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

builder.Services.AddHostedSingletonAsService<BaseEmoter, SemTvEmoter>();
builder.Services.AddHostedSingletonAsService<BaseEmoter, FfzEmoter>();
builder.Services.AddHostedSingletonAsService<BaseEmoter, BttvEmoter>();

builder.Services.AddSingleton<EmoteVault>();

builder.Services.AddSingleton<LinkGlobal>();

builder.Services.AddHostedSingleton<TwitchChatter>();

builder.Services.AddHostedSingleton<Retranslator>();
builder.Services.AddHostedSingleton<ItsMyLife>();
builder.Services.AddHostedSingleton<StealthActor>();

WebApplication app = builder.Build();

app.MapHub<LinkHub>("/hub");

app.UseStaticFiles();

app.Run();

[JsonSerializable(typeof(LinkMessage))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}