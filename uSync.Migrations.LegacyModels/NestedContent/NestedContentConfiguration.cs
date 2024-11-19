using System.Runtime.Serialization;

namespace uSync.Migrations.LegacyModels.NestedContent;

/// <summary>
///     Represents the configuration for the nested content value editor.
/// </summary>
public class NestedContentConfiguration
{
    public ContentType[]? ContentTypes { get; set; }

    public int? MinItems { get; set; }

    public int? MaxItems { get; set; }

    public bool ConfirmDeletes { get; set; } = true;

    public bool ShowIcons { get; set; } = true;

    public bool ExpandsOnLoad { get; set; } = true;

    public bool HideLabel { get; set; }

    [DataContract]
    public class ContentType
    {
        [DataMember(Name = "ncAlias")]
        public string? Alias { get; set; }

        [DataMember(Name = "ncTabAlias")]
        public string? TabAlias { get; set; }

        [DataMember(Name = "nameTemplate")]
        public string? Template { get; set; }
    }
}