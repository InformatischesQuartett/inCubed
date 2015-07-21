using System.Collections.Generic;
using UnityEngine;

public class FlickerLightTest : MonoBehaviour
{
    private int direction;
    private float intensity;
    private float increment;


	// Use this for initialization
	void Start ()
	{
	    intensity = 0;
        direction = 1;
	    increment = 0.01f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    intensity = intensity + (increment*direction);

	    if (intensity <= 0)
	    {
	        intensity = 0;
	        direction = 1;
	    }
	    else if (intensity > 2)
	    {
	        intensity = 2;
	        direction = -1;
	    }

        this.transform.GetComponent<Light>().intensity = intensity;


	}

    private void OnGUI()
    {
        if (Debug.isDebugBuild && Config.DebugLight)
        {
            GUILayout.BeginArea(new Rect(50.0f, 50.0f, Screen.width - 50.0f, Screen.height - 50.0f));
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200.0f));
                {

                    GUILayout.Label("Light Intensity: " + intensity);

                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
    }

}
