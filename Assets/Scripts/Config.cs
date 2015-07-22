using System.Collections.Generic;

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

    static Config()
    {
        GameStates = new Dictionary<string, bool>();
        GameStates.Add("gameStarted", true);
    }
}
