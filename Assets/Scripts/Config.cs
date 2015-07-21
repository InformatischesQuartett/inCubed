﻿using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Animations;

public static class Config
{
    public static Dictionary<string, bool> GameStates { get; set; }

    public static bool DebugController = false;
    public static bool DebugLight = false;
    public static bool DebugRayTrigger = true;

    public static float triggerTimer = 5f;


    static Config()
    {
        //initialize GameStates
        GameStates = new Dictionary<string, bool>();
        GameStates.Add("gameStarted", true);
    }
}
