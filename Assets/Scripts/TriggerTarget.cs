using UnityEngine;

public class TriggerTarget : MonoBehaviour
{
    public string[] changesGamestate;
    public string[] dependsOnGamestate;
    public GameObject winParticle;

    private bool ready;
    private bool triggered;

    private GameObject parent;

    private void EventTrigger()
    {
        this.GetComponent<Collider>().enabled = false;

        for (int i = 0; i < changesGamestate.Length; i++)
        {
            Config.GameStates[changesGamestate[i]] = true;
            Instantiate(winParticle);
            Destroy(this.gameObject);
        }
    }

    private void HighlightTrigger()
    {
        this.BroadcastMessage("ShowHighlight");
    }

    private void Start()
    {
        this.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (!triggered && !ready)
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

    private void DeactivateTrigger()
    {
        this.GetComponent<Collider>().enabled = false;
    }

    private void ActivateTrigger()
    {
        this.GetComponent<Collider>().enabled = true;
    }

    private void RegisterTrigger(GameObject sender)
    {
        parent = sender;
        sender.SendMessage("TriggerReg", this.gameObject);
    }
}
