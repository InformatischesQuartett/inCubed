using System.Collections.Generic;

public static class Config
{
    public static Dictionary<string, bool> GameStates { get; set; }

    static Config()
    {
        //initialize GameStates
        GameStates = new Dictionary<string, bool>();
        GameStates.Add("gameStarted", false);
    }
}
