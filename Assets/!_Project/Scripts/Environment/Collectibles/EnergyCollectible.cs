using UnityEngine;

public class EnergyCollectible : MonoBehaviour
{
    [SerializeField] private float _amount = 100f;

    public void OnAddEnergy(GameObject other)
    {
        if (other.TryGetComponent<CharacterEnergy>(out var energy))
        {
            energy.AddEnergy(_amount);
            Destroy(gameObject);
        }
    }
}
