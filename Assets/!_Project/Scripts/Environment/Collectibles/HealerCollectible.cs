using Unity.VisualScripting;
using UnityEngine;

public class HealerCollectible : MonoBehaviour
{
    [SerializeField] private int _amount = 1;

    public void OnHeal(GameObject other)
    {
        if (other.TryGetComponent<CharacterHealth>(out var health))
        {
            health.Heal(_amount);
            Destroy(gameObject);
        }
    }
}
