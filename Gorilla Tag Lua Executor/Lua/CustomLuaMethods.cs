using MoonSharp.Interpreter;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Gorilla_Tag_Lua_Executor.Lua
{
    public static class CustomLua
    {
        public class LUA_MouseWrapper
        {
            public bool leftButtonPressed = Mouse.current.leftButton.isPressed;
            public bool rightButtonPressed = Mouse.current.rightButton.isPressed;
            public Vector2 position = Mouse.current.position.value;

            public static LUA_MouseWrapper Get() { return new LUA_MouseWrapper(); }
        }

        public class LUA_ControllerInput
        {
            public bool isHoldingRightGrip;
            public bool isHoldingLeftGrip;
            public bool isHoldingRightTrigger;
            public bool isHoldingLeftTrigger;

            public float rightGripValue;
            public float leftGripValue;
            public float rightTriggerValue;
            public float leftTriggerValue;
        }

        public class LUA_InputManager
        {
            public static LUA_ControllerInput GetControllerInput()
            {
                bool isHoldingRightGrip;
                bool isHoldingLeftGrip;
                bool isHoldingRightTrigger;
                bool isHoldingLeftTrigger;

                float rightGripValue;
                float leftGripValue;
                float rightTriggerValue;
                float leftTriggerValue;

                List<UnityEngine.XR.InputDevice> leftList = new List<UnityEngine.XR.InputDevice>();
                List<UnityEngine.XR.InputDevice> rightList = new List<UnityEngine.XR.InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, leftList);
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightList);

                rightList[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out isHoldingRightGrip);
                leftList[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out isHoldingLeftGrip);
                rightList[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out isHoldingRightTrigger);
                leftList[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out isHoldingLeftTrigger);

                rightList[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out rightGripValue);
                leftList[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out leftGripValue);
                rightList[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out rightTriggerValue);
                leftList[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out leftTriggerValue);

                var input = new LUA_ControllerInput
                {
                    isHoldingRightGrip = isHoldingRightGrip,
                    isHoldingLeftGrip = isHoldingLeftGrip,
                    isHoldingRightTrigger = isHoldingRightTrigger,
                    isHoldingLeftTrigger = isHoldingLeftTrigger,

                    rightGripValue = rightGripValue,
                    leftGripValue = leftGripValue,
                    rightTriggerValue = rightTriggerValue,
                    leftTriggerValue = leftTriggerValue,
                };

                return input;
            }

            public static LUA_MouseWrapper GetMouse() => LUA_MouseWrapper.Get();
        }

        public class LUA_GameObjectWrapper
        {
            public static GameObject   Find(string n) => GameObject.Find(n);
            public static Object       FindFirstObjectByType(System.Type t) => GameObject.FindFirstObjectByType(t);
            public static Object       FindObjectOfType(System.Type t) => GameObject.FindObjectOfType(t);
            public static Object[]     FindObjectsOfType(System.Type t) => GameObject.FindObjectsOfType(t);
            public static Object       FindAnyObjectByType(System.Type t) => GameObject.FindAnyObjectByType(t);
            public static GameObject   FindGameObjectWithTag(string t) => GameObject.FindGameObjectWithTag(t);
            public static GameObject[] FindGameObjectsWithTag(string t) => GameObject.FindGameObjectsWithTag(t);
            public static void         Destroy(GameObject o) => GameObject.Destroy(o);
            public static void         Destroy(GameObject o, float t) => GameObject.Destroy(o, t);
            public static void         DestroyImmediate(GameObject o, bool aDA) => GameObject.DestroyImmediate(o, aDA);
            public static void         CreatePrimitive(PrimitiveType t) => GameObject.CreatePrimitive(t);
            public static void         Instantiate(GameObject o, Vector3 p, Quaternion r) => GameObject.Instantiate(o, p, r);
            public static void         DontDestroyOnLoad(GameObject o) => GameObject.DontDestroyOnLoad(o);

            public static GameObject New() => new GameObject();
            public static GameObject New(string n) => new GameObject(n);
            public static GameObject New(string n, params System.Type[] t) => new GameObject(n, t);
        }

        public class LUA_Vector4Wrapper
        {
            public static Vector4 one => Vector4.one;
            public static Vector4 zero => Vector4.zero;
            public static Vector4 negativeInfinity => Vector4.negativeInfinity;
            public static Vector4 positiveInfinity => Vector4.positiveInfinity;
            public static float   kEpsilon => Vector4.kEpsilon;

            public static float   Distance(Vector4 f, Vector4 t) => Vector4.Distance(f, t);
            public static float   Dot(Vector4 l, Vector4 r) => Vector4.Dot(l, r);
            public static Vector4 Lerp(Vector4 f, Vector4 t, float time) => Vector4.Lerp(f, t, time);
            public static Vector4 LerpUnclamped(Vector4 f, Vector4 t, float time) => Vector4.LerpUnclamped(f, t, time);
            public static float   Magnitude(Vector4 v) => Vector4.Magnitude(v);
            public static Vector4 Max(Vector4 lhs, Vector4 rhs) => Vector4.Max(lhs, rhs);
            public static Vector4 Min(Vector4 lhs, Vector4 rhs) => Vector4.Min(lhs, rhs);
            public static Vector4 MoveTowards(Vector4 c, Vector4 t, float mDD) => Vector4.MoveTowards(c, t, mDD);
            public static Vector4 Normalize(Vector4 v) => Vector4.Normalize(v);
            public static Vector4 Project(Vector4 a, Vector4 b) => Vector4.Project(a, b);
            public static float   SqrMagnitude(Vector4 v) => Vector4.SqrMagnitude(v);
            public static Vector4 SqrMagnitude(Vector4 a, Vector4 b) => Vector4.Scale(a, b);

            public static Vector4 New() => new Vector4();
            public static Vector4 New(float x, float y, float z) => new Vector4(x, y, z);
            public static Vector4 New(float x, float y, float z, float w) => new Vector4(x, y, z, w);
        }

        public class LUA_Vector3Wrapper
        {
            public static Vector3 up => Vector3.up;
            public static Vector3 down => Vector3.down;
            public static Vector3 right => Vector3.right;
            public static Vector3 left => Vector3.left;
            public static Vector3 one => Vector3.one;
            public static Vector3 zero => Vector3.zero;
            public static Vector3 back => Vector3.back;
            public static Vector3 forward => Vector3.forward;
            public static Vector3 negativeInfinity => Vector3.negativeInfinity;
            public static Vector3 positiveInfinity => Vector3.positiveInfinity;
            public static float   kEpsilon => Vector3.kEpsilon;
            public static float   kEpsilonNormalSqrt => Vector3.kEpsilonNormalSqrt;

            public static float   Distance(Vector3 f, Vector3 t) => Vector3.Distance(f, t);
            public static float   Angle(Vector3 f, Vector3 t) => Vector3.Angle(f, t);
            public static Vector3 ClampMagnitude(Vector3 v, float m) => Vector3.ClampMagnitude(v, m);
            public static Vector3 Cross(Vector3 l, Vector3 r) => Vector3.Cross(l, r);
            public static float   Dot(Vector3 l, Vector3 r) => Vector3.Dot(l, r);
            public static Vector3 Lerp(Vector3 f, Vector3 t, float time) => Vector3.Lerp(f, t, time);
            public static Vector3 LerpUnclamped(Vector3 f, Vector3 t, float time) => Vector3.LerpUnclamped(f, t, time);
            public static float   Magnitude(Vector3 v) => Vector3.Magnitude(v);
            public static Vector3 Max(Vector3 lhs, Vector3 rhs) => Vector3.Max(lhs, rhs);
            public static Vector3 Min(Vector3 lhs, Vector3 rhs) => Vector3.Min(lhs, rhs);
            public static Vector3 MoveTowards(Vector3 c, Vector3 t, float mDD) => Vector3.MoveTowards(c, t, mDD);
            public static Vector3 Normalize(Vector3 v) => Vector3.Normalize(v);
            public static void    OrthoNormalize(ref Vector3 n, ref Vector3 t) => Vector3.OrthoNormalize(ref n, ref t);
            public static Vector3 Project(Vector3 v, Vector3 oN) => Vector3.Project(v, oN);
            public static Vector3 ProjectOnPlane(Vector3 v, Vector3 pN) => Vector3.ProjectOnPlane(v, pN);
            public static Vector3 Reflect(Vector3 iD, Vector3 iN) => Vector3.Reflect(iD, iN);
            public static Vector3 RotateTowards(Vector3 c, Vector3 t, float mRD, float mMD) => Vector3.RotateTowards(c, t, mRD, mMD);
            public static Vector3 Slerp(Vector3 a, Vector3 b, float time) => Vector3.Slerp(a, b, time);
            public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float time) => Vector3.SlerpUnclamped(a, b, time);
            public static Vector3 SmoothDamp(Vector3 c, Vector3 t, ref Vector3 cV, float sV, float sT, float mS) => Vector3.SmoothDamp(c, t, ref cV, sV, sT, mS);
            public static float   SqrMagnitude(Vector3 v) => Vector3.SqrMagnitude(v);

            public static Vector3 New() => new Vector3();
            public static Vector3 New(float x, float y, float z) => new Vector3(x, y, z);
        }

        public class LUA_Vector2Wrapper
        {
            public static Vector2 up => Vector2.up;
            public static Vector2 down => Vector2.down;
            public static Vector2 right => Vector2.right;
            public static Vector2 left => Vector2.left;
            public static Vector2 one => Vector2.one;
            public static Vector2 zero => Vector2.zero;
            public static Vector2 negativeInfinity => Vector2.negativeInfinity;
            public static Vector2 positiveInfinity => Vector2.positiveInfinity;
            public static float   kEpsilon => Vector2.kEpsilon;
            public static float   kEpsilonNormalSqrt => Vector2.kEpsilonNormalSqrt;

            public static float   Distance(Vector2 f, Vector2 t) => Vector2.Distance(f, t);
            public static float   Angle(Vector2 f, Vector2 t) => Vector2.Angle(f, t);
            public static Vector2 ClampMagnitude(Vector2 v, float m) => Vector2.ClampMagnitude(v, m);
            public static float   Dot(Vector2 l, Vector2 r) => Vector2.Dot(l, r);
            public static Vector2 Lerp(Vector2 f, Vector2 t, float time) => Vector2.Lerp(f, t, time);
            public static Vector2 LerpUnclamped(Vector2 f, Vector2 t, float time) => Vector2.LerpUnclamped(f, t, time);
            public static Vector2 Max(Vector2 lhs, Vector2 rhs) => Vector2.Max(lhs, rhs);
            public static Vector2 Min(Vector2 lhs, Vector2 rhs) => Vector2.Min(lhs, rhs);
            public static Vector2 MoveTowards(Vector2 c, Vector2 t, float mDD) => Vector2.MoveTowards(c, t, mDD);
            public static Vector2 Normalize(Vector2 v) => Vector3.Normalize(v);
            public static Vector2 Reflect(Vector2 iD, Vector2 iN) => Vector2.Reflect(iD, iN);
            public static Vector2 SmoothDamp(Vector2 c, Vector2 t, ref Vector2 cV, float sV, float sT, float mS) => Vector2.SmoothDamp(c, t, ref cV, sV, sT, mS);
            public static float   SqrMagnitude(Vector2 v) => Vector2.SqrMagnitude(v);
            public static Vector2 Perpendicular(Vector2 iD) => Vector2.Perpendicular(iD);
            public static float   SignedAngle(Vector2 f, Vector2 t) => Vector2.SignedAngle(f, t);

            public static Vector2 New() => new Vector2();
            public static Vector2 New(float x, float y) => new Vector2(x, y);
        }


        public class LUA_QuaternionWrapper
        {
            public Quaternion identity => Quaternion.identity;
            public float kEpsilon => Quaternion.kEpsilon;

            public static Quaternion Inverse(Quaternion r) => Quaternion.Inverse(r);
            public static float      Angle(Quaternion a, Quaternion b) => Quaternion.Angle(a, b);
            public static Quaternion Angle(float a, Vector3 axis) => Quaternion.AngleAxis(a, axis);
            public static float      Dot(Quaternion a, Quaternion b) => Quaternion.Dot(a, b);
            public static Quaternion Euler(float x, float y, float z) => Quaternion.Euler(x, y, z);
            public static Quaternion Euler(Vector3 v) => Quaternion.Euler(v);
            public static Quaternion FromToRotation(Vector3 fD, Vector3 tD) => Quaternion.FromToRotation(fD, tD);
            public static Quaternion Lerp(Quaternion a, Quaternion b, float t) => Quaternion.Lerp(a, b, t);
            public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t) => Quaternion.LerpUnclamped(a, b, t);
            public static Quaternion LookRotation(Vector3 f, Vector3 u) => Quaternion.LookRotation(f, u);
            public static Quaternion Normalize(Quaternion q) => Quaternion.Normalize(q);
            public static Quaternion RotateTowards(Quaternion f, Quaternion t, float MDD) => Quaternion.RotateTowards(f, t, MDD);
            public static Quaternion Slerp(Quaternion a, Quaternion b, float t) => Quaternion.Slerp(a, b, t);
            public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t) => Quaternion.SlerpUnclamped(a, b, t);

            public static Quaternion New() => new Quaternion();
            public static Quaternion New(float x, float y, float z, float w) => new Quaternion(x, y, z, w);
        }

        public static bool LUA_print(object msg)
        {
            Debug.Log(msg);

            return true;
        }

        public static bool LUA_printerr(object msg)
        {
            Debug.LogError(msg);

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
