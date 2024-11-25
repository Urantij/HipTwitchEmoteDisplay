using System.Diagnostics.CodeAnalysis;

namespace HipTwitchEmoteDisplay.Utils;

public static class ServiceProvider
{
    public static void AddHostedSingleton<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        T>(
        this IServiceCollection collection) where T : class, IHostedService
    {
        collection.AddSingleton<T>();
        collection.AddHostedService<T>(sp => sp.GetRequiredService<T>());
    }

    public static void AddHostedSingletonAsService<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
        this IServiceCollection collection)
        where TService : class, IHostedService
        where TImplementation : class, TService
    {
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService, TImplementation>(sp => sp.GetRequiredService<TImplementation>());
        collection.AddHostedService<TImplementation>(sp => sp.GetRequiredService<TImplementation>());
    }
}