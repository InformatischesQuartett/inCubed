using UnityEngine;



public class AndroidInterface : MonoBehaviour
{
    public GameObject Plane;

    private AndroidJavaObject _toastExample;
    private AndroidJavaObject _activityContext;
    private AndroidJavaClass _pluginClass;

    private float _alpha;
    private float _curAlpha;
    private SHAPETYPE _lastShape;
    private Vector2 _lastShapePos;

    private float _rotSpeed;
    private Texture2D _webcamTex;

    private const int MaxRotSpeed = 200;
    private const float SpeedIncrease = 1.005f;

    private void Start()
    {
        _rotSpeed = 1;

        _alpha = 1;
        _curAlpha = -1;

        AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");

        _pluginClass = new AndroidJavaClass("com.Company.inCubed.CamHandler");
        _toastExample = _pluginClass.CallStatic<AndroidJavaObject>("instance");

        //_webcamTex = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);
        _webcamTex = new Texture2D(1024, 768, TextureFormat.RGBA32, false);
    }

    private void Update()
    {
        if (_toastExample != null)
        {
            _alpha = (float) _toastExample.Call<double>("GetAvgBrightness");
            _curAlpha = _curAlpha < 0 ? _alpha : Mathf.Lerp(_curAlpha, _alpha, Time.deltaTime);
        }

        _rotSpeed *= 1.01f;

        if (_rotSpeed < MaxRotSpeed)
            _rotSpeed *= SpeedIncrease;

        Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed * Time.deltaTime);
    }

    public void NewData(string dummy)
    {
        var data = _toastExample.Call<byte[]>("GetBuffer");

        if (data.Length > 0)
        {
            _webcamTex.LoadRawTextureData(data);
            _webcamTex.Apply();

            Plane.GetComponent<Renderer>().material.mainTexture = _webcamTex;
        }
    }

    public void ShapeDetected(string shape)
    {
        _lastShape = (SHAPETYPE) int.Parse(shape);

        if (_toastExample != null)
        {
            var shapePos = _toastExample.Call<double[]>("GetShapePosition");
            _lastShapePos = new Vector2((float) shapePos[0], (float) shapePos[1]);
        }
    }
}
