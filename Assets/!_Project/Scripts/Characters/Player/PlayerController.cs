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

    [SerializeField] private PlayerDeathHandler _deathHandler;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        IsActive = true;
    }

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

    public void OnDie()
    {
        IsActive = false;

        _lookRotate.enabled = false;
        _jumpAbility.enabled = false;
        _shootAbility.enabled = false;
        _shootAbility.enabled = false;

        _deathHandler.HandleDeath();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!IsActive)
        {
            return;
        }
        Vector2 direction = context.ReadValue<Vector2>();
        _lookRotate.SetLookInput(direction);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsActive)
        {
            return;
        }
        Vector2 direction = context.ReadValue<Vector2>();
        _movement.SetDirection(direction);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!IsActive)
        {
            return;
        }
        if (context.performed)
        {
            _jumpAbility.TryPerform();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!IsActive)
        {
            return;
        }
        if (context.performed)
        {
            _shootAbility.TryPerform();
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        SceneChanger sceneChanger = ServiceLocator.GetService<SceneChanger>();
        if (sceneChanger != null)
        {
            sceneChanger.OnMainMenu();
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

        GameManager gameManager = ServiceLocator.GetService<GameManager>();

        if (gameManager != null)
        {
            _characterHealth.DeathEvent.AddListener(gameManager.SetDefeat);
        }
    }
}
