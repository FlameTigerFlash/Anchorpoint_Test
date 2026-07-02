using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private LayerMask _enemyLayerMask;

    [SerializeField] private string _attackTriggerName = "InitiateAttack";
    [SerializeField] private string _attackSpeedParameterName = "Speed";

    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _hitBoxRange = 1f;
    [SerializeField] private float _attackCooldown = 2f;

    [SerializeField] private int _attackDamage = 1;

    public float AttackRange => _attackRange;
    public float HitBoxRange => _hitBoxRange;

    public bool CanAttack { get; private set; }

    private Vector3 _lastAttackPos = Vector3.zero;

    private void Awake()
    {
        _animator.SetFloat(_attackSpeedParameterName, 1.0f / _attackCooldown);
        CanAttack = true;
    }

    private void OnDrawGizmos()
    {
        if (_lastAttackPos != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_lastAttackPos, _hitBoxRange);
        }
    }

    public void TryAttack(Vector3 targetPos)
    {
        if (CanAttack)
        {
            _animator.SetTrigger(_attackTriggerName);
            Attack(targetPos);
            StartCoroutine(AttackCooldownCoroutine(_attackCooldown));
            _lastAttackPos = targetPos;
        }
    }

    private Vector3 GetAttackPoint(Vector3 targetPos)
    {
        if (Vector3.Distance(targetPos, _startPoint.position) <= _attackRange)
        {
            return targetPos;
        }

        Vector3 direction = targetPos - _startPoint.position;

        return _startPoint.position + direction.normalized * _attackRange;
    }

    private void Attack(Vector3 targetPos)
    {
        Vector3 attackPoint = GetAttackPoint(targetPos);

        Collider[] hitColliders = Physics.OverlapSphere(attackPoint, _hitBoxRange, _enemyLayerMask);

        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent<CharacterHealth>(out var hp))
            {
                hp.ReceiveDamage(_attackDamage);
            }  
        }
    }

    private IEnumerator AttackCooldownCoroutine(float delay)
    {
        CanAttack = false;
        yield return new WaitForSeconds(delay);
        CanAttack = true;
    }
}
