using MoonSharp.Interpreter;
using System.Net;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Gorilla_Tag_Lua_Executor.Lua
{
    public static class CustomLua
    {
        [MoonSharpUserData]
        public class LUA_Game
        {
            public GorillaLocomotion.Player GetLocalGorillaPlayer()
            {
                return GorillaLocomotion.Player.Instance;
            }
        }

        public static GameObject LUA_GameObjectCreate(string n)
        {
            return new GameObject(n);
        }

        public static bool LUA_GameObjectDestroy(GameObject n)
        {
            GameObject.Destroy(n);

            return true;
        }

        public static GameObject LUA_GameObjectFind(string n)
        {
            return GameObject.Find(n);
        }

        public static bool LUA_print(object msg)
        {
            Debug.Log(msg);

            return true;
        }

        public static bool LUA_loadstring(string url)
        {
            using (WebClient client = new WebClient())
            {
                string code = client.DownloadString(url);

                LuaInterface.RunCode(@"" + code);
            }

            return true;
        }
    }
}
