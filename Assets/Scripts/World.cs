using UnityEngine;

public class World : MonoBehaviour
{
    private GameObject[] sides = new GameObject[6];

    private InputGamepad inputGamepad;

	// Use this for initialization
    private void Start()
    {
        inputGamepad = GameObject.Find("CardboardMain").GetComponent<InputGamepad>();

        sides[0] = Instantiate(Resources.Load("Prefabs/25kpoly") as GameObject);
        sides[0].transform.parent = this.transform;
        sides[0].name = "25kpoly";

        for (int i = 0; i < sides.Length; i++)
        {
            if (sides[i] == null)
            {
                sides[i] = Instantiate(Resources.Load("Prefabs/DummySide" + (i+1)) as GameObject);
                sides[i].transform.parent = this.transform;
                sides[i].name = "Side" + i;
            }
        }

        //Front Side
        sides[0].transform.position = new Vector3(0, 0, 84);
        sides[0].transform.Rotate(new Vector3(-90, 0, 0));

        //Back Side
        sides[1].transform.position = new Vector3(0, 0, -92);
        sides[1].transform.Rotate(new Vector3(90, 0, 0));

        //Right
        sides[2].transform.position = new Vector3(100, 0, 0);
        sides[2].transform.Rotate(new Vector3(0, 0, 90));

        //Left
        sides[3].transform.position = new Vector3(-100, 0, 0);
        sides[3].transform.Rotate(new Vector3(0, 0, -90));

        //Top
        sides[4].transform.position = new Vector3(0, 97, 0);
        sides[4].transform.Rotate(180, 0, 0);

        //Bottom
        sides[5].transform.position = new Vector3(0, -100, 0);
    }

    // Update is called once per frame
	void Update () {
        this.transform.Rotate(inputGamepad.JoystickAxes["LS Y"], inputGamepad.JoystickAxes["LS X"], 0);
    
    }
}
