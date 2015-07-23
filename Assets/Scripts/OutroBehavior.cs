using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class OutroBehavior : MonoBehaviour {
    public enum OutroState
    {
        Island,
        Transport1,
        Wormhole,
        Story,
        StoryStay,
        Transport2,
        RealMode
    }

    // TODO: not public, use find and stuff
    public World WorldScript;
    public GameObject WorldObj;

    public GameObject Plane;
    public GameObject StoryPlane;
    public GameObject Planes;
    public Texture WormholeTex;
    public RawImage WebcamTex;
    public GameObject CalibRect;
    public CardboardHead CardbordScript;

    public Camera leftCam;
    public Camera rightCam;

    private OutroState _outroState;

    private float _rotSpeed;
    private float _starTime;
    private int _storyPlaneId;
    private Vector3 _tempTargetPos;

    private float _lastDataCheck;
    private Texture2D _webcamTex;

    private Renderer _planeRenderer;
    private Grayscale _grayScriptLeft;
    private Twirl _twirlScriptLeft;
    private ColorCorrectionCurves _cccScriptLeft;
    private Grayscale _grayScriptRight;
    private Twirl _twirlScriptRight;
    private ColorCorrectionCurves _cccScriptRight;

    private const float AngleIncrease = 3f;
    private const float SaturationDecrease = 0.25f;
    private const float RampIncrease = 0.25f;

    private readonly Vector3 _planeStartPos = new Vector3(0f, 0f, -8.5f);
    private readonly Vector3 _planeOddPos = new Vector3(-20f, 0f, -8.5f);
    private readonly Vector3 _planeEvenPos = new Vector3(20f, 0f, -8.5f);
    private readonly Vector3 _planeTargetSize = new Vector3(0.3f, 1, 0.169f);
    private readonly Vector3 _planeTargetSizeMax = new Vector3(1.00f, 1, 0.562f);

    private void Start()
    {
        _outroState = OutroState.Island;
        _starTime = Time.realtimeSinceStartup;

        _rotSpeed = 10;
        _lastDataCheck = -1;
        _storyPlaneId = 1;

        //_webcamTex = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);
        _webcamTex = new Texture2D(1024, 768, TextureFormat.RGBA32, false);

        Planes.SetActive(false);

        _planeRenderer = Plane.GetComponent<Renderer>();

        StoryPlane.transform.localScale = new Vector3(0.0001f, 0, 0.0001f);
        _tempTargetPos = new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), _planeStartPos.z);

        var nextTex = Resources.Load<Texture2D>("Textures/Credits/Credits1");
        StoryPlane.GetComponent<Renderer>().material.mainTexture = nextTex;

        _cccScriptLeft = leftCam.GetComponent<ColorCorrectionCurves>();
        _cccScriptRight = rightCam.GetComponent<ColorCorrectionCurves>();

        _grayScriptLeft = leftCam.GetComponent<Grayscale>();
        _grayScriptRight = rightCam.GetComponent<Grayscale>();
        _grayScriptLeft.enabled = false;
        _grayScriptRight.enabled = false;

        _twirlScriptLeft = leftCam.GetComponent<Twirl>();
        _twirlScriptRight = rightCam.GetComponent<Twirl>();
        _twirlScriptLeft.enabled = false;
        _twirlScriptRight.enabled = false;
    }

    private void Update()
    {
        if (!WorldScript.IsWorldOpening && !WorldScript.IsWorldOpen)
            WorldScript.OpenWorld();

        if (!WorldScript.IsWorldOpen)
            return;

        switch (_outroState)
        {
            case OutroState.Island:
                if (Time.realtimeSinceStartup - _starTime > 10)
                    _outroState = OutroState.Transport1;

                break;

            case OutroState.Transport1:
                if (_cccScriptLeft.saturation >= 0)
                {
                    _cccScriptLeft.saturation -= SaturationDecrease*Time.deltaTime;
                    _cccScriptRight.saturation -= SaturationDecrease*Time.deltaTime;
                }
                else
                {
                    if (!_grayScriptLeft.enabled)
                    {
                        _grayScriptLeft.enabled = true;
                        _grayScriptRight.enabled = true;
                    }

                    _grayScriptLeft.rampOffset += RampIncrease*Time.deltaTime;
                    _grayScriptRight.rampOffset += RampIncrease * Time.deltaTime;
                }

                if (_grayScriptLeft.rampOffset > 1.0f)
                {
                    Planes.SetActive(true);
                    WorldObj.SetActive(false);
                    _planeRenderer.material.mainTexture = WormholeTex;
                    _outroState = OutroState.Wormhole;
                }

                break;

            case OutroState.Wormhole:
                CardbordScript.trackPosition = false;
                CardbordScript.trackRotation = false;

                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed * Time.deltaTime);

                if (_grayScriptLeft.rampOffset >= 0)
                {
                    _grayScriptLeft.rampOffset -= RampIncrease * Time.deltaTime;
                    _grayScriptRight.rampOffset -= RampIncrease * Time.deltaTime;
                }
                else
                {
                    if (_grayScriptLeft.enabled)
                    {
                        _grayScriptLeft.enabled = false;
                        _grayScriptRight.enabled = false;
                    }

                    if (_cccScriptLeft.saturation < 0.4f)
                    {
                        _cccScriptLeft.saturation += SaturationDecrease * Time.deltaTime;
                        _cccScriptRight.saturation += SaturationDecrease * Time.deltaTime;
                    }
                    else
                    {
                        _cccScriptLeft.saturation = 0.45f;
                        _cccScriptRight.saturation = 0.45f;
                    }
                }

                if (_cccScriptLeft.saturation > 0.4f)
                    _outroState = OutroState.Story;

                break;

            case OutroState.Story:
                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed * Time.deltaTime);

                StoryPlane.transform.localScale = Vector3.Lerp(StoryPlane.transform.localScale,
                    _planeTargetSize, Time.deltaTime);

                if (StoryPlane.transform.localScale.x >= 0.14f)
                {
                    _starTime = Time.realtimeSinceStartup;
                    _outroState = OutroState.StoryStay;
                }

                break;

            case OutroState.StoryStay:
                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed * Time.deltaTime);

                if (Time.realtimeSinceStartup - _starTime > 5)
                {
                    StoryPlane.transform.localScale = Vector3.Lerp(StoryPlane.transform.localScale,
                        _planeTargetSizeMax, Time.deltaTime);
                    StoryPlane.transform.position = Vector3.Lerp(StoryPlane.transform.position,
                        _storyPlaneId % 2 == 0 ? _planeOddPos : _planeEvenPos, Time.deltaTime);

                    if (StoryPlane.transform.localScale.x >= 0.34f)
                    {
                        if (_storyPlaneId < 3)
                        {
                            _storyPlaneId++;

                            StoryPlane.transform.position = _planeStartPos;
                            StoryPlane.transform.localScale = new Vector3(0.0001f, 0, 0.0001f);

                            var nextTex = Resources.Load<Texture2D>("Textures/Story/Story" + _storyPlaneId);
                            StoryPlane.GetComponent<Renderer>().material.mainTexture = nextTex;

                            _outroState = OutroState.Story;
                        }
                        else
                            _outroState = OutroState.Transport2;
                    }
                }
                else
                {
                    StoryPlane.transform.localPosition = Vector3.Lerp(StoryPlane.transform.localPosition,
                        _tempTargetPos, Time.deltaTime * 2);

                    var dist = Vector3.Distance(StoryPlane.transform.localPosition, _tempTargetPos);
                    if (dist < 0.001f)
                        _tempTargetPos = new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), _planeStartPos.z);
                }

                break;

            case OutroState.Transport2:
                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed * Time.deltaTime);

                _twirlScriptLeft.angle += AngleIncrease*Time.deltaTime;
                _twirlScriptRight.angle += AngleIncrease * Time.deltaTime;

                if (_cccScriptLeft.saturation >= 0)
                {
                    _cccScriptLeft.saturation -= SaturationDecrease * Time.deltaTime;
                    _cccScriptRight.saturation -= SaturationDecrease * Time.deltaTime;
                }
                else
                {
                    if (!_grayScriptLeft.enabled)
                    {
                        _grayScriptLeft.enabled = true;
                        _grayScriptRight.enabled = true;
                    }

                    _grayScriptLeft.rampOffset += RampIncrease * Time.deltaTime;
                    _grayScriptRight.rampOffset += RampIncrease * Time.deltaTime;
                }

                if (_grayScriptLeft.rampOffset > 1.0f)
                {
                    _outroState = OutroState.RealMode;
                    CalibRect.SetActive(true);
                    Planes.SetActive(false);
                    Config.ResumeOpenCV();
                }

                break;
     
            case OutroState.RealMode:
                Plane.transform.rotation = Quaternion.Euler(270, 0, 0);

                if (Config.CamDataUpdate > 0 && Config.CamDataUpdate > _lastDataCheck)
                {
                    _lastDataCheck = Config.CamDataUpdate;

                    if (Config.CamData.Length > 0)
                    {
                        _webcamTex.LoadRawTextureData(Config.CamData);
                        _webcamTex.Apply();

                        WebcamTex.texture = _webcamTex;
                        _planeRenderer.material.mainTexture = _webcamTex;
                    }
                }

                if (_grayScriptLeft.rampOffset >= 0)
                {
                    _grayScriptLeft.rampOffset -= RampIncrease * Time.deltaTime;
                    _grayScriptRight.rampOffset -= RampIncrease * Time.deltaTime;
                }
                else
                {
                    if (_grayScriptLeft.enabled)
                    {
                        _grayScriptLeft.enabled = false;
                        _grayScriptRight.enabled = false;
                    }

                    if (_cccScriptLeft.saturation < 0.4f)
                    {
                        _cccScriptLeft.saturation += SaturationDecrease * Time.deltaTime;
                        _cccScriptRight.saturation += SaturationDecrease * Time.deltaTime;
                    }
                    else
                    {
                        _cccScriptLeft.saturation = 0.45f;
                        _cccScriptRight.saturation = 0.45f;
                    }

                    if (_twirlScriptLeft.angle - AngleIncrease * Time.deltaTime > 0)
                    {
                        _twirlScriptLeft.angle -= AngleIncrease * Time.deltaTime;
                        _twirlScriptRight.angle -= AngleIncrease * Time.deltaTime;
                    }
                    else
                    {
                        _twirlScriptLeft.enabled = false;
                        _twirlScriptRight.enabled = false;
                    }
                }

                break;
        }
    }
}
