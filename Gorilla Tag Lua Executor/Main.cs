/*
 * 
 * 
 * TODO:
 * FIx THIS FUCKONG ISSUE WERE _loop KEEPS ITS FUCKING VALUE AFTER BEING SET TO NULL GRRRRAAAHHHHH 🦅🦅🦅🦅🦅
 * 
 * 
 * 
 * 
 */






using BepInEx;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using MoonSharp.Interpreter;
using Gorilla_Tag_Lua_Executor.Lua;
using Microsoft.SqlServer.Server;
using UnityEngine.InputSystem;
using OculusSampleFramework;
using TMPro;

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
        private Rect executorWindowRect = new Rect(20, 20, 500, 450);

        private GUIStyle textAreaStyle = new GUIStyle();
        private GUIStyle windowStyle = new GUIStyle();
        private GUIStyle buttonStyle = new GUIStyle();

        public string code = @"-- https://github.com/0xVidde/Gorilla-Tag-Lua-Executor";

        public MainMod()
        {
            this.textAreaStyle.normal.background = MakeTexture(2, 2, new Color(0.1f, 0.1f, 0.1f, 1f));
            this.textAreaStyle.normal.textColor = Color.white;
            this.textAreaStyle.alignment = TextAnchor.UpperLeft;
            this.textAreaStyle.clipping = TextClipping.Clip;
            this.textAreaStyle.border = new RectOffset(8, 8, 8, 8);

            this.windowStyle.normal.background = MakeTexture(2, 2, new Color(0.05f, 0.05f, 0.05f, 1));
            this.windowStyle.normal.textColor = Color.white;
            this.windowStyle.alignment = TextAnchor.UpperCenter;
            this.windowStyle.clipping = TextClipping.Clip;
            this.windowStyle.border = new RectOffset(8, 8, 8, 8);

            this.buttonStyle.normal.background = MakeTexture(2, 2, new Color(0.1f, 0.1f, 0.1f, 1f));
            this.buttonStyle.normal.textColor = Color.white;
            this.buttonStyle.alignment = TextAnchor.MiddleCenter;
            this.buttonStyle.clipping = TextClipping.Clip;
            this.buttonStyle.border = new RectOffset(8, 8, 8, 8);
        }

        private Texture2D MakeTexture(int width, int height, Color col)
        {
            Color[] array = new Color[width * height];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = col;
            }
            Texture2D texture2D = new Texture2D(width, height);
            texture2D.SetPixels(array);
            texture2D.Apply();
            return texture2D;
        }

        public void Start()
        {
            LuaInterface.InitLuaEngine();
        } 

        public void OnGUI()
        {
            executorWindowRect = GUI.Window(0, executorWindowRect, ExecutorWindow, "", windowStyle);
        }

        float damping = 6.5f;
        bool dragging = false;
        bool wasHoldingLastFrame = false;
        Vector2 initialMouseOffset = new Vector2();
        public void HandleCustomMovement(ref Rect moveRect, Rect detectionRect)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 sanitizedMousePosition = new Vector2(mousePosition.x, Screen.height - mousePosition.y);

            if (!dragging && Mouse.current.leftButton.isPressed && detectionRect.Contains(sanitizedMousePosition) && !wasHoldingLastFrame)
                initialMouseOffset = detectionRect.position - sanitizedMousePosition;

            if (Mouse.current.leftButton.isPressed && detectionRect.Contains(sanitizedMousePosition) && !wasHoldingLastFrame)
                dragging = true;

            if (!Mouse.current.leftButton.isPressed)
                dragging = false;

            Vector2 targetPosition = sanitizedMousePosition + initialMouseOffset;

            if (dragging)
            {
                moveRect.position = Vector2.Lerp(moveRect.position, targetPosition, Time.deltaTime * damping);
            }

            wasHoldingLastFrame = Mouse.current.leftButton.isPressed;
        }

        public void RenderTitleBar(string title, Rect rect)
        {
            GUI.Label(new Rect((rect.width / 2) - (title.Length * 3.5f), 2, 100, 100), title);
        }

        void ExecutorWindow(int windowID)
        {
            RenderTitleBar("Ormbunke x64", executorWindowRect);

            code = @"" + GUI.TextArea(new Rect(5, 23, 490, 400), code, textAreaStyle);

            if (GUI.Button(new Rect(5, 426, 490, 20), "Run Code", buttonStyle))
            {
                LuaInterface.RunCode(code);

                Debug.Log("Ormbunke => Ran Code!");
            }

            foreach (DynValue coroutine in LuaInterface.loopCoroutines)
            {
                coroutine.Coroutine.Resume();
            }

            HandleCustomMovement(ref executorWindowRect, new Rect(executorWindowRect.x, executorWindowRect.y, executorWindowRect.width, 20));
        }
    }
}
