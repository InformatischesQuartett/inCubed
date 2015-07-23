using UnityEngine;
using System.Collections;

public class moveSelf : MonoBehaviour
{

    public float x;
    public float y;
    public float z;

    public float destroyTimer;
    public bool runTimer;

    private float currentTimer;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
	    currentTimer += Time.deltaTime;

	    if (currentTimer > destroyTimer)
	    {
	        Destroy(this.gameObject);
	    }

	}
}
