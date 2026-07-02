using UnityEngine;
using System.Collections;

public abstract class BaseDiscreteAbility : MonoBehaviour
{
    [SerializeField] protected CharacterEnergy _characterEnergy;

    [SerializeField] protected float _cooldown = 2f;
    [SerializeField] protected float _energyCost = 10f;

    public bool CanPerform { get; protected set;}

    protected virtual void Awake()
    {
        CanPerform = true;
        if (_characterEnergy == null)
        {
            _characterEnergy = GetComponent<CharacterEnergy>();
        }
    }

    public virtual bool TryPerform()
    {
        if (_energyCost != 0 && _characterEnergy == null)
        {
            Debug.LogWarning("Energy-consuming ability should have a reference to a Character Energy component.");
            return false;
        }

        if (!(CanPerform && (_energyCost == 0 || _characterEnergy.CurEnergy >= _energyCost)))
        {
            return false;
        }

        if (_energyCost > 0)
        {
            _characterEnergy.SubtractEnergy(_energyCost);
        }
        else if (_energyCost < 0)
        {
            _characterEnergy.AddEnergy(-_energyCost);
        }

        Perform();
        StartCoroutine(CooldownCoroutine(_cooldown));

        return true;
    }

    protected abstract void Perform();

    private IEnumerator CooldownCoroutine(float delay)
    {
        CanPerform = false;
        yield return new WaitForSeconds(delay);
        CanPerform = true;
    }
}
