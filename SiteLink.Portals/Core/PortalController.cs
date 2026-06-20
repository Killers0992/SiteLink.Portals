using SiteLink.API.Core;

namespace Portals.Core;

public class PortalController
{
    public static void Update(World world)
    {
        if (!Portal.SpawnedPortals.TryGetValue(world, out List<Portal> portals))
            return;

        try
        {
            foreach (Portal portal in portals)
            {
                portal.Update();
            }
        }
        catch (Exception)
        {

        }
    }
}
