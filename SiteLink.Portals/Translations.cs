using System.ComponentModel;

namespace Portals;

public sealed class Translations
{
    [Description("Placeholders: {server_name}")]
    public string ServerNotFound { get; set; } =
        "<size=5>Server\n\"<color=red>{server_name}</color>\"\nnot found!</size>";
}
