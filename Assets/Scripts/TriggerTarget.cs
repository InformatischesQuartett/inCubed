using UnityEngine;

public class TriggerTarget : MonoBehaviour
{
    public string[] changesGamestate;
    public string[] dependsOnGamestate;
    public bool destroy;
    public bool instantiate;
    public spawns[] prefabs;
    public bool message;
    public string eventMessage;

    public bool ready;
    public bool triggered;

    public string registerTo;

    private GameObject parent;
    private GameObject world;

    private float preWarmTimer = 2;

    private void EventTrigger()
    {
        DeactivateTrigger();

        for (int i = 0; i < changesGamestate.Length; i++)
        {
            Config.GameStates[changesGamestate[i]] = true;
        }
        if (instantiate)
        {
            foreach (var prefab in prefabs)
            {
                if (prefab.atNativePos)
                {
                    Instantiate(prefab.prefab);
                }
                else
                {
                    Instantiate(prefab.prefab, this.transform.position, this.transform.rotation);
                }
            }
        }
        if (destroy)
        {
            Destroy(this.gameObject);
        }
        if (message)
        {
            world.SendMessage(eventMessage);
        }
    }

    private void HighlightTrigger()
    {
        this.BroadcastMessage("ShowHighlight", SendMessageOptions.DontRequireReceiver);
    }

    private void Start()
    {
        world = GameObject.Find("World");
        DeactivateTrigger();
        if (registerTo != string.Empty)
        {
            GameObject.Find(registerTo).SendMessage("TriggerReg", this.gameObject);
        }
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
                if (preWarmTimer < 0)
                {
                    ActivateTrigger();
                }
                else
                {
                    preWarmTimer -= Time.deltaTime;
                    ready = false;
                }
            }
        }
    }

    private void DeactivateTrigger()
    {
        this.GetComponent<Collider>().enabled = false;
    }

    private void ActivateTrigger()
    {
        if (ready)
        {
            this.GetComponent<Collider>().enabled = true;
        }
    }

    private void RegisterTrigger(GameObject sender)
    {
        parent = sender;
        sender.SendMessage("TriggerReg", this.gameObject);
    }
}
