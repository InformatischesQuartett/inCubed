using UnityEngine;
using System.Collections;

public class MousControls : MonoBehaviour
{
    public bool UseMouseLook;
    public float lookSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButton(0) && UseMouseLook)
	    {
            this.transform.Rotate(Vector3.up  , Input.GetAxis("MouseX") * Time.deltaTime * lookSpeed);
            this.transform.Rotate(Vector3.left, Input.GetAxis("MouseY") * Time.deltaTime * lookSpeed);

        }
    }
}
