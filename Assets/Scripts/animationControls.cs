using UnityEngine;

public class animationControls : MonoBehaviour
{

    public bool animate;
    public string anim;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (animate)
	    {
	        this.gameObject.GetComponent<Animation>().Play(anim);
	        animate = false;
	    }
	}

    void dubistneganzliebedrumwinkmalbitte()
    {
        animate = true;
        anim = "Wave";
    }

    void panic()
    {
        animate = true;
        anim = "Panic";
    }
}
