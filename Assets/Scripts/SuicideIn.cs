using UnityEngine;
using System.Collections;

public class SuicideIn : MonoBehaviour
{
    public bool enabled = true;
    public float timer;
	
	// Update is called once per frame
	void Update ()
	{
	    if (enabled)
	    {
	        timer -= Time.deltaTime;
	        if (timer < 0)
	        {
                Destroy(this.gameObject);
	        }
	    }
	}
}
