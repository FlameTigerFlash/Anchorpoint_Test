using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PlayerDetector : MonoBehaviour
{
    public UnityEvent PlayerEnteredEvent;
    public UnityEvent PlayerExitedEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other.gameObject))
        {
            Debug.Log("Player has entered the base.");
            PlayerEnteredEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other.gameObject))
        {
            Debug.Log("Player has exited the base.");
            PlayerExitedEvent.Invoke();
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
