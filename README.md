
# Ormbunke
Ormbunke is a LUA interface inside of Gorilla Tag. It supports a lot of things, will add even more. If you've ever cheated in roblox then you know what an executor is. It's basically a program that runs a script that changes things in the game at runtime so you don't have to close the game inorder to switch or remove mods.


## Authors

- [@Vidde](https://www.youtube.com/channel/UCHvt7X1hBjoTpUzXEiSjzVw)


## Installation
Download the .dll and place it inside of your plugins folder.
## Roadmap
- Make Interface be able to access Networking related classes.

- Make menu look better.

- Add error logging to make it easier for further use.

- Add Syntax highlighting

- Add Auto complete
## Documentation


```C#
Mathf:... => Unity Mathf namespace wrapper

GmaeObject:Find(string) => GameObject.Find Wrapper
GameObject:CreatePrimitive(PrimitiveType) => GameObject.CreatePrimitive Wrapper
Create(string) => new GameObject Wrapper
Destroy(GameObject) => GameObject.Destroy Wrapper

loadstring(string) => Gets raw code from specified URL and runs it
print(string) => Prints specified message to Unity console.


-- Game Instances --
_GorillaBattleManager  => GorillaBattleManager.instance
_GorillaPlayer         => GorillaLocomotion.Player.instance
_GorillaDayNight       => GorillaDayNight.instance
_GorillaComputer       => GorillaParent.instance
_BetterDayNightManager => BetterDayNightManager.instance
_GorillaParent         => GorillaParent.instance

```
Supports Following Unity Classes / Namespaces / Enums / Structs
```C#
GameObject
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

Mathf
Vector2
Vector3
Vector4
Quaternion
```
## Acknowledgements
 - [LUA Lexer](https://www.moonsharp.org/)
