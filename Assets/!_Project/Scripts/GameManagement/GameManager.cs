using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private MapLocator _mapLocator;

    [SerializeField] private SceneChanger _sceneChanger;

    [SerializeField] private float _gameEndingDelay = 2f;
    
    [SerializeField, Range(1, 20)] private int _collectedCrystalsGoal = 2;

    public int CollectedCrystalsGoal => _collectedCrystalsGoal;

    public bool IsFinished {  get; private set; }

    private void Awake()
    {
        IsFinished = false;
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
        if (!IsFinished)
        {
            IsFinished = true;
            StartCoroutine(GameEndingCoroutine(_sceneChanger.OnDefeatScreen, _gameEndingDelay));
        }
    }

    private void SetVictory()
    {
        if (!IsFinished)
        {
            IsFinished = true;
            StartCoroutine(GameEndingCoroutine(_sceneChanger.OnVictoryScreen, _gameEndingDelay));
        }
    }

    private void SetupServices()
    {
        ServiceLocator.RegisterService<GameManager>(this);
        ServiceLocator.RegisterService<StatsManager>(_statsManager);
        ServiceLocator.RegisterService<MapLocator>(_mapLocator);
        ServiceLocator.RegisterService<SceneChanger>(_sceneChanger);
    }

    private IEnumerator GameEndingCoroutine(Action action, float delay)
    {
        IsFinished = true;
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}
