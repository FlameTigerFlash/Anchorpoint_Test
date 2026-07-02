using UnityEngine;
using UnityEngine.Events;

public class ExtinguishableObject : MonoBehaviour
{
    public UnityEvent ExtinguishedEvent;

    public void OnExtinguish()
    {
        ExtinguishedEvent.Invoke();
        Destroy(gameObject);
    }
}
