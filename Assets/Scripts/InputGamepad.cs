using UnityEngine;
using System.Collections.Generic;

public class InputGamepad : MonoBehaviour
{
    public Dictionary<string, bool> JoystickButtons { get; private set; }
    public Dictionary<string, float> JoystickAxes { get; private set; }

    // Use this for initialization
	void Start ()
	{
	    JoystickButtons = new Dictionary<string, bool>();
        JoystickAxes = new Dictionary<string, float>();

        JoystickButtons.Add("A", false);
        JoystickButtons.Add("B", false);
        JoystickButtons.Add("X", false);
        JoystickButtons.Add("Y", false);
        JoystickButtons.Add("LB", false);
        JoystickButtons.Add("RB", false);
	    JoystickButtons.Add("LS", false);
        JoystickButtons.Add("RS", false);
        JoystickButtons.Add("Start", false);
        JoystickButtons.Add("Select", false);

        JoystickAxes.Add("LS X", 0.0f);
        JoystickAxes.Add("LS Y", 0.0f);
        JoystickAxes.Add("RS X", 0.0f);
        JoystickAxes.Add("RS Y", 0.0f);
        JoystickAxes.Add("DPAD X", 0.0f);
        JoystickAxes.Add("DPAD Y", 0.0f);
        JoystickAxes.Add("RT", 0.0f);
        JoystickAxes.Add("LT", 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        JoystickButtons["A"] =      Input.GetKey(KeyCode.JoystickButton0); //A
        JoystickButtons["B"] =      Input.GetKey(KeyCode.JoystickButton1); //B
        JoystickButtons["X"] =      Input.GetKey(KeyCode.JoystickButton2); //X
        JoystickButtons["Y"] =      Input.GetKey(KeyCode.JoystickButton3); //Y
        JoystickButtons["LB"] =     Input.GetKey(KeyCode.JoystickButton4); //LB
        JoystickButtons["RB"] =     Input.GetKey(KeyCode.JoystickButton5); //RB
        JoystickButtons["LS"] =     Input.GetKey(KeyCode.JoystickButton8); //LS
        JoystickButtons["RS"] =     Input.GetKey(KeyCode.JoystickButton9); //RS
        JoystickButtons["Start"] =  Input.GetKey(KeyCode.JoystickButton10); //Start
        JoystickButtons["Select"] = Input.GetKey(KeyCode.JoystickButton11); //Select

        JoystickAxes["LS X"] =      Input.GetAxis("Joystick Axis 1"); //LS X
        JoystickAxes["LS Y"] =      Input.GetAxis("Joystick Axis 2"); //LS Y
        JoystickAxes["RS X"] =      Input.GetAxis("Joystick Axis 3"); //RS X
        JoystickAxes["RS Y"] =      Input.GetAxis("Joystick Axis 4"); //RS Y
        JoystickAxes["DPAD X"] =    Input.GetAxis("Joystick Axis 5"); //DPAD X
        JoystickAxes["DPAD Y"] =    Input.GetAxis("Joystick Axis 6"); //DPAD Y
        JoystickAxes["RT"] =        Input.GetAxis("Joystick Axis 12"); //RT
        JoystickAxes["LT"] =        Input.GetAxis("Joystick Axis 13"); //LT
	}

    private void OnGUI()
    {
        if (Debug.isDebugBuild && Config.DebugController)
        {
            GUILayout.BeginArea(new Rect(50.0f, 50.0f, Screen.width - 50.0f, Screen.height - 50.0f));
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200.0f));
                {
                    foreach (KeyValuePair<string, bool> buttonState in JoystickButtons)
                    {
                        GUILayout.Label(buttonState.Key + ": " + buttonState.Value);
                    }
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical(GUILayout.Width(200.0f));
                {
                    foreach (KeyValuePair<string, float> axisState in JoystickAxes)
                    {
                        GUILayout.Label(axisState.Key + ": " + axisState.Value);
                    }
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical(GUILayout.Width(200.0f));
                {
                    foreach (var stick in Input.GetJoystickNames())
                    {
                        GUILayout.Label("Stick: " + stick);
                    }

                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
    }
}
