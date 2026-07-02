using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterHealth _characterHealth;
    [SerializeField] private CharacterEnergy _characterEnergy;
    [SerializeField] private PlayerInventory _inventory;

    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private PlayerLookRotate _lookRotate;
    [SerializeField] private JumpAbility _jumpAbility;
    [SerializeField] private ShootAbility _shootAbility;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetupServicesConnection();
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _lookRotate.SetLookInput(direction);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _movement.SetDirection(direction);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _jumpAbility.TryPerform();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _shootAbility.TryPerform();
        }
    }

    private void SetupServicesConnection()
    {
        StatsManager statsManager = ServiceLocator.GetService<StatsManager>();

        if (statsManager != null)
        {
            _characterHealth.HealthChangedEvent.AddListener(statsManager.OnHealthChanged);
            statsManager.OnHealthChanged(_characterHealth.Health);

            statsManager.SetCharacterEnergy(_characterEnergy);

            _inventory.InventoryStateChangedEvent.AddListener(statsManager.OnInventoryStateChanged);
            statsManager.OnInventoryStateChanged(_inventory.IsCarrying);
        }

        MapLocator mapLocator = ServiceLocator.GetService<MapLocator>();

        if (mapLocator != null)
        {
            mapLocator.SetPlayerLink(gameObject);
        }
    }
}
