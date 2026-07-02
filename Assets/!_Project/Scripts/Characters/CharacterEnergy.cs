using UnityEngine;

public class CharacterEnergy : MonoBehaviour
{
    [SerializeField, Range(0, float.MaxValue)] private float _maxEnergy = 100f;
    [SerializeField] private float _initialEnergy = 30f;
    [SerializeField] private float _energyRegeneration = 10f;
    [SerializeField] private float _naturalRegenerationCeiling = 20f;

    public float CurEnergy { get; private set;}

    private void Awake()
    {
        CurEnergy = Mathf.Clamp(_initialEnergy, 0, _maxEnergy);
    }

    private void Update()
    {
        HandleRegeneration(_energyRegeneration * Time.deltaTime);
    }

    public void AddEnergy(float amount)
    {
        CurEnergy = Mathf.Clamp(CurEnergy + Mathf.Abs(amount), 0, _maxEnergy);
    }

    public void SubtractEnergy(float amount)
    {
        CurEnergy = Mathf.Clamp(CurEnergy - Mathf.Abs(amount), 0, _maxEnergy);
    }

    private void HandleRegeneration(float amount)
    {
        if (CurEnergy < _naturalRegenerationCeiling)
        {
            CurEnergy = Mathf.Clamp(CurEnergy + amount, 0, _naturalRegenerationCeiling);
        }
    }
}
