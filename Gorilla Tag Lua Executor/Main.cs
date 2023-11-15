using BepInEx;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace Gorilla_Tag_Lua_Executor
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Main : BaseUnityPlugin
    {
        private const string modGUID = "GT.Lua.Exec";
        private const string modName = "Ormbunke Executor";
        private const string modVersion = "0.0.2";

        public void Awake()
        {
            var harmony = new Harmony(modGUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("FixedUpdate", MethodType.Normal)]
    public class MainPatch
    {
        static void Prefix(GorillaLocomotion.Player __instance)
        {
            if (!GameObject.Find("Ormbunke Executor"))
            {
                GameObject mod = new GameObject("Ormbunke Executor");
                mod.AddComponent<MainMod>();
            }
        }
    }

    public class MainMod : MonoBehaviour
    {
        private Rect windowRect = new Rect(20, 20, 500, 450);

        public string code = @"-- https://github.com/0xVidde/Gorilla-Tag-Lua-Executor
-- By Vidde

-- Example
local tempObj = GameObject:New('Example Object')
local dist = Vector3:Distance(tempObj.transform.position, _GorillaPlayer.transform.position)

print(dist)

GameObject:Destroy(tempObj, 1)";

        public void Start()
        {
            Lua.LuaInterface.InitLua();
        }

        public void OnGUI()
        {
            windowRect = GUI.Window(0, windowRect, DoWindow, "Ormbunke");
        }

        void DoWindow(int windowID)
        {
            code = GUI.TextArea(new Rect(5, 23, 490, 400), code);

            code = @"" + code;

            if (GUI.Button(new Rect(5, 426, 490, 20), "Run Code"))
            {
                Lua.LuaInterface.RunCode(code);

                Debug.Log("Ran Code!");
            }

            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }
    }
}