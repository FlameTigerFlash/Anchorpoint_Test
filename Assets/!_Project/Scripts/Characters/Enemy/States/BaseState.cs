using UnityEngine;

public enum StateName {Idle, Chase, Attack};

[RequireComponent(typeof(EnemyController))]
public abstract class BaseState : MonoBehaviour
{
    protected EnemyController _controller;

    protected virtual void Awake()
    {
        _controller = GetComponent<EnemyController>();
    }

    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    public abstract void Execute();
}
