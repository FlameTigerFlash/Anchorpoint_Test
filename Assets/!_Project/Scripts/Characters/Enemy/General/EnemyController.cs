using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, INavigate, IWatch, IMeleeAttack
{
    [SerializeField] private NavMeshAgent _navigator;
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private NPCVision _lookRotate;

    [SerializeField] private StateName _initialState = StateName.Idle;

    [SerializeField] private IdleState _idleState;
    [SerializeField] private ChaseState _chaseState;
    [SerializeField] private MeleeAttackState _attackState;

    [SerializeField] private float _executionDelay = 0.3f;

    public NavMeshAgent Navigator => _navigator;

    public MeleeAttack MeleeAttack => _meleeAttack;

    public NPCVision Watch => _lookRotate;

    private BaseState _currentState;

    private StateName _currentStateType;

    private void Start()
    {
        OnChangeState(_initialState);
        StartCoroutine(StateExecutionCycle(_executionDelay));
    }

    public void OnChangeState(StateName newStateType)
    {
        if (newStateType == _currentStateType && _currentState != null)
        {
            return;
        }
        switch (newStateType)
        {
            case StateName.Idle:
                ChangeState(_idleState);
                break;
            case StateName.Chase:
                ChangeState(_chaseState);
                break;
            case StateName.Attack:
                ChangeState(_attackState);
                break;
            default:
                return;
        }
        _currentStateType = newStateType;
    }

    private void ChangeState(BaseState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }

    private IEnumerator StateExecutionCycle(float delay)
    {
        while (true)
        {
            if (_currentState != null)
            {
                _currentState.Execute();
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
