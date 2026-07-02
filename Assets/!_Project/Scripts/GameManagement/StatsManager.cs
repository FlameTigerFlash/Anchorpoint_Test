using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private HealthDisplay _healthDisplay;
    [SerializeField] private EnergyDisplay _energyDisplay;
    [SerializeField] private HaulingCrystalDisplay _haulingCrystalDisplay;
    [SerializeField] private CrystalsDisplay _crystalsDisplay;

    private CharacterEnergy _playerEnergy;

    public int CollectedCrystals 
    {
        get => _collectedCrystals;
        private set
        {
            int temp = _collectedCrystals;
            _collectedCrystals = value;
            if (temp != _collectedCrystals)
            {
                OnCrystalsAmountChanged();
            }
        }
    }

    private int _collectedCrystals = 0;

    private void Start()
    {
        _crystalsDisplay.SetRequiredAmount(_gameManager.CollectedCrystalsGoal);
        CollectedCrystals = 0;
        OnCrystalsAmountChanged();
    }

    private void Update()
    {
        HandleStats();
    }

    public void SetCharacterEnergy(CharacterEnergy energy)
    {
        _playerEnergy = energy;
    }

    public void OnHealthChanged(int health)
    {
        _healthDisplay.Display(health);
    }

    public void OnInventoryStateChanged(bool inventoryFull)
    {
        _haulingCrystalDisplay.Display(inventoryFull);
    }

    public void OnCrystalsAmountChanged()
    {
        _crystalsDisplay.Display(CollectedCrystals);
    }

    public void AddCrystal()
    {
        CollectedCrystals += 1;
    }

    private void HandleStats()
    {
        if (_playerEnergy != null)
        {
            _energyDisplay.Display(_playerEnergy.CurEnergy);
        }
    }
}
