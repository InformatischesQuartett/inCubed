using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class IntroBehavior : MonoBehaviour {
    public enum IntroState
    {
        Loading,
        Calibration,
        Transport,
        Wormhole,
        Story,
        StoryStay,
        Start
    }

    // TODO: not public, use find and stuff
    public GameObject Plane;
    public Texture WormholeTex;
    public GameObject LoadingRect;
    public GameObject CalibRect;
    public Text TimeText;
    public GameObject StoryPlane;

    private IntroState _introState;

    private float _rotSpeed;
    private float _starTime;
    private int _storyPlaneId;
    private Vector3 _tempTargetPos;

    private float _lastDataCheck;
    private Texture2D _webcamTex;

    private Renderer _planeRenderer;
    private Grayscale _grayScript;
    private Twirl _twirlScript;
    private ColorCorrectionCurves _cccScript;

    private const int MaxRotSpeed = 200;
    private const float SpeedIncrease = 1.01f;
    private const float AngleIncrease = 3f;
    private const float SaturationDecrease = 0.25f;
    private const float RampIncrease = 0.25f;

    private readonly Vector3 _bgPlaneTargetPos = new Vector3(0, 0, -7.5f);
    private readonly Vector3 _planeStartPos = new Vector3(0f, 0f, -8.5f);
    private readonly Vector3 _planeOddPos = new Vector3(-20f, 0f, -8.5f);
    private readonly Vector3 _planeEvenPos = new Vector3(20f, 0f, -8.5f);
    private readonly Vector3 _planeTargetSize = new Vector3(0.3f, 1, 0.169f);
    private readonly Vector3 _planeTargetSizeMax = new Vector3(1.00f, 1, 0.562f);

    private void Start()
    {
        _introState = IntroState.Loading;
        _starTime = Time.realtimeSinceStartup;

        _rotSpeed = 10;
        _lastDataCheck = -1;
        _storyPlaneId = 1;

        //_webcamTex = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);
        _webcamTex = new Texture2D(1024, 768, TextureFormat.RGBA32, false);

        _planeRenderer = Plane.GetComponent<Renderer>();
        _cccScript = GetComponent<ColorCorrectionCurves>();

        _grayScript = GetComponent<Grayscale>();
        _grayScript.enabled = false;

        _twirlScript = GetComponent<Twirl>();
        _twirlScript.enabled = false;

        StoryPlane.transform.localScale = new Vector3(0.0001f, 0, 0.0001f);
        _tempTargetPos = new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), _planeStartPos.z);

        var nextTex = Resources.Load<Texture2D>("Textures/Story/Story1");
        StoryPlane.GetComponent<Renderer>().material.mainTexture = nextTex;
    }

    private void Update()
    {
        switch (_introState)
        {
            case IntroState.Loading:
                if (Config.CamDataUpdate > 0 && Config.CamDataUpdate > _lastDataCheck)
                {
                    _introState = IntroState.Calibration;

                    LoadingRect.SetActive(false);
                    CalibRect.SetActive(true);
                }

                break;

            case IntroState.Calibration:
                if (Config.CamDataUpdate > 0 && Config.CamDataUpdate > _lastDataCheck)
                {
                    _lastDataCheck = Config.CamDataUpdate;

                    if (Config.CamData.Length > 0)
                    {
                        _webcamTex.LoadRawTextureData(Config.CamData);
                        _webcamTex.Apply();

                        _planeRenderer.material.mainTexture = _webcamTex;
                    }
                }

                var timeLeft = 5f;

                if (Config.LastShape == SHAPETYPE.Star)
                {
                    timeLeft = 5 - (Time.realtimeSinceStartup - _starTime);

                    if (timeLeft <= 0)
                    {
                        _introState = IntroState.Transport;
                        _starTime = Time.realtimeSinceStartup;
                        _twirlScript.enabled = true;
                        CalibRect.SetActive(false);
                    }
                }
                else
                    _starTime = Time.realtimeSinceStartup;

                var timeLeftClamped = (int) Mathf.Clamp(timeLeft, 0, 5);
                TimeText.text = timeLeftClamped + ((timeLeftClamped == 1) ? " Sekunde" : " Sekunden");

                break;

            case IntroState.Transport:
                Plane.transform.position = Vector3.Lerp(Plane.transform.position, _bgPlaneTargetPos, Time.deltaTime);

                _rotSpeed *= SpeedIncrease;

                if (_rotSpeed < MaxRotSpeed)
                    _rotSpeed *= SpeedIncrease;

                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed*Time.deltaTime);

                _twirlScript.angle += AngleIncrease*Time.deltaTime;

                if (_cccScript.saturation >= 0)
                    _cccScript.saturation -= SaturationDecrease*Time.deltaTime;
                else
                {
                    if (!_grayScript.enabled)
                        _grayScript.enabled = true;

                    _grayScript.rampOffset += RampIncrease*Time.deltaTime;
                }

                if (_grayScript.rampOffset > 1.0f)
                {
                    _planeRenderer.material.mainTexture = WormholeTex;
                    _introState = IntroState.Wormhole;
                    _rotSpeed = 3;
                }

                break;

            case IntroState.Wormhole:
                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed*Time.deltaTime);

                if (_grayScript.rampOffset >= 0)
                    _grayScript.rampOffset -= RampIncrease*Time.deltaTime;
                else
                {
                    if (_grayScript.enabled)
                        _grayScript.enabled = false;

                    if (_cccScript.saturation < 0.4f)
                        _cccScript.saturation += SaturationDecrease*Time.deltaTime;
                    else
                        _cccScript.saturation = 0.45f;
                }

                if (_twirlScript.angle - AngleIncrease*Time.deltaTime > 0)
                    _twirlScript.angle -= AngleIncrease*Time.deltaTime;
                else
                    _twirlScript.enabled = false;

                if (_cccScript.saturation > 0.4f && !_twirlScript.enabled)
                    _introState = IntroState.Story;

                break;

            case IntroState.Story:
                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed * Time.deltaTime);

                StoryPlane.transform.localScale = Vector3.Lerp(StoryPlane.transform.localScale,
                    _planeTargetSize, Time.deltaTime);

                if (StoryPlane.transform.localScale.x >= 0.14f)
                {
                    _starTime = Time.realtimeSinceStartup;
                    _introState = IntroState.StoryStay;
                }

                break;

            case IntroState.StoryStay:
                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed * Time.deltaTime);

                if (Time.realtimeSinceStartup - _starTime > 5)
                {
                    StoryPlane.transform.localScale = Vector3.Lerp(StoryPlane.transform.localScale,
                        _planeTargetSizeMax, Time.deltaTime);
                    StoryPlane.transform.position = Vector3.Lerp(StoryPlane.transform.position,
                        _storyPlaneId%2 == 0 ? _planeOddPos : _planeEvenPos, Time.deltaTime);

                    if (StoryPlane.transform.localScale.x >= 0.34f)
                    {
                        if (_storyPlaneId == 6)
                            _storyPlaneId = 1;

                        if (_storyPlaneId < 5)
                        {
                            _storyPlaneId++;

                            StoryPlane.transform.position = _planeStartPos;
                            StoryPlane.transform.localScale = new Vector3(0.0001f, 0, 0.0001f);

                            var nextTex = Resources.Load<Texture2D>("Textures/Story/Story" + _storyPlaneId);
                            StoryPlane.GetComponent<Renderer>().material.mainTexture = nextTex;

                            _introState = IntroState.Story;
                        }
                        else
                            _introState = IntroState.Start;
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

            case IntroState.Start:
                // Application.LoadLevel();
                break;
        }
    }
}
