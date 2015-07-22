using UnityEngine;
using System.Collections;

public class TriggerRay : MonoBehaviour
{
    private RaycastHit hit;
    private bool isHit;
    private float rayLength;
    private float lookAtTimer;

    private Transform lastTarget;

    private bool traveling;
    private float currentTravelTime;
    private float travelDistance;
    private Vector3 startPos;
    private Vector3 endPos;


	// Use this for initialization
	void Start ()
	{
	    rayLength = 200f;
	    lookAtTimer = Config.triggerTimer;
	}
	
	// Update is called once per frame
	void Update ()
	{

	    if (Physics.Raycast(transform.position, Vector3.forward, out hit, rayLength) && !traveling)
	    {
	        isHit = true;
	        lookAtTimer -= Time.deltaTime;
            if (lookAtTimer < 0)
	            hit.collider.gameObject.SendMessage("EventTrigger", SendMessageOptions.DontRequireReceiver);

            hit.collider.gameObject.SendMessage("HighlightTrigger", SendMessageOptions.DontRequireReceiver);
	        lastTarget = hit.transform;
	    }
	    else
	    {
	        isHit = false;
	        lookAtTimer = Config.triggerTimer;
            lastTarget = null;
	    }
	}

    private void OnGUI()
    {
        //Travel stuff
        if (traveling)
        {
            currentTravelTime += Time.deltaTime;
            if (currentTravelTime > Config.travelTime)
            {
                currentTravelTime = Config.travelTime;
            }

            float perc = currentTravelTime/Config.travelTime;
            transform.position = Vector3.Lerp(startPos, endPos, perc);
            if (perc >= 1)
            {
                traveling = false;
            }
        }

        // Ray stuff
        if (Debug.isDebugBuild && Config.DebugRayTrigger)
        {
            Debug.DrawLine(transform.position, Vector3.forward * rayLength, Color.red);

            GUILayout.BeginArea(new Rect(50.0f, 50.0f, Screen.width - 50.0f, Screen.height - 50.0f));
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200.0f));
                {
                    GUILayout.Label("Ray Hit: " + isHit);
                    GUILayout.Label("Timer: " + lookAtTimer);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
    }

    void Travel()
    {
        traveling = true;
        startPos = this.transform.position;
        endPos = lastTarget.transform.position;

        travelDistance = Vector3.Distance(startPos, endPos);
        currentTravelTime = 0f;

    }
}
