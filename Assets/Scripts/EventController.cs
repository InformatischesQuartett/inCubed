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
	        LetThereBeLight();
	        Config.GameStates["crystalActivated"] = true;
            Config.GameStates["shovelTaken"] = true;
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
        uicanvas.SendMessage("Display", 3);
        GameObject.Find("frau_2").GetComponent<AudioSource>().Play();
    }

    void GebirgeGrav()
    {
        GameObject.Find("kasten_gravinator_gebirge").GetComponent<AudioSource>().Play();
    }

    void WuesteGrav()
    {
        uicanvas.SendMessage("Display", 5);
    }

    void FeuerGrav()
    {
        uicanvas.SendMessage("Display", 4);
    }

    void SchaufelShow()
    {
        uicanvas.SendMessage("Display", 1);
        //Config.ResumeOpenCV();
    }

    void StartBurn()
    {
        GameObject.Find("ParticleVolc").GetComponent<ParticleSystem>().Play();
        GameObject.Find("ParticleHouse").GetComponent<ParticleSystem>().Play();
        GameObject.Find("mann1").SendMessage("panic");
    }

    void UITorch()
    {
        uicanvas.SendMessage("Display", 2);
    }

    void StopBurn()
    {
        Destroy(GameObject.Find("ParticleHouse"));
        GameObject.Find("mann1").SendMessage("dubistneganzliebedrumwinkmalbitte");
    }
}
