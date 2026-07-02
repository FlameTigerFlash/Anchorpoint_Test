using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private MapLocator _mapLocator;

    [SerializeField, Range(1, 20)] private int _collectedCrystalsGoal = 2;

    public int CollectedCrystalsGoal => _collectedCrystalsGoal;

    private void Awake()
    {
        SetupServices();
    }

    private void Update()
    {
        if (_statsManager.CollectedCrystals == _collectedCrystalsGoal)
        {
            SetVictory();
        }
    }

    public void SetDefeat()
    {
        Debug.Log("Defeat!");
    }

    private void SetVictory()
    {
        Debug.Log("Victory!");
    }

    private void SetupServices()
    {
        ServiceLocator.RegisterService<GameManager>(this);
        ServiceLocator.RegisterService<StatsManager>(_statsManager);
        ServiceLocator.RegisterService<MapLocator>(_mapLocator);
    }
}
