using UnityEngine;

public class AndroidInterface : MonoBehaviour
{
    public void NewData(string dummy)
    {
        Config.NewData();
    }

    public void ShapeDetected(string shape)
    {
        Config.ShapeDetected(shape);
    }
}
