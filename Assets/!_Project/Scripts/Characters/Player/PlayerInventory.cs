using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public UnityEvent<bool> InventoryStateChangedEvent;

    public bool IsCarrying 
    {
        get => _isCarrying; 
        private set
        {
            bool temp = _isCarrying;
            _isCarrying = value;
            if (temp != _isCarrying)
            {
                InventoryStateChangedEvent.Invoke(_isCarrying);
            }
        }
    }

    private bool _isCarrying;

    private void Awake()
    {
        IsCarrying = false;
    }

    public bool TryUnload()
    {
        if (IsCarrying)
        {
            IsCarrying = false;
            return true;
        }
        return false;
    }

    public bool TryLoad()
    {
        if (!IsCarrying)
        {
            IsCarrying = true;
            return true;
        }
        return false;
    }
}
