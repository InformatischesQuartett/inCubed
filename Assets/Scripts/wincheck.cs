using UnityEngine;
using System.Collections;

public class wincheck : MonoBehaviour
{

    private float timer =5;

	// Use this for initialization
	void Start () {
	    //Config.PauseOpenCV();
	}
	
	// Update is called once per frame
	void Update () {
        if (Config.GameStates["firstCubeTaken"] && Config.GameStates["secondCubeTaken"] && Config.GameStates["thirdCubeTaken"])
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Application.LoadLevel("Outro");
            }
        }

	}
}
