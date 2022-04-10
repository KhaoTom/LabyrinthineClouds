using UnityEngine;

public class TransformAdjustment : MonoBehaviour
{
    public void SetRotateX(float value)
    {
        var current = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(value, current.y, current.z);
    }
}
