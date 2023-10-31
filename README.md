
# Ormbunke
Ormbunke is a LUA interface inside of Gorilla Tag. It supports a lot of things, will add even more. If you've ever cheated in roblox then you know what an executor is. It's basically a program that runs a script that changes things in the game at runtime so you don't have to close the game inorder to switch or remove mods.

## Why?
This makes it extremely easy to load mods ar runtime. Why have to reopen the game when you want to change / add mods when you can just cope paste a script and click a button?

## Authors

- [@Vidde](https://www.youtube.com/channel/UCHvt7X1hBjoTpUzXEiSjzVw)


## Installation
Download the .dll and place it inside of your plugins folder.
## Roadmap

- Make menu look better.

- Add error logging to make it easier for further use.

- Add Syntax highlighting

- Add Auto complete
## Documentation


```C#
GmaeObject:Find(string) => GameObject.Find Wrapper
GameObject:CreatePrimitive(PrimitiveType) => GameObject.CreatePrimitive Wrapper
GameObject:New(string) => new GameObject() Wrapper
GameObject:Destroy(GameObject) => GameObject.Destroy Wrapper

loadstring(string) => Gets raw code from specified URL and runs it
print(string) => Prints specified message to Unity console.
printerr(string) => Prints specified message to Unity console as an error.


-- Game Instances --
_GorillaBattleManager    => GorillaBattleManager.instance
_GorillaPlayer           => GorillaLocomotion.Player.instance
_GorillaDayNight         => GorillaDayNight.instance
_GorillaComputer         => GorillaParent.instance
_BetterDayNightManager   => BetterDayNightManager.instance
_GorillaParent           => GorillaParent.instance
_PhotonNetworkController => PhotonNetworkController.Instance

```
Supports Following Unity Classes / Namespaces / Enums / Structs
```C#
GameObject
UnityEngine.Object
Transform
Rigidbody
BoxCollider
CapsuleCollider
Collider
MeshCollider
MeshRenderer
SkinnedMeshRenderer
Shader
Material

GorillaLocomotion.Player
VRRig
GorillaBattleManager
GorillaDayNight
GorillaComputer
GorillaParent
BetterDayNightManager
PhotonNetworkController

PhotonEvent
PhotonHandler
PhotonNetworkController
PhotonTag
PhotonView

Mathf
Vector2
Vector3
Vector4
Quaternion
```
## Acknowledgements
 - [LUA Lexer](https://www.moonsharp.org/)

## Screenshots
![Screenshot](https://cdn.discordapp.com/attachments/1084603189053116538/1168375298941779978/image.png)

