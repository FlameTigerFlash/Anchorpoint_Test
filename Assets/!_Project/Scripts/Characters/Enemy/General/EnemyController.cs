using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, INavigate, IWatch, IMeleeAttack
{
    [SerializeField] private NavMeshAgent _navigator;
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private NPCVision _lookRotate;

    [SerializeField] private IdleState _idleState;
    [SerializeField] private ChaseState _chaseState;
    [SerializeField] private MeleeAttackState _attackState;

    [SerializeField] private SoundBucket _walkingSoundBucket;

    [SerializeField] private StateName _initialState = StateName.Idle;

    [SerializeField] private float _executionDelay = 0.3f;

    [SerializeField] private bool _activeAtStart = true;

    public NavMeshAgent Navigator => _navigator;

    public MeleeAttack MeleeAttack => _meleeAttack;

    public NPCVision Watch => _lookRotate;

    public bool IsActive { get; protected set; }

    private BaseState _currentState;

    private StateName _currentStateType;

    private void Start()
    {
        IsActive = _activeAtStart;
        OnChangeState(_initialState);
        StartCoroutine(StateExecutionCycle(_executionDelay));
    }

    private void Update()
    {
        if (!Navigator.isStopped)
        {
            _walkingSoundBucket.StartPlaying();
        }
        else
        {
            _walkingSoundBucket.StopPlaying();
        }
    }

    public void SetActive(bool active)
    {
        IsActive = active;
    }

    public void OnChangeState(StateName newStateType)
    {
        if (!IsActive && newStateType != StateName.Idle && _currentStateType == StateName.Idle)
        {
            return;
        }
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
