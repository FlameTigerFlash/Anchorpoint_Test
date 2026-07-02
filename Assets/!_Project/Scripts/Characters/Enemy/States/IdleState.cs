using UnityEngine;

public class IdleState : BaseState
{
    private MapLocator _mapLocator;

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
        GameObject player = _mapLocator.GetPlayer();
        if (player != null)
        {
            _controller.OnChangeState(StateName.Chase);
        }
        else
        {
            Debug.LogWarning("Could not find the player.");
        }
    }
}
