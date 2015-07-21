using UnityEngine;
using System.Collections;

public class TriggerRay : MonoBehaviour
{
    private RaycastHit hit;
    private bool isHit;
    private float rayLength;
    private float lookAtTimer;

	// Use this for initialization
	void Start ()
	{
	    rayLength = 200f;
	    lookAtTimer = Config.triggerTimer;
	}
	
	// Update is called once per frame
	void Update ()
	{

	    if (Physics.Raycast(transform.position, Vector3.forward, out hit, rayLength))
	    {
	        isHit = true;
	        lookAtTimer -= Time.deltaTime;
            if (lookAtTimer < 0)
	            hit.collider.gameObject.SendMessage("EventTrigger");

            hit.collider.gameObject.SendMessage("HighlightTrigger");
	    }
	    else
	    {
	        isHit = false;
	        lookAtTimer = Config.triggerTimer;
	    }
	}

    private void OnGUI()
    {
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
}
