using UnityEngine;

public class ChaseState : BaseState
{
    private MapLocator _mapLocator;

    private GameObject _player;

    public override void EnterState()
    {
        base.EnterState();

        if (_mapLocator == null)
        {
            _mapLocator = ServiceLocator.GetService<MapLocator>();
        }

        _player = _mapLocator.GetPlayer();
        if (_player == null)
        {
            Debug.LogWarning("Could not find the player.");
            _controller.OnChangeState(StateName.Idle);
        }
        else
        {
            _controller.Navigator.isStopped = false;
            _controller.Navigator.SetDestination(_player.transform.position);
        }
    }

    public override void Execute()
    {
        if (_player == null)
        {
            _controller.OnChangeState(StateName.Idle);
            return;
        }

        bool targetReachable = _controller.Watch.GetTargetRayCollision(_player, out var hitPos, _controller.MeleeAttack.AttackRange);
        if (targetReachable)
        {
            _controller.OnChangeState(StateName.Attack);
            return;
        }

        _controller.Navigator.SetDestination(_player.transform.position);
    }
}
