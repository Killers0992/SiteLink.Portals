using SiteLink.API.Core;
using SiteLink.API.Misc;
using SiteLink.API.Networking;
using SiteLink.API.Networking.Objects;
using SiteLink.API.Translations;
using UnityEngine;

namespace Portals.Core;

public class Portal
{
    private DateTime _nextCheck;
    private TextToyObject _text;
    private Func<string> _textFormat;

    public static Dictionary<World, List<Portal>> SpawnedPortals = new Dictionary<World, List<Portal>>();

    public const float MinimumDistanceToActivePortal = 1.5f;

    public World World { get; }
    public Server Server => Server.Get<Server>(name: TargetServer);
    public string TargetServer { get; }
    public Vector3 Position { get; }
    public Quaternion Rotation { get; }

    public Portal(World world, string targetServer, Func<string> textFormat, Vector3 position, Quaternion rotation)
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

        _text.TextToy.TextFormat = string.Empty;
        _text.TextToy.DisplaySize = new Vector2(150f, 50f);

        portals.Add(this);
    }

    public void UpdateText()
    {
        foreach (Session observer in World.GetClientsSnapshot())
        {
            TranslationContext context = TranslationContext.For(observer, Server, MainClass.Instance)
                .With("server_name", TargetServer);
            _text.SendText(observer, FormatText(observer), context);
        }
    }

    public string FormatText(Session observer = null)
    {
        if (Server == null)
            return MainClass.Instance.Translate(
                observer,
                translations => translations.ServerNotFound,
                TranslationContext.For(observer, server: null, MainClass.Instance)
                    .With("server_name", TargetServer));

        string tempText = _textFormat.Invoke();
        return TranslationManager.Format(
                tempText
                    .Replace("%serverName%", "{server_name}")
                    .Replace("%onlinePlayers%", "{online}")
                    .Replace("%maxPlayers%", "{max_players}"),
                TranslationContext.For(observer, Server, MainClass.Instance))
            .Format();
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

        UpdateText();
    }

    void PlayerActivatedPortal(Session session)
    {
        session.Connection?.Connect(TargetServer, false);
    }
}
