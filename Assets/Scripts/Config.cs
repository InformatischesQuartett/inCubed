using System.Collections.Generic;
using UnityEngine;

public enum SHAPETYPE
{
    None = -1,
    Triangle,
    Rectangle,
    Pentagon,
    Heptagon,
    Circle,
    Star
}

public static class Config
{
    public static Dictionary<string, bool> GameStates { get; set; }

    public static bool DebugController = false;
    public static bool DebugLight = false;
    public static bool DebugRayTrigger = true;
    public static bool ShowFps = true;

    public static float triggerTimer = 5f;
    public static float travelTime = 5f;

    public static AndroidJavaObject OpenCVPlugin;
    public static AndroidJavaClass PluginClass;

    public static SHAPETYPE LastShape;
    public static Vector2 LastShapePos;

    public static byte[] CamData;
    public static float CamDataUpdate;

    static Config()
    {
        GameStates = new Dictionary<string, bool>();
        GameStates.Add("gameStarted", true);

#if !UNITY_EDITOR
        PluginClass = new AndroidJavaClass("com.Company.inCubed.CamHandler");
        OpenCVPlugin = PluginClass.CallStatic<AndroidJavaObject>("instance");
#endif

        CamDataUpdate = -1;
    }

    public static void NewData()
    {
        if (OpenCVPlugin != null)
        {
            CamData = OpenCVPlugin.Call<byte[]>("GetBuffer");
            CamDataUpdate = Time.realtimeSinceStartup;
        }
    }

    public static void ShapeDetected(string shape)
    {
        LastShape = (SHAPETYPE)int.Parse(shape);

        if (OpenCVPlugin != null)
        {
            var shapePos = OpenCVPlugin.Call<double[]>("GetShapePosition");
            LastShapePos = new Vector2((float)shapePos[0], (float)shapePos[1]);
        }
    }
}
