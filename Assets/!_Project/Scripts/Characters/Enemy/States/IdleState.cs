using UnityEngine;

public class IdleState : BaseState
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
        _controller.Navigator.isStopped = true;
    }

    public override void Execute()
    {
        if (_mapLocator == null)
        {
            Debug.LogWarning("Cannot find Map Locator.");
            _mapLocator = ServiceLocator.GetService<MapLocator>();
            return;
        }
        if (_player == null)
        {
            _player = _mapLocator.GetPlayer();
        }
        if (_player != null)
        {
            _controller.OnChangeState(StateName.Chase);
        }
    }
}
