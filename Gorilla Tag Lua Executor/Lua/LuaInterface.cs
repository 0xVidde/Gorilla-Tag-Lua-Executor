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

            #region Register Types

            // Classes
            UserData.RegisterType<GameObject>();
            UserData.RegisterType<UnityEngine.Object>();
            UserData.RegisterType<Transform>();
            UserData.RegisterType<Rigidbody>();
            UserData.RegisterType<BoxCollider>();
            UserData.RegisterType<CapsuleCollider>();
            UserData.RegisterType<Collider>();
            UserData.RegisterType<MeshCollider>();
            UserData.RegisterType<MeshRenderer>();
            UserData.RegisterType<Shader>();
            UserData.RegisterType<Material>();
            UserData.RegisterType<SkinnedMeshRenderer>();
            
            UserData.RegisterType<GorillaLocomotion.Player>();
            UserData.RegisterType<VRRig>();
            UserData.RegisterType<GorillaBattleManager>();
            UserData.RegisterType<GorillaDayNight>();
            UserData.RegisterType<GorillaComputer>();
            UserData.RegisterType<GorillaParent>();
            UserData.RegisterType<BetterDayNightManager>();

            UserData.RegisterType<PhotonHandler>();
            UserData.RegisterType<PhotonNetworkController>();
            UserData.RegisterType<PhotonTag>();
            UserData.RegisterType<PhotonView>();

            UserData.RegisterType<LUA_Vector2Wrapper>();
            UserData.RegisterType<LUA_Vector3Wrapper>();
            UserData.RegisterType<LUA_Vector4Wrapper>();
            UserData.RegisterType<LUA_QuaternionWrapper>();
            UserData.RegisterType<LUA_GameObjectWrapper>();

            // Structs
            UserData.RegisterType<Mathf>();
            UserData.RegisterType<Vector2>();
            UserData.RegisterType<Vector3>();
            UserData.RegisterType<Vector4>();
            UserData.RegisterType<Quaternion>();

            // Enums
            UserData.RegisterType<PrimitiveType>();

            #endregion

            #region Globals

            // Classes
            mainScript.Globals["GameObject"] = UserData.CreateStatic<LUA_GameObjectWrapper>();
            mainScript.Globals["Vector2"] =    UserData.CreateStatic<LUA_Vector2Wrapper>();
            mainScript.Globals["Vector3"] =    UserData.CreateStatic<LUA_Vector3Wrapper>();
            mainScript.Globals["Vector4"] =    UserData.CreateStatic<LUA_Vector4Wrapper>();
            mainScript.Globals["Quaternion"] = UserData.CreateStatic<LUA_QuaternionWrapper>();

            // Enums
            mainScript.Globals["PrimitiveType"] = UserData.CreateStatic<PrimitiveType>();

            // Methods
            mainScript.Globals["print"] = (Func<object, bool>)CustomLua.LUA_print;
            mainScript.Globals["printerr"] = (Func<object, bool>)CustomLua.LUA_printerr;
            mainScript.Globals["loadstring"] = (Func<string, bool>)CustomLua.LUA_loadstring;

            // Structs
            mainScript.Globals["Mathf"] = new Mathf();

            // Constant game instances
            mainScript.Globals["_GorillaBattleManager"] = GorillaBattleManager.instance;
            mainScript.Globals["_PhotonNetworkController"] = PhotonNetworkController.Instance;
            mainScript.Globals["_GorillaPlayer"] = GorillaLocomotion.Player.Instance;
            mainScript.Globals["_GorillaDayNight"] = GorillaDayNight.instance;
            mainScript.Globals["_GorillaComputer"] = GorillaComputer.instance;
            mainScript.Globals["_GorillaParent"] = GorillaParent.instance;
            mainScript.Globals["_BetterDayNightManager"] = BetterDayNightManager.instance;

            #endregion

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
            try
            {
                DynValue res = mainScript.DoString(code);

                return res.Boolean;
            }
            catch (ScriptRuntimeException ex)
            {
                Debug.LogError("SCRIPT ERROR => " + ex.DecoratedMessage);
            }

            return false;
        }
    }
}
