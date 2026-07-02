using UnityEngine;
using UnityEngine.Events;

public class ImpactDetection : MonoBehaviour
{
    [SerializeField] private bool _destroyAfterCollision = true;
    [SerializeField] private bool _collideWithTriggers = false;

    public UnityEvent<GameObject> ImpactEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (!_collideWithTriggers && other.isTrigger)
        {
            return;
        }

        ImpactEvent.Invoke(other.gameObject);

        if (_destroyAfterCollision)
        {
            Destroy(gameObject);
        }
    }
}
