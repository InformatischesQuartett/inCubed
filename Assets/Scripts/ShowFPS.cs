using UnityEngine;
using System.Collections;

public class ShowFPS : MonoBehaviour {
    private void OnGUI()
    {
        if (Debug.isDebugBuild && Config.ShowFps)
        {
            GUILayout.BeginArea(new Rect(50.0f, 50.0f, Screen.width - 50.0f, Screen.height - 50.0f));
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200.0f));
                {

                    GUILayout.Label("FPS: " + (1/Time.deltaTime));

                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
    }
}
