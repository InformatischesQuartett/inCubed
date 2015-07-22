using UnityEngine;

public class TravelTarget : MonoBehaviour {

    public string[] dependsOnGamestate;

    private bool ready;

	// Use this for initialization
	void Start () {
        this.GetComponent<Collider>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
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

            if (ready)
            {
                this.GetComponent<Collider>().enabled = true;
            }
        }
	
	}
    private void EventTrigger()
    {
        //this.GetComponent<Collider>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").SendMessage("Travel");
        //DoTravel
    }
}
