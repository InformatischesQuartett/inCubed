using UnityEngine;
using System.Collections;

public class CreatureBehavior : MonoBehaviour {

    private float speed;
    private int range;
    private Vector3 waypoint;

	// Use this for initialization
	void Start () {
        range = 10;
        speed = 5;

        //initialize first Waypoint
        getNewWayPoint();
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
        if ((transform.position - waypoint).magnitude < 3)
        {
            // when the distance between us and the target is less than 3
            // create a new way point target
            getNewWayPoint();
        }
	}

    private void getNewWayPoint()
    {
        waypoint = new Vector3(Random.Range(transform.position.x - range, transform.position.x + range), 1, Random.Range(transform.position.z - range, transform.position.z + range));
        waypoint.y = 0;
        transform.LookAt(waypoint);
        Debug.Log(waypoint + " and " + (transform.position - waypoint).magnitude);
    }

    /**
     * Idle mode to wait a few seconds before walking again
     **/
    private void idle()
    {
        
    }

    private void checkCollision()
    {

    }
}
