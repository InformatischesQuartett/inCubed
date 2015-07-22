using UnityEngine;

public enum SHAPETYPE
{
    None=-1,
    Triangle,
    Rectangle,
    Pentagon,
    Heptagon,
    Circle,
    Star
}

public class AndroidInterface : MonoBehaviour
{
    private AndroidJavaObject _toastExample;
    private AndroidJavaObject _activityContext;
    private AndroidJavaClass _pluginClass;

    private float _alpha;
    private float _curAlpha;
    private float _minThreshold;
    private float _maxThreshold;
    private SHAPETYPE _lastShape;
    private Vector2 _lastShapePos;

    public Texture2D FadeOutTexture;
    private Texture2D _webcamTex;

    private void Start()
    {
        _alpha = 1;
        _curAlpha = -1;
        _minThreshold = 10;
        _maxThreshold = 10;

        _pluginClass = new AndroidJavaClass("de.infoquart.unitymobile.CamHandler");
        _toastExample = _pluginClass.CallStatic<AndroidJavaObject>("instance");

        //_webcamTex = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);
        _webcamTex = new Texture2D(1024, 768, TextureFormat.RGBA32, false);
    }

    private void Update()
    {
        _alpha = (float)_toastExample.Call<double>("GetAvgBrightness");
        _curAlpha = _curAlpha < 0 ? _alpha : Mathf.Lerp(_curAlpha, _alpha, Time.deltaTime);
    }

    public void NewData(string dummy)
    {
        var data = _toastExample.Call<byte[]>("GetBuffer");

        if (data.Length > 0)
        {
            _webcamTex.LoadRawTextureData(data);
            _webcamTex.Apply();
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

    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, Screen.height, Screen.width, -Screen.height), _webcamTex);
    }
}
