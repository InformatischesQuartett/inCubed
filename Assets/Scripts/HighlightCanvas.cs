using UnityEngine;
using System.Collections;

public class HighlightCanvas : MonoBehaviour
{

    private bool show;

	// Use this for initialization
	void Start ()
	{
	    this.GetComponent<Canvas>().enabled = false;
	}

    void ShowHighlight()
    {
        show = true;
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Canvas>().enabled = show;
        this.transform.Rotate(0, 150 * Time.deltaTime, 0);
	    show = false;
	}
}
