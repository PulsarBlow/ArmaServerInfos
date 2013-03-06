#ArmaServerInfos
> Armored Assault 2 - GameServer Query Library

##Description

ArmaServerInfos is a .NET C# network library which encapsulates the Arma2 UDP server protocol.
With ArmaServerInfos you can query any server in the wild to retrieve its exposed data and work with them easily.

Building a game launcher UI on top of it is pretty simple.


##Examples

### Basic
```csharp
GameServer server = new GameServer("127.0.0.1", 2302);
server.Update();
```

That's all !
Now your GameServer object reference contains all the server data :
* server.ServerInfos : contains all the parsed game server data (like server version, mods, max number of players, number of players online etc..)
* server.Players : contains the list of current players

### Events
The update process triggers a OnlineStateChanged event so you can bind some UI changes.

```csharp
GameServer server = new GameServer("127.0.0.1", 2302);
server.OnlineStateChanged += MyEventHandler;
server.Update();
```

## Solution structure
There are 3 projects in the solution

* ArmaServerInfos : This is the main library
* ArmaServerInfos.Tests : Unit tests suit for the library
* ConsoleApp : A simple console application provided as an implementation example.

## Requirements

* Visual Studio 2012
* .NET 4.5

The raw code should work with on any .NET 2.0+ system (Mono even). You just have to recreate a solution targeting your requirements.
