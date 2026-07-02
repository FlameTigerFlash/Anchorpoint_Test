using UnityEngine;

public class MeleeAttackState : BaseState
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
            _controller.OnChangeState(StateName.Idle);
        }
        else
        {
            _controller.Navigator.SetDestination(_player.transform.position);
            _controller.Watch.SetTarget(_player);
        }
    }

    public override void Execute()
    {
        if (_player == null)
        {
            _controller.OnChangeState(StateName.Idle);
            return;
        }

        bool targetReachable = _controller.Watch.GetTargetRayCollision(out var hitPos, _controller.MeleeAttack.AttackRange);
        if (targetReachable)
        {
            _controller.MeleeAttack.TryAttack(hitPos);
        }
        else
        {
            _controller.OnChangeState(StateName.Chase);
        }

    }
}
