using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private GameObject[] sides = new GameObject[6];

    private InputGamepad inputGamepad;

	// Use this for initialization
    private void Start()
    {
        Config.World = this.gameObject;

        inputGamepad = GameObject.Find("CardboardMain").GetComponent<InputGamepad>();

        sides[0] = Instantiate(Resources.Load("Prefabs/Gebirge") as GameObject);
        sides[0].transform.parent = this.transform;
        sides[0].name = "Gebirge";

        sides[1] = Instantiate(Resources.Load("Prefabs/Dorf") as GameObject);
        sides[1].transform.parent = this.transform;
        sides[1].name = "Dorf";

        sides[2] = Instantiate(Resources.Load("Prefabs/Eis") as GameObject);
        sides[2].transform.parent = this.transform;
        sides[2].name = "Eis";

        sides[3] = Instantiate(Resources.Load("Prefabs/Feuer") as GameObject);
        sides[3].transform.parent = this.transform;
        sides[3].name = "Feuer";

        sides[4] = Instantiate(Resources.Load("Prefabs/Wueste") as GameObject);
        sides[4].transform.parent = this.transform;
        sides[4].name = "Wueste";

        sides[5] = Instantiate(Resources.Load("Prefabs/Wald") as GameObject);
        sides[5].transform.parent = this.transform;
        sides[5].name = "Wald";

        for (int i = 0; i < sides.Length; i++)
        {
            if (sides[i] == null)
            {
                sides[i] = Instantiate(Resources.Load("Prefabs/DummySide" + (i)) as GameObject);
                sides[i].transform.parent = this.transform;
                sides[i].name = "Side" + i;
            }
        }

        //Front Side
        sides[0].transform.position = new Vector3(-9.8f, -27, 67.7f);
        sides[0].transform.Rotate(new Vector3(-90, 0, 0));

        //Back Side
        sides[1].transform.position = new Vector3(0, 0, -100);
        sides[1].transform.Rotate(new Vector3(90, 0, 0));

        //Right
        sides[2].transform.position = new Vector3(100, 0, 0);
        sides[2].transform.Rotate(new Vector3(0, 0, 90));

        //Left
        sides[3].transform.position = new Vector3(-150, -6, 176);
        sides[3].transform.Rotate(new Vector3(0, 0, -90));

        //Top
        sides[4].transform.position = new Vector3(0, 100, 0);
        sides[4].transform.Rotate(180, 0, 0);

        //Bottom
        sides[5].transform.position = new Vector3(0, -100, 0);
    }

    // Update is called once per frame
	void Update () {
//        this.transform.Rotate(inputGamepad.JoystickAxes["LS Y"], inputGamepad.JoystickAxes["LS X"], 0);
    
    }

    private void OnGUI()
    {
        if (Debug.isDebugBuild && Config.DebugGamestate)
        {
            GUILayout.BeginArea(new Rect(50.0f, 50.0f, Screen.width - 50.0f, Screen.height - 50.0f));
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200.0f));
                {
                    foreach (KeyValuePair<string, bool> state in Config.GameStates)
                    {
                        GUILayout.Label(state.Key + ": " + state.Value);
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
    }

}
