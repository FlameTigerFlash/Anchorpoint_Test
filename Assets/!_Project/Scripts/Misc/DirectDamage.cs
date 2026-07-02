using UnityEngine;

public class DirectDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    public void OnTryDealDamage(GameObject other)
    {
        if (other.TryGetComponent<CharacterHealth>(out var hp))
        {
            hp.ReceiveDamage(_damage);
        }
    }
}
