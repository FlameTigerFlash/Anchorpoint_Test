using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PlayerDetector : MonoBehaviour
{
    public UnityEvent<GameObject> PlayerEnteredEvent;
    public UnityEvent<GameObject> PlayerExitedEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other.gameObject))
        {
            PlayerEnteredEvent.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other.gameObject))
        {
            PlayerExitedEvent.Invoke(other.gameObject);
        }
    }

    private bool IsPlayer(GameObject otherObject)
    {
        if (otherObject.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }
}
