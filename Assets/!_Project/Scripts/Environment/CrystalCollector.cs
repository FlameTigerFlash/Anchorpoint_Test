using UnityEngine;
using UnityEngine.Events;

public class CrystalCollector : MonoBehaviour
{
    public UnityEvent CrystalCollectedEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInventory>(out var inventory) && inventory.TryUnload())
        {
            CrystalCollectedEvent.Invoke();
        }
    }
}
