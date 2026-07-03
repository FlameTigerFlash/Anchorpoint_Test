using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private int _initialHealth = 1;

    [SerializeField] private bool _destroyIfNoHP = true;

    public UnityEvent<int> HealthChangedEvent;
    public UnityEvent DeathEvent;

    public int Health
    {
        get
        {
            return _health;
        }
        protected set
        {
            int temp = _health;
            _health = Mathf.Min(value, _maxHealth);
            
            if (temp != _health)
            {
                HealthChangedEvent.Invoke(_health);
            }
            if (_health <= 0)
            {
                Die();
            }
        }
    }

    private int _health;

    private void Awake()
    {
        ResetToInitial();
    }

    public void ResetToInitial()
    {
        Health = Mathf.Clamp(_initialHealth, 1, _maxHealth);
    }

    public void ReceiveDamage(int damage)
    {
        Health -= Mathf.Abs(damage);
    }

    public void Heal(int amount)
    {
        Health += Mathf.Abs(amount);
    }

    public void Die()
    {
        DeathEvent.Invoke();
        if (_destroyIfNoHP)
        {
            Destroy(gameObject);
        }
    }
}
