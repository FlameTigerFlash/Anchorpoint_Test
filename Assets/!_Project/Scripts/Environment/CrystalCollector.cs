using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class CrystalCollector : BaseProgressiveTask
{
    private PlayerInventory _inventory;

    private void Update()
    {
        if (_inventory != null && _inventory.IsCarrying)
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
        if (other.TryGetComponent<PlayerInventory>(out var inventory))
        {
            _inventory = inventory;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_inventory == null || other.gameObject == _inventory.gameObject)
        {
            _inventory = null;
        }
    }

    protected override void Finish()
    {
        if (_inventory != null && _inventory.TryUnload())
        {
            base.Finish();
        }
        Progress = 0;
    }
}
