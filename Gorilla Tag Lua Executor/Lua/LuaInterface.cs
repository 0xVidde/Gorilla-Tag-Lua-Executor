using MoonSharp;
using MoonSharp.Interpreter;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Gorilla_Tag_Lua_Executor.Lua.CustomLua;
using GorillaNetworking;
using Photon.Pun;

namespace Gorilla_Tag_Lua_Executor.Lua
{
    public static class LuaInterface
    {
        public static Script mainScript;

        public static void InitLua()
        {
            Debug.Log("Initiating LUA Engine...");

            mainScript = new Script();

            UserData.RegisterType<Mathf>();

            // Components
            UserData.RegisterType<GameObject>();
            UserData.RegisterType<Transform>();
            UserData.RegisterType<Rigidbody>();
            UserData.RegisterType<BoxCollider>();
            UserData.RegisterType<CapsuleCollider>();
            UserData.RegisterType<Collider>();
            UserData.RegisterType<MeshCollider>();
            UserData.RegisterType<MeshRenderer>();
            UserData.RegisterType<Shader>();
            UserData.RegisterType<SkinnedMeshRenderer>();
             
            UserData.RegisterType<GorillaLocomotion.Player>();
            UserData.RegisterType<VRRig>();
            UserData.RegisterType<GorillaBattleManager>();
            UserData.RegisterType<GorillaDayNight>();
            UserData.RegisterType<GorillaComputer>();
            UserData.RegisterType<GorillaParent>();
            UserData.RegisterType<BetterDayNightManager>();
            UserData.RegisterType<PhotonNetworkController>();

            // Math
            UserData.RegisterType<Vector2>();
            UserData.RegisterType<Vector3>();
            UserData.RegisterType<Vector4>();

            mainScript.Globals["print"] = (Func<object, bool>)CustomLua.LUA_print;
            mainScript.Globals["loadstring"] = (Func<string, bool>)CustomLua.LUA_loadstring;

            mainScript.Globals["_GorillaBattleManager"] = GorillaBattleManager.instance;
            mainScript.Globals["_GorillaPlayer"] = GorillaLocomotion.Player.Instance;
            mainScript.Globals["_GorillaDayNight"] = GorillaDayNight.instance;
            mainScript.Globals["_GorillaComputer"] = GorillaComputer.instance;
            mainScript.Globals["_GorillaParent"] = GorillaParent.instance;
            mainScript.Globals["_BetterDayNightManager"] = BetterDayNightManager.instance;

            mainScript.Globals["GameObject"] = new GameObject("GameObject LUA Wrapper");
            mainScript.Globals["Create"] = (Func<string, GameObject>)CustomLua.LUA_GameObjectCreate;
            mainScript.Globals["Destroy"] = (Func<GameObject, bool>)CustomLua.LUA_GameObjectDestroy;
            mainScript.Globals["Find"] = (Func<string, GameObject>)CustomLua.LUA_GameObjectFind;

            mainScript.Globals["Mathf"] = new Mathf();
            mainScript.Globals["Vector2"] = new Vector2();
            mainScript.Globals["Vector3"] = new Vector3();
            mainScript.Globals["Vector4"] = new Vector4();

            foreach (Type item in UserData.GetRegisteredTypes())
            {
                Debug.Log("Loaded Type Into LUA Engine: " + item.Name);
            }

            foreach (DynValue item in mainScript.Globals.Keys)
            {
                Debug.Log("Loaded Global Into LUA Engine: " + item.ToString());
            }
        }

        public static bool RunCode(string code)
        {
            DynValue res = mainScript.DoString(code);

            return res.Boolean;
        }
    }
}
