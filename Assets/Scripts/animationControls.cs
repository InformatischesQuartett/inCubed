using UnityEngine;

public class animationControls : MonoBehaviour
{

    public bool animate;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (animate)
	    {
	        this.gameObject.GetComponent<Animation>().Play("Wave");
	        animate = false;
	    }
	}

    void dubistneganzliebedrumwinkmalbitte()
    {
        animate = true;
    }
}
