using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private readonly GameObject[] _sides = new GameObject[6];

    private int _rotationStep;
    private Vector3 _rotatePoint;
    private const float OpenSpeed = 10f;

    private InputGamepad inputGamepad;

    public bool IsWorldOpen;
    public bool IsWorldOpening;

    private SHAPETYPE _lastShape;

	// Use this for initialization
    private void Start()
    {
        Config.World = this.gameObject;

        var cardMain = GameObject.Find("CardboardMain");
        if (cardMain != null)
            inputGamepad = cardMain.GetComponent<InputGamepad>();

        _sides[0] = Instantiate(Resources.Load("Prefabs/Gebirge") as GameObject);
        _sides[0].transform.parent = this.transform;
        _sides[0].name = "Gebirge";

        _sides[1] = Instantiate(Resources.Load("Prefabs/Dorf") as GameObject);
        _sides[1].transform.parent = this.transform;
        _sides[1].name = "Dorf";

        _sides[2] = Instantiate(Resources.Load("Prefabs/Eis") as GameObject);
        _sides[2].transform.parent = this.transform;
        _sides[2].name = "Eis";

        _sides[3] = Instantiate(Resources.Load("Prefabs/Feuer") as GameObject);
        _sides[3].transform.parent = this.transform;
        _sides[3].name = "Feuer";

        _sides[4] = Instantiate(Resources.Load("Prefabs/Wueste") as GameObject);
        _sides[4].transform.parent = this.transform;
        _sides[4].name = "Wueste";

        _sides[5] = Instantiate(Resources.Load("Prefabs/Wald") as GameObject);
        _sides[5].transform.parent = this.transform;
        _sides[5].name = "Wald";

        for (int i = 0; i < _sides.Length; i++)
        {
            if (_sides[i] == null)
            {
                _sides[i] = Instantiate(Resources.Load("Prefabs/DummySide" + (i)) as GameObject);
                _sides[i].transform.parent = this.transform;
                _sides[i].name = "Side" + i;
            }
        }

        //Front Side
        _sides[0].transform.position = new Vector3(-9.8f, -27, 67.7f);
        _sides[0].transform.Rotate(new Vector3(-90, 0, 0));

        //Back Side
        _sides[1].transform.position = new Vector3(0, 0, -100);
        _sides[1].transform.Rotate(new Vector3(90, 0, 0));

        //Right
        _sides[2].transform.position = new Vector3(100, 0, 0);
        _sides[2].transform.Rotate(new Vector3(0, 0, 90));

        //Left
        _sides[3].transform.position = new Vector3(-150, -6, 176);
        _sides[3].transform.Rotate(new Vector3(0, 0, -90));

        //Top
        _sides[4].transform.position = new Vector3(0, 100, 0);
        _sides[4].transform.Rotate(180, 0, 0);

        //Bottom
        _sides[5].transform.position = new Vector3(0, -100, 0);

        // Aufklappen
        _rotatePoint = _sides[4].transform.position + 100*Vector3.forward;
        _rotationStep = 0;

        IsWorldOpen = false;
        IsWorldOpening = false;

        _lastShape = SHAPETYPE.Star;
    }

    // Update is called once per frame
    private void Update()
    {
        /*if (!IsWorldOpening && Config.LastShape != _lastShape)
        {
            var newShape = Config.LastShape;

            if (_lastShape == SHAPETYPE.Star && newShape == SHAPETYPE.Circle)
                transform.Rotate(new Vector3(-90, 0, 0));
            if (_lastShape == SHAPETYPE.Star && newShape == SHAPETYPE.Triangle)
                transform.Rotate(new Vector3(+90, 0, 0));
            if (_lastShape == SHAPETYPE.Star && newShape == SHAPETYPE.Heptagon)
                transform.Rotate(new Vector3(0, -90, 0));
            if (_lastShape == SHAPETYPE.Star && newShape == SHAPETYPE.Rectangle)
                transform.Rotate(new Vector3(0, +90, 0));

            if (_lastShape == SHAPETYPE.Circle && newShape == SHAPETYPE.Pentagon)
                transform.Rotate(new Vector3(-90, 0, 0));
            if (_lastShape == SHAPETYPE.Circle && newShape == SHAPETYPE.Star)
                transform.Rotate(new Vector3(+90, 0, 0));
            if (_lastShape == SHAPETYPE.Circle && newShape == SHAPETYPE.Heptagon)
                transform.Rotate(new Vector3(0, -90, 0));
            if (_lastShape == SHAPETYPE.Circle && newShape == SHAPETYPE.Rectangle)
                transform.Rotate(new Vector3(0, +90, 0));

            if (_lastShape == SHAPETYPE.Pentagon && newShape == SHAPETYPE.Triangle)
                transform.Rotate(new Vector3(-90, 0, 0));
            if (_lastShape == SHAPETYPE.Pentagon && newShape == SHAPETYPE.Circle)
                transform.Rotate(new Vector3(+90, 0, 0));
            if (_lastShape == SHAPETYPE.Pentagon && newShape == SHAPETYPE.Heptagon)
                transform.Rotate(new Vector3(0, -90, 0));
            if (_lastShape == SHAPETYPE.Pentagon && newShape == SHAPETYPE.Rectangle)
                transform.Rotate(new Vector3(0, +90, 0));

            if (_lastShape == SHAPETYPE.Triangle && newShape == SHAPETYPE.Star)
                transform.Rotate(new Vector3(-90, 0, 0));
            if (_lastShape == SHAPETYPE.Triangle && newShape == SHAPETYPE.Pentagon)
                transform.Rotate(new Vector3(+90, 0, 0));
            if (_lastShape == SHAPETYPE.Triangle && newShape == SHAPETYPE.Heptagon)
                transform.Rotate(new Vector3(0, -90, 0));
            if (_lastShape == SHAPETYPE.Triangle && newShape == SHAPETYPE.Rectangle)
                transform.Rotate(new Vector3(0, +90, 0));

            if (_lastShape == SHAPETYPE.Heptagon && newShape == SHAPETYPE.Circle)
                transform.Rotate(new Vector3(-90, 0, 0));
            if (_lastShape == SHAPETYPE.Heptagon && newShape == SHAPETYPE.Triangle)
                transform.Rotate(new Vector3(+90, 0, 0));
            if (_lastShape == SHAPETYPE.Heptagon && newShape == SHAPETYPE.Pentagon)
                transform.Rotate(new Vector3(0, -90, 0));
            if (_lastShape == SHAPETYPE.Heptagon && newShape == SHAPETYPE.Star)
                transform.Rotate(new Vector3(0, +90, 0));

            if (_lastShape == SHAPETYPE.Triangle && newShape == SHAPETYPE.Heptagon)
                transform.Rotate(new Vector3(-90, 0, 0));
            if (_lastShape == SHAPETYPE.Triangle && newShape == SHAPETYPE.Rectangle)
                transform.Rotate(new Vector3(+90, 0, 0));
            if (_lastShape == SHAPETYPE.Triangle && newShape == SHAPETYPE.Pentagon)
                transform.Rotate(new Vector3(0, -90, 0));
            if (_lastShape == SHAPETYPE.Triangle && newShape == SHAPETYPE.Star)
                transform.Rotate(new Vector3(0, +90, 0));

            _lastShape = Config.LastShape;
        }
        */
//      this.transform.Rotate(inputGamepad.JoystickAxes["LS Y"], inputGamepad.JoystickAxes["LS X"], 0);

        switch (_rotationStep)
        {
            case 1:
                _sides[4].transform.RotateAround(_rotatePoint, new Vector3(1, 0, 0), OpenSpeed * Time.deltaTime);

                if (_sides[4].transform.rotation.eulerAngles.x <= 270)
                {
                    _rotationStep++;
                    _rotatePoint = _sides[0].transform.position - 72 * Vector3.up + 30 * Vector3.forward;
                }

                break;

            case 2:
                _sides[0].transform.RotateAround(_rotatePoint, new Vector3(1, 0, 0), OpenSpeed * Time.deltaTime);
                _sides[4].transform.RotateAround(_rotatePoint, new Vector3(1, 0, 0), OpenSpeed * Time.deltaTime);

                if (_sides[4].transform.rotation.eulerAngles.x < 10)
                {
                    _rotationStep++;
                    _rotatePoint = _sides[2].transform.position - 100 * Vector3.up;
                }           

                break;

            case 3:
                _sides[2].transform.RotateAround(_rotatePoint, new Vector3(0, 0, 1), -OpenSpeed * Time.deltaTime);

                if (_sides[2].transform.rotation.eulerAngles.z > 300)
                {
                    _rotationStep++;
                    _rotatePoint = _sides[3].transform.position - 92 * Vector3.up - 50 * Vector3.left;
                }

                break;

            case 4:
                _sides[3].transform.RotateAround(_rotatePoint, new Vector3(0, 0, 1), OpenSpeed * Time.deltaTime);

                if (_sides[3].transform.rotation.eulerAngles.z < 10)
                {
                    _rotationStep++;
                    _rotatePoint = _sides[1].transform.position - 100 * Vector3.up;
                }

                break;

            case 5:
                _sides[1].transform.RotateAround(_rotatePoint, new Vector3(1, 0, 0), -OpenSpeed * Time.deltaTime);

                if (_sides[1].transform.rotation.eulerAngles.x > 300)
                {
                    _rotationStep++;
                    IsWorldOpen = true;
                }

                break;
        }
    }

    public void OpenWorld()
    {
        IsWorldOpening = true;
        _rotationStep = 1;
    }

    private void OnGUI()
    {
        if (Debug.isDebugBuild && Config.DebugGamestate)
        {
            GUILayout.BeginArea(new Rect(50.0f, 50.0f, Screen.width - 50.0f, Screen.height - 50.0f));
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200.0f));
                {
                    foreach (KeyValuePair<string, bool> state in Config.GameStates)
                    {
                        GUILayout.Label(state.Key + ": " + state.Value);
                    }
                    GUILayout.Label("Ambient Intensity: " + RenderSettings.ambientIntensity);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
    }

}
