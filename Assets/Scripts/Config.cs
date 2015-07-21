using System.Collections.Generic;
using System.Diagnostics;

public static class Config
{
    public static Dictionary<string, bool> GameStates { get; set; }

    public static bool DebugController = false;
    public static bool DebugLight = false;


    static Config()
    {
        //initialize GameStates
        GameStates = new Dictionary<string, bool>();
        GameStates.Add("gameStarted", false);
    }
}
