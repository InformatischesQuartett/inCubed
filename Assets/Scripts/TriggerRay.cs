﻿using UnityEngine;
using System.Collections;

public class TriggerRay : MonoBehaviour
{
    private RaycastHit hit;
    private bool isHit;
    private float rayLength;
    private float lookAtTimer;

    private Transform lastTarget;
    private GameObject currentTravelPoint;

    private bool traveling;
    private float currentTravelTime;

    private Vector3 startPos;
    private Vector3 endPos;
    private Quaternion startRot;
    private Quaternion endRot;

	// Use this for initialization
	void Start ()
	{
	    rayLength = 200f;
	    lookAtTimer = Config.triggerTimer;
	}
	
	// Update is called once per frame
	void Update ()
	{

	    if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength) && !traveling)
	    {
	        if (hit.transform.gameObject.CompareTag("TravelTarget"))
	        {
	            isHit = true;
	            lookAtTimer -= Time.deltaTime;
	            if (lookAtTimer < 0)
	                hit.collider.gameObject.SendMessage("EventTrigger", SendMessageOptions.DontRequireReceiver);

	            hit.collider.gameObject.SendMessage("HighlightTrigger", SendMessageOptions.DontRequireReceiver);
	            lastTarget = hit.transform;
	        }
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

            float t = currentTravelTime/Config.travelTime;
            t = t*t*t*(t*(6f*t - 15f) + 10f); //Smoothstep
            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            if (t >= 1)
            {
                traveling = false;
            }
        }

        // Ray stuff
        if (Debug.isDebugBuild && Config.DebugRayTrigger)
        {
            Debug.DrawLine(transform.position, transform.forward * rayLength, Color.red);

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
        //activate the Collider of the position we are leaving
        if (currentTravelPoint != null)
        {
            currentTravelPoint.GetComponent<Collider>().enabled = true;
        }

        traveling = true;
        startPos = this.transform.position;
        endPos = lastTarget.transform.position;
        startRot = this.transform.rotation;
        endRot = lastTarget.transform.rotation;

        //Setting the next travelpoint and deactivating its collider
        currentTravelPoint = lastTarget.gameObject;
        currentTravelPoint.GetComponent<Collider>().enabled = false;

        currentTravelTime = 0f;

    }
}
