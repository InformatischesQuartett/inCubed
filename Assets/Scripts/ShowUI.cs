using UnityEngine;
using System.Collections;

public class ShowUI : MonoBehaviour
{
    private int lasttarget = -1;
    private float timer = 3;

	// Use this for initialization
	void Start ()
	{
	    for (int i = 0; i < this.transform.childCount; i++)
	    {
	        this.transform.GetChild(i).gameObject.SetActive(false);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    if (timer > 0)
	    {
	        timer -= Time.deltaTime;
	    }
	    else if (timer < 0)
	    {
	        if (lasttarget != -1)
	        {
	            transform.GetChild(lasttarget).gameObject.SetActive(false);
	            lasttarget = -1;
	        }
	        timer = 0;
	    }
	}

    void Display(int num)
    {
        num--;
        lasttarget = num;
        transform.GetChild(num).gameObject.SetActive(true);
        timer = 3;
    }
}
