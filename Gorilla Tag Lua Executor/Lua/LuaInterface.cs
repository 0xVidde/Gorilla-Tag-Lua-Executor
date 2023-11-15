using MoonSharp;
using MoonSharp.Interpreter;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Gorilla_Tag_Lua_Executor.Lua.CustomLua;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine.InputSystem;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Gorilla_Tag_Lua_Executor.Lua
{
    public static class LuaInterface
    {
        public static List<Script> loadedScripts = new List<Script>();
        public static List<DynValue> loopCoroutines = new List<DynValue>();

        public static void InitLuaEngine()
        {
            Debug.Log("Initiating LUA Engine...");

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

            UserData.RegisterType<GUI>();

            UserData.RegisterType<Time>();
            
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

            UserData.RegisterType<LUA_ControllerInput>();
            UserData.RegisterType<LUA_InputManager>();
            UserData.RegisterType<LUA_KeyboardWrapper>();
            UserData.RegisterType<LUA_MouseWrapper>();

            UserData.RegisterType<LUA_GUIWrapper>();
            UserData.RegisterType<LUA_RectWrapper>();

            // Structs
            UserData.RegisterType<Mathf>();
            UserData.RegisterType<Vector2>();
            UserData.RegisterType<Vector3>();
            UserData.RegisterType<Vector4>();
            UserData.RegisterType<Quaternion>();
            UserData.RegisterType<Rect>();

            // Enums
            UserData.RegisterType<PrimitiveType>();

            #endregion

            foreach (Type item in UserData.GetRegisteredTypes())
            {
                Debug.Log("Loaded Type Into LUA Engine: " + item.Name);
            }
        }

        public static void InitScript(Script script)
        {
            #region Globals

            // Classes
            script.Globals["GameObject"] = UserData.CreateStatic<LUA_GameObjectWrapper>();
            script.Globals["InputManager"] = UserData.CreateStatic<LUA_InputManager>();
            script.Globals["GUI"] = UserData.CreateStatic<LUA_GUIWrapper>();
            script.Globals["Time"] = UserData.CreateStatic<Time>();

            // Enums
            script.Globals["PrimitiveType"] = UserData.CreateStatic<PrimitiveType>();


            // Methods
            script.Globals["print"] = (Func<object, bool>)CustomLua.LUA_print;
            script.Globals["printerr"] = (Func<object, bool>)CustomLua.LUA_printerr;
            script.Globals["loadstring"] = (Func<string, bool>)CustomLua.LUA_loadstring;

            // Structs
            script.Globals["Mathf"] = new Mathf();
            script.Globals["Vector2"] = UserData.CreateStatic<LUA_Vector2Wrapper>();
            script.Globals["Vector3"] = UserData.CreateStatic<LUA_Vector3Wrapper>();
            script.Globals["Vector4"] = UserData.CreateStatic<LUA_Vector4Wrapper>();
            script.Globals["Quaternion"] = UserData.CreateStatic<LUA_QuaternionWrapper>();

            script.Globals["Rect"] = UserData.CreateStatic<LUA_RectWrapper>();

            // Constant game instances
            script.Globals["_GorillaBattleManager"] = GorillaBattleManager.instance;
            script.Globals["_PhotonNetworkController"] = PhotonNetworkController.Instance;
            script.Globals["_GorillaPlayer"] = GorillaLocomotion.Player.Instance;
            script.Globals["_GorillaDayNight"] = GorillaDayNight.instance;
            script.Globals["_GorillaComputer"] = GorillaComputer.instance;
            script.Globals["_GorillaParent"] = GorillaParent.instance;
            script.Globals["_BetterDayNightManager"] = BetterDayNightManager.instance;

            // Glboals
            script.Globals["_G.Author"] = new string('.', 0);

            #endregion
        }

        public static void RunCode(string code)
        {
            Script newScript = new Script();
            InitScript(newScript);

            try
            {
                try
                {
                    DynValue loopCourrutine = null;

                    DynValue status = newScript.DoString(code);

                    if (code.Contains("function _loop()"))
                        loopCourrutine = newScript.Globals.Get("_loop");

                    if (loopCourrutine != null && loopCourrutine.Type == DataType.Function)
                    {
                        loopCourrutine = newScript.CreateCoroutine(loopCourrutine);
                        loopCourrutine.Coroutine.AutoYieldCounter = 100;

                        loopCoroutines.Add(loopCourrutine);
                    }
                }
                catch (ScriptRuntimeException ex)
                {
                    Debug.LogError("ScriptRuntimeException => " + ex.DecoratedMessage);
                }
            }
            catch (SyntaxErrorException ex)
            {
                Debug.LogError("SyntaxErrorException => " + ex.DecoratedMessage);
            }

            loadedScripts.Add(newScript);
        }
    }
}
