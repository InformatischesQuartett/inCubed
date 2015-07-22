using UnityEngine;

public class TravelTarget : MonoBehaviour {

    public string[] dependsOnGamestate;

    private bool ready;
    private bool isCenterTrigger;
    private GameObject parent;

	// Use this for initialization
	void Start () {
        this.GetComponent<Collider>().enabled = false;
	    this.GetComponent<Renderer>().enabled = false;

        if (this.transform.parent.name == "centerTrigger")
        {
            isCenterTrigger = true;
        }
        CheckForGamestates();
        ActivateTrigger();
	}
	
	// Update is called once per frame
	void Update () {
        CheckForGamestates();

        if (isCenterTrigger)
        {
            this.GetComponent<Collider>().enabled = true;
            this.GetComponent<Renderer>().enabled = true;
        }
	
	}
    private void EventTrigger()
    {
        //this.GetComponent<Collider>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").SendMessage("Travel");

        Config.World.BroadcastMessage("DeactivateTrigger");
        if (!isCenterTrigger)
        {
            parent.SendMessage("ActivateTriggers");
        }
        else
        {
            Config.World.BroadcastMessage("ActivateTravelTriggers");
        }
    }

    private void DeactivateTrigger()
    {
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Renderer>().enabled = false;
    }

    private void ActivateTrigger()
    {
        if (ready)
        {
            this.GetComponent<Collider>().enabled = true;
            this.GetComponent<Renderer>().enabled = true;
        }
    }

    private void ActivateTravelTriggers()
    {
        ActivateTrigger();
    }

    private void RegisterTrigger(GameObject sender)
    {
        parent = sender;
        sender.SendMessage("TriggerReg", this.gameObject);
    }

    private void CheckForGamestates()
    {
        if (!ready)
        {
            ready = true;
            foreach (string state in dependsOnGamestate)
            {
                if (!Config.GameStates[state])
                {
                    ready = false;
                }
            }

            /*if (ready)
            {
                this.GetComponent<Collider>().enabled = true;
                this.GetComponent<Renderer>().enabled = true;
            }*/
        }
    }
}
