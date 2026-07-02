using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

public class InvasionEvent : BaseEvent
{
    [SerializeField, NotNull] private List<Transform> _enemySpawnPoints;

    [SerializeField, Range(1, 10)] private int _minEnemies = 2;
    [SerializeField, Range(1, 10)] private int _maxEnemies = 3;

    [SerializeField, Range(0f, float.MaxValue)] float _duration = 10f;
    //[SerializeField, Range(0f, 1)] private float _spawnInterval = 0.1f;

    public override string EventDescription
    {
        get
        {
            return "The enemies are coming! Defend yourself at all cost!";
        }

    }

    private List<GameObject> _enemies = new List<GameObject>();

    private void Awake()
    {
        if (_minEnemies > _maxEnemies)
        {
            (_minEnemies, _maxEnemies) = (_maxEnemies, _minEnemies);
        }
    }

    private void SpawnEnemies()
    {
        int enemiesNum = Random.Range(_minEnemies, _maxEnemies + 1);
        int ind = 0;
        for (int i = 0; i < enemiesNum; i++)
        {
            ind = Random.Range(0, _enemySpawnPoints.Count);

            Vector3 pos = _enemySpawnPoints[ind].position;
            Quaternion rot = _enemySpawnPoints[ind].rotation;

            GameObject enemy = EnemyFactory.Instance.SpawnEnemy(pos, rot);
            _enemies.Add(enemy);
        }
    }

    private void Cleanup()
    {
        foreach (GameObject enemy in _enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        _enemies.Clear();
    }

    protected override void Initiate()
    {
        Cleanup();
        SpawnEnemies();
        StartCoroutine(DurationCoroutine(_duration));
    }

    private IEnumerator DurationCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        Cleanup();
        IsFinished = true;
    }
}
