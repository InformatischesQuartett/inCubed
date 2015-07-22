using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class IntroBehavior : MonoBehaviour {
    public enum IntroState
    {
        Loading,
        Calibration,
        Transport,
        Wormhole
    }

    // TODO: not public, use find and stuff
    public GameObject Plane;
    public Texture WormholeTex;
    public GameObject LoadingRect;
    public GameObject CalibRect;
    public Text TimeText;

    private IntroState _introState;

    private float _rotSpeed;
    private float _starTime;

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

    private void Start()
    {
        _introState = IntroState.Loading;
        _starTime = Time.realtimeSinceStartup;

        _rotSpeed = 10;
        _lastDataCheck = -1;

        //_webcamTex = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);
        _webcamTex = new Texture2D(1024, 768, TextureFormat.RGBA32, false);

        _planeRenderer = Plane.GetComponent<Renderer>();
        _cccScript = GetComponent<ColorCorrectionCurves>();

        _grayScript = GetComponent<Grayscale>();
        _grayScript.enabled = false;

        _twirlScript = GetComponent<Twirl>();
        _twirlScript.enabled = false;
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
                _rotSpeed *= SpeedIncrease;

                if (_rotSpeed < MaxRotSpeed)
                    _rotSpeed *= SpeedIncrease;

                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed*Time.deltaTime);

                _twirlScript.angle += AngleIncrease * Time.deltaTime;

                if (_cccScript.saturation >= 0)
                    _cccScript.saturation -= SaturationDecrease * Time.deltaTime;
                else
                {
                    if (!_grayScript.enabled)
                        _grayScript.enabled = true;

                    _grayScript.rampOffset += RampIncrease * Time.deltaTime;
                }

                if (_grayScript.rampOffset > 1.0f)
                {
                    _planeRenderer.material.mainTexture = WormholeTex;
                    _introState = IntroState.Wormhole;
                    _rotSpeed = 5;
                }

                break;

            case IntroState.Wormhole:
                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed*Time.deltaTime);

                if (_grayScript.rampOffset >= 0)
                    _grayScript.rampOffset -= RampIncrease * Time.deltaTime;
                else
                {
                    if (_grayScript.enabled)
                        _grayScript.enabled = false;

                    if (_cccScript.saturation < 1)
                        _cccScript.saturation += SaturationDecrease * Time.deltaTime;
                    else
                        _cccScript.saturation = 1;
                }

                if (_twirlScript.angle - AngleIncrease * Time.deltaTime > 0)
                    _twirlScript.angle -= AngleIncrease * Time.deltaTime;
                else
                    _twirlScript.enabled = false;

                break;
        }
    }
}
