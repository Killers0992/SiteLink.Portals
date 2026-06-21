using Microsoft.Extensions.DependencyInjection;
using SiteLink.API;
using SiteLink.API.Plugins;

namespace Portals;

public class MainClass : Plugin<Config, Translations>
{
    public static MainClass Instance { get; private set; }
    public override string Name { get; } = "Portals";

    public override string Description { get; } = "Adds ability to spawn portals which redirect player to servers";

    public override string Author { get; } = "Killers0992";

    public override Version Version { get; } = new Version(1, 0, 2);

    public override Version ApiVersion { get; } = new Version(SiteLinkAPI.ApiVersionText);
    public override string Repository => "Killers0992/SiteLink.Portals";

    public override void OnLoad(IServiceCollection collection)
    {
        Instance = this;
    }

    public override void OnUnload()
    {
        Instance = null;
        base.OnUnload();
    }
}
