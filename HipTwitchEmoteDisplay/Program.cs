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

// var sampleTodos = new Todo[]
// {
//     new(1, "Walk the dog"),
//     new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
//     new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
//     new(4, "Clean the bathroom"),
//     new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
// };

app.MapHub<LinkHub>("/hub");

app.UseStaticFiles();

// var todosApi = app.MapGroup("/todos");
// todosApi.MapGet("/", () => sampleTodos);
// todosApi.MapGet("/{id}", (int id) =>
//     sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
//         ? Results.Ok(todo)
//         : Results.NotFound());

app.Run();

// public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

// [JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(LinkMessage))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}