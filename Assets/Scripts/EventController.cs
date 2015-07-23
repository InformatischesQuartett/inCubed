using UnityEngine;

public class EventController : MonoBehaviour
{

    public GameObject uicanvas;

	// Use this for initialization
	void Start ()
	{
	    uicanvas = GameObject.Find("UICanvas");
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.L))
	    {
	        FirstCube();

	    }
	}

    void LetThereBeLight()
    {
        RenderSettings.ambientIntensity = 1;
        RenderSettings.reflectionIntensity = 1;
        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Skybox;

        GameObject.Find("frau_2").SendMessage("dubistneganzliebedrumwinkmalbitte");
    }

    void FirstCube()
    {
        uicanvas.SendMessage("Display", 2);
        GameObject.Find("frau_2").GetComponent<AudioSource>().Play();
    }

    void GebirgeGrav()
    {
    GameObject.Find("kasten_gravinator_gebirge").GetComponent<AudioSource>().Play();
    }
}
