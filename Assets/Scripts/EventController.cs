using UnityEngine;

public class EventController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.L))
	    {
	        LetThereBeLight();
	    }
	}

    void LetThereBeLight()
    {
        RenderSettings.ambientIntensity = 1;
        RenderSettings.reflectionIntensity = 1;
        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Skybox;

        GameObject.Find("frau_2").SendMessage("dubistneganzliebedrumwinkmalbitte");
    }
}
