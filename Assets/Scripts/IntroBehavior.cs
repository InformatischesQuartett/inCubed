using UnityEngine;

public class IntroBehavior : MonoBehaviour {
    public GameObject Plane;

    private float _rotSpeed;

    private float _lastDataCheck;
    private Texture2D _webcamTex;

    private const int MaxRotSpeed = 200;
    private const float SpeedIncrease = 1.005f;

    private void Start()
    {
        _rotSpeed = 1;
        _lastDataCheck = 0;

        //_webcamTex = new Texture2D(1920, 1080, TextureFormat.RGBA32, false);
        _webcamTex = new Texture2D(1024, 768, TextureFormat.RGBA32, false);
    }

    private void Update()
    {
        _rotSpeed *= 1.01f;

        if (_rotSpeed < MaxRotSpeed)
            _rotSpeed *= SpeedIncrease;

        Plane.transform.Rotate(new Vector3(0, 1, 0), _rotSpeed * Time.deltaTime);

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
    }
}
