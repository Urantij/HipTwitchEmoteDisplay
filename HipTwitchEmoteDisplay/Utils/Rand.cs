namespace HipTwitchEmoteDisplay.Utils;

public static class Rand
{
    public static T GetItem<T>(this Random random, ICollection<T> items)
    {
        int index = random.Next(0, items.Count);

        return items.ElementAt(index);
    }
}