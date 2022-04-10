using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float startSpeed = 1f;
    public Vector3 startAxis = Vector3.right;
    public bool playOnAwake = true;

    public float Speed { get; set; }
    public Vector3 Axis { get; set; }
    public bool Playing { get; set; }

    private void Awake()
    {
        Speed = startSpeed;
        Axis = startAxis;
        enabled = playOnAwake;
    }

    private void Update()
    {
        transform.Rotate(Axis, Speed * Time.deltaTime);
    }

    public void Play()
    {
        enabled = true;
    }

    public void Stop()
    {
        enabled = false;
    }
}