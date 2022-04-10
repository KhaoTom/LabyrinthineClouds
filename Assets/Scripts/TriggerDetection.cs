using UnityEngine;
using UnityEngine.Events;

public class TriggerDetection : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public int triggerCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        triggerCount++;
        onTriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        triggerCount--;
        if (triggerCount <= 0)
        {
            triggerCount = 0;
            onTriggerExit.Invoke();
        }
    }
}
