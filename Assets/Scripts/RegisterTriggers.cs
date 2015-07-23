using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class RegisterTriggers : MonoBehaviour
{
    public List<GameObject> triggers;

	// Use this for initialization
	void Start () {
	    this.BroadcastMessage("RegisterTrigger", this.gameObject);
	}

    void TriggerReg(GameObject trigger)
    {
        triggers.Add(trigger);
    }

    void ActivateTriggers()
    {
        foreach (var trigger in triggers)
        {
            if (trigger != null)
            {
                trigger.SendMessage("ActivateTrigger");
            }
        }
    }
    void DeactivateTriggers()
    {
        foreach (var trigger in triggers)
        {
            if (trigger != null)
            {
                trigger.SendMessage("DeactivateTrigger");
            }
        }
    }
}
