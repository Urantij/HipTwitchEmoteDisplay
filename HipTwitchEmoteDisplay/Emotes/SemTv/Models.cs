// using System.Text.Json.Serialization;
//
// namespace HipTwitchEmoteDisplay.Emotes.SemTv;
//
// public class Connection
// {
//     [JsonConstructor]
//     public Connection(
//         string id,
//         string platform,
//         string username,
//         string displayName,
//         object linkedAt,
//         int? emoteCapacity,
//         string emoteSetId,
//         object emoteSet
//     )
//     {
//         this.Id = id;
//         this.Platform = platform;
//         this.Username = username;
//         this.DisplayName = displayName;
//         this.LinkedAt = linkedAt;
//         this.EmoteCapacity = emoteCapacity;
//         this.EmoteSetId = emoteSetId;
//         this.EmoteSet = emoteSet;
//     }
//
//     [JsonPropertyName("id")] public string Id { get; }
//
//     [JsonPropertyName("platform")] public string Platform { get; }
//
//     [JsonPropertyName("username")] public string Username { get; }
//
//     [JsonPropertyName("display_name")] public string DisplayName { get; }
//
//     [JsonPropertyName("linked_at")] public object LinkedAt { get; }
//
//     [JsonPropertyName("emote_capacity")] public int? EmoteCapacity { get; }
//
//     [JsonPropertyName("emote_set_id")] public string EmoteSetId { get; }
//
//     [JsonPropertyName("emote_set")] public object EmoteSet { get; }
// }
//
// public class Editor
// {
//     [JsonConstructor]
//     public Editor(
//         string id,
//         int? permissions,
//         bool? visible,
//         object addedAt
//     )
//     {
//         this.Id = id;
//         this.Permissions = permissions;
//         this.Visible = visible;
//         this.AddedAt = addedAt;
//     }
//
//     [JsonPropertyName("id")] public string Id { get; }
//
//     [JsonPropertyName("permissions")] public int? Permissions { get; }
//
//     [JsonPropertyName("visible")] public bool? Visible { get; }
//
//     [JsonPropertyName("added_at")] public object AddedAt { get; }
// }
//
// public class EmoteSet2
// {
//     [JsonConstructor]
//     public EmoteSet2(
//         string id,
//         string name,
//         int? flags,
//         List<object> tags,
//         int? capacity
//     )
//     {
//         this.Id = id;
//         this.Name = name;
//         this.Flags = flags;
//         this.Tags = tags;
//         this.Capacity = capacity;
//     }
//
//     [JsonPropertyName("id")] public string Id { get; }
//
//     [JsonPropertyName("name")] public string Name { get; }
//
//     [JsonPropertyName("flags")] public int? Flags { get; }
//
//     [JsonPropertyName("tags")] public IReadOnlyList<object> Tags { get; }
//
//     [JsonPropertyName("capacity")] public int? Capacity { get; }
// }
//
// public class Owner
// {
//     [JsonConstructor]
//     public Owner(
//         string id,
//         string username,
//         string displayName,
//         string avatarUrl,
//         Style style,
//         List<string> roleIds,
//         List<Connection> connections
//     )
//     {
//         this.Id = id;
//         this.Username = username;
//         this.DisplayName = displayName;
//         this.AvatarUrl = avatarUrl;
//         this.Style = style;
//         this.RoleIds = roleIds;
//         this.Connections = connections;
//     }
//
//     [JsonPropertyName("id")] public string Id { get; }
//
//     [JsonPropertyName("username")] public string Username { get; }
//
//     [JsonPropertyName("display_name")] public string DisplayName { get; }
//
//     [JsonPropertyName("avatar_url")] public string AvatarUrl { get; }
//
//     [JsonPropertyName("style")] public Style Style { get; }
//
//     [JsonPropertyName("role_ids")] public IReadOnlyList<string> RoleIds { get; }
//
//     [JsonPropertyName("connections")] public IReadOnlyList<Connection> Connections { get; }
// }
//
// public class Style
// {
//     [JsonConstructor]
//     public Style(
//         int? color,
//         string badgeId,
//         string paintId
//     )
//     {
//         this.Color = color;
//         this.BadgeId = badgeId;
//         this.PaintId = paintId;
//     }
//
//     [JsonPropertyName("color")] public int? Color { get; }
//
//     [JsonPropertyName("badge_id")] public string BadgeId { get; }
//
//     [JsonPropertyName("paint_id")] public string PaintId { get; }
// }
//
// public class User
// {
//     [JsonConstructor]
//     public User(
//         string id,
//         string username,
//         string displayName,
//         long? createdAt,
//         string avatarUrl,
//         Style style,
//         List<EmoteSet> emoteSets,
//         List<Editor> editors,
//         List<string> roles,
//         List<Connection> connections
//     )
//     {
//         this.Id = id;
//         this.Username = username;
//         this.DisplayName = displayName;
//         this.CreatedAt = createdAt;
//         this.AvatarUrl = avatarUrl;
//         this.Style = style;
//         this.EmoteSets = emoteSets;
//         this.Editors = editors;
//         this.Roles = roles;
//         this.Connections = connections;
//     }
//
//     [JsonPropertyName("id")] public string Id { get; }
//
//     [JsonPropertyName("username")] public string Username { get; }
//
//     [JsonPropertyName("display_name")] public string DisplayName { get; }
//
//     [JsonPropertyName("created_at")] public long? CreatedAt { get; }
//
//     [JsonPropertyName("avatar_url")] public string AvatarUrl { get; }
//
//     [JsonPropertyName("style")] public Style Style { get; }
//
//     [JsonPropertyName("emote_sets")] public IReadOnlyList<EmoteSet> EmoteSets { get; }
//
//     [JsonPropertyName("editors")] public IReadOnlyList<Editor> Editors { get; }
//
//     [JsonPropertyName("roles")] public IReadOnlyList<string> Roles { get; }
//
//     [JsonPropertyName("connections")] public IReadOnlyList<Connection> Connections { get; }
// }