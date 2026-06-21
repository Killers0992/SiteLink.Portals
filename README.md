![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/Killers0992/SiteLink.Portals/total?label=Downloads\&labelColor=2e343e\&color=00FFFF\&style=for-the-badge)
[![Discord](https://img.shields.io/discord/1434213646510325762?label=Discord\&labelColor=2e343e\&color=00FFFF\&style=for-the-badge)](https://discord.gg/Sva8TaCR7Q)

# SiteLink.Portals

**SiteLink.Portals** is a core API plugin for [SiteLink](https://github.com/Killers0992/SiteLink) that provides a **universal portal system** for redirecting players between servers inside the SiteLink network.  
It allows other plugins (like [SiteLink.Lobby](https://github.com/Killers0992/SiteLink.Lobby)) to **spawn, manage, and update interactive portals** that transfer players seamlessly.

---

## 🧩 Requirements

| Dependency | Version |
|-------------|----------|
| [SiteLink](https://github.com/Killers0992/SiteLink) | **2.1.0** or newer |

---

## ✨ Features

- **Portal API** – Simple API for creating in-world portals that redirect players to specific servers.  
- **Reusable System** – Other plugins can easily spawn portals dynamically or from configuration.  
- **Efficient Update Loop** – Handles player detection and teleportation smoothly via `PortalController`.  
- **Text Rendering** – Each portal automatically displays its target server name using 3D text objects.  

---

## 🧠 How It Works

- Each **Portal** exists inside a specific **World** instance.  
- The **PortalController** continuously checks player proximity to portals.  
- When a player enters the portal radius, they are automatically connected to the target server.  
- Text objects above portals display formatted information, such as the destination server name.

---

## 🚀 Installation

1. Place the compiled **`Portals.dll`** into your SiteLink **`Plugins`** directory.  
2. Restart SiteLink — the plugin loads automatically.  
3. Other plugins (like Lobby) can now create portals using the Portals API.

