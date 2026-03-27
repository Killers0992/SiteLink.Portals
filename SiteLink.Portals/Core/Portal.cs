using UnityEngine;
using SiteLink.API.Core;
using SiteLink.API.Networking;
using SiteLink.API.Networking.Objects;

namespace Portals.Core;

public class Portal
{
    private DateTime _nextCheck;
    private TextToyObject _text;
    private string _textFormat;

    public static Dictionary<World, List<Portal>> SpawnedPortals = new Dictionary<World, List<Portal>>();

    public const float MinimumDistanceToActivePortal = 1.5f;

    public World World { get; }
    public Server Server => Server.Get<Server>(name: TargetServer);
    public string TargetServer { get; }
    public Vector3 Position { get; }
    public Quaternion Rotation { get; }

    public Portal(World world, string targetServer, string textFormat, Vector3 position, Quaternion rotation)
    {
        _textFormat = textFormat;

        World = world;
        TargetServer = targetServer;
        Position = position;
        Rotation = rotation;

        if (!SpawnedPortals.TryGetValue(world, out List<Portal> portals))
        {
            portals = new List<Portal>();
            SpawnedPortals.Add(world, portals);
        }

        _text = new TextToyObject(world);

        _text.Position = position;
        _text.Rotation = rotation;

        _text.TextToy.Position = position;
        _text.TextToy.Rotation = rotation;
        _text.TextToy.Scale = Vector3.one;

        _text.TextToy.TextFormat = FormatText();
        _text.TextToy.DisplaySize = new Vector2(150f, 50f);

        portals.Add(this);
    }

    public string FormatText()
    {
        if (Server == null)
        {
            return $"<size=5>Server\n\"<color=red>{TargetServer}</color>\"\nnot found!\n\nmodify plugins/lobby/config.yml";
        }

        string tempText = _textFormat;

        Dictionary<string, Func<string>> placeHolders = new Dictionary<string, Func<string>>()
        {
            { "%serverName%", () =>
                {
                    return Server.Name;
                }
            },
            { "%onlinePlayers%", () => 
                {
                    return Server.SessionsCount.ToString();
                } 
            },
            { "%maxPlayers%", () =>
                {
                    return Server.Settings.MaxClients.ToString();
                }
            }
        };

        foreach (var placeholder in placeHolders)
        {
            tempText = tempText.Replace(placeholder.Key, placeholder.Value.Invoke());
        }

        return tempText;
    }

    public void Update()
    {
        if (_nextCheck > DateTime.Now)
            return;

        foreach (Session session in World.GetClientsSnapshot())
        {
            if (Vector3.Distance(session.Position, Position) > MinimumDistanceToActivePortal)
                continue;

            PlayerActivatedPortal(session);
        }

        _nextCheck = DateTime.Now.AddSeconds(1);
    }

    void PlayerActivatedPortal(Session session)
    {
        session.Connection?.Connect(TargetServer, false);
    }
}
