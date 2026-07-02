using UnityEngine;

public class CrystalCollectible : MonoBehaviour
{
    public void OnGiveCrystal(GameObject other)
    {
        if (other.TryGetComponent<PlayerInventory>(out var inventory))
        {
            if (inventory.TryLoad())
            {
                Destroy(gameObject);
            }
        }
    }
}
