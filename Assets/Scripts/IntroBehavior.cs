using UnityEngine;

public class IntroBehavior : MonoBehaviour {
    public enum IntroState
    {
        Calibration,
        Transport,
        Wormhole
    }

    public GameObject Plane;

    private IntroState _introState;
    private float _rotSpeed;

    private float _lastDataCheck;
    private Texture2D _webcamTex;

    private const int MaxRotSpeed = 200;
    private const float SpeedIncrease = 1.01f;

    private void Start()
    {
        _introState = IntroState.Calibration;

        _rotSpeed = 5;
        _lastDataCheck = 0;

        //_webcamTex = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);
        _webcamTex = new Texture2D(1024, 768, TextureFormat.RGBA32, false);
    }

    private void Update()
    {
        switch (_introState)
        {
            case IntroState.Calibration:
                if (Config.CamDataUpdate > _lastDataCheck)
                {
                    _lastDataCheck = Config.CamDataUpdate;

                    if (Config.CamData.Length > 0)
                    {
                        _webcamTex.LoadRawTextureData(Config.CamData);
                        _webcamTex.Apply();

                        Plane.GetComponent<Renderer>().material.mainTexture = _webcamTex;
                    }
                }

                if (Time.realtimeSinceStartup > 15)
                    _introState = IntroState.Transport;

                break;

            case IntroState.Transport:
                _rotSpeed *= SpeedIncrease;

                if (_rotSpeed < MaxRotSpeed)
                    _rotSpeed *= SpeedIncrease;

                Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed*Time.deltaTime);

                if (Time.realtimeSinceStartup > 30)
                    _introState = IntroState.Wormhole;

                break;
        }
    }
}
