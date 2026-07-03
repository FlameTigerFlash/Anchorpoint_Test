using UnityEngine;

public class RechargingPoint : BaseProgressiveTask
{
    [SerializeField, Min(1)] private int _rechargingAmount = 1;

    private CharacterEnergy _energy;

    private void Update()
    {
        if (_energy != null)
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
        if (other.TryGetComponent<CharacterEnergy>(out var energy))
        {
            _energy = energy;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_energy == null || other.gameObject == _energy.gameObject)
        {
            _energy = null;
        }
    }

    protected override void Finish()
    {
        if (_energy != null)
        {
            _energy.AddEnergy(_rechargingAmount);
            base.Finish();
        }
        Progress = 0;
    }
}
