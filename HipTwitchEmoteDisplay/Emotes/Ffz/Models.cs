// using System.Text.Json.Serialization;
//
// namespace HipTwitchEmoteDisplay.Emotes.Ffz;
//
// // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
// public class _1532818
// {
//     [JsonConstructor]
//     public _1532818(
//         int? id,
//         int? type,
//         object icon,
//         string title,
//         object css,
//         List<Emoticon> emoticons
//     )
//     {
//         this.Id = id;
//         this.Type = type;
//         this.Icon = icon;
//         this.Title = title;
//         this.Css = css;
//         this.Emoticons = emoticons;
//     }
//
//     [JsonPropertyName("id")] public int? Id { get; }
//
//     [JsonPropertyName("_type")] public int? Type { get; }
//
//     [JsonPropertyName("icon")] public object Icon { get; }
//
//     [JsonPropertyName("title")] public string Title { get; }
//
//     [JsonPropertyName("css")] public object Css { get; }
//
//     [JsonPropertyName("emoticons")] public IReadOnlyList<Emoticon> Emoticons { get; }
// }
//
// public class _1539687
// {
//     [JsonConstructor]
//     public _1539687(
//         int? id,
//         int? type,
//         object icon,
//         string title,
//         object css,
//         List<Emoticon> emoticons
//     )
//     {
//         this.Id = id;
//         this.Type = type;
//         this.Icon = icon;
//         this.Title = title;
//         this.Css = css;
//         this.Emoticons = emoticons;
//     }
//
//     [JsonPropertyName("id")] public int? Id { get; }
//
//     [JsonPropertyName("_type")] public int? Type { get; }
//
//     [JsonPropertyName("icon")] public object Icon { get; }
//
//     [JsonPropertyName("title")] public string Title { get; }
//
//     [JsonPropertyName("css")] public object Css { get; }
//
//     [JsonPropertyName("emoticons")] public IReadOnlyList<Emoticon> Emoticons { get; }
// }
//
// public class Owner
// {
//     [JsonConstructor]
//     public Owner(
//         int? id,
//         string name,
//         string displayName
//     )
//     {
//         this.Id = id;
//         this.Name = name;
//         this.DisplayName = displayName;
//     }
//
//     [JsonPropertyName("_id")] public int? Id { get; }
//
//     [JsonPropertyName("name")] public string Name { get; }
//
//     [JsonPropertyName("display_name")] public string DisplayName { get; }
// }
//
// public class Sets
// {
//     [JsonConstructor]
//     public Sets(
//         Set __3,
//         _1532818 __1532818,
//         _1539687 __1539687
//     )
//     {
//         this._3 = __3;
//         this._1532818 = __1532818;
//         this._1539687 = __1539687;
//     }
//
//     [JsonPropertyName("3")] public Set _3 { get; }
//
//     [JsonPropertyName("1532818")] public _1532818 _1532818 { get; }
//
//     [JsonPropertyName("1539687")] public _1539687 _1539687 { get; }
// }
//
// public class Urls
// {
//     [JsonConstructor]
//     public Urls(
//         string __1,
//         string __2,
//         string __4
//     )
//     {
//         this._1 = __1;
//         this._2 = __2;
//         this._4 = __4;
//     }
//
//     [JsonPropertyName("1")] public string _1 { get; }
//
//     [JsonPropertyName("2")] public string _2 { get; }
//
//     [JsonPropertyName("4")] public string _4 { get; }
// }
//
// public class Users
// {
//     [JsonConstructor]
//     public Users(
//         List<string> __1532818
//     )
//     {
//         this._1532818 = __1532818;
//     }
//
//     [JsonPropertyName("1532818")] public IReadOnlyList<string> _1532818 { get; }
// }