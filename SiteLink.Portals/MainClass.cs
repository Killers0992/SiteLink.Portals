using Microsoft.Extensions.DependencyInjection;
using SiteLink.API;
using SiteLink.API.Plugins;

namespace Portals;

public class MainClass : Plugin
{
    public override string Name { get; } = "Portals";

    public override string Description { get; } = "Adds ability to spawn portals which redirect player to servers";

    public override string Author { get; } = "Killers0992";

    public override Version Version { get; } = new Version(1, 0, 2);

    public override Version ApiVersion { get; } = new Version(SiteLinkAPI.ApiVersionText);

    public override void OnLoad(IServiceCollection collection)
    {

    }
}
