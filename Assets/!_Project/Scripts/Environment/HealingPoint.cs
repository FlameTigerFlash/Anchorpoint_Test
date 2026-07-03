using UnityEngine;

public class HealingPoint : BaseProgressiveTask
{
    [SerializeField, Min(1)] private int _healingAmount = 1;

    private CharacterHealth _health;

    private void Update()
    {
        if (_health != null)
        {
            Progress += Time.deltaTime;
        }
        else
        {
            Progress = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterHealth>(out var health))
        {
            _health = health;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_health == null || other.gameObject == _health.gameObject)
        {
            _health = null;
        }
    }

    protected override void Finish()
    {
        if (_health != null)
        {
            _health.Heal(_healingAmount);
            base.Finish();
        }
        Progress = 0;
    }
}
