using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushEvent : BaseEvent
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField, Range(1f, 20f)] private float _ambushDistance = 10f;
    [SerializeField, Range(5f, float.MaxValue)] float _duration = 10f;

    [SerializeField, Range(1, 10)] private int _enemiesCount = 1;

    public override string EventDescription 
    { 
        get
        {
            return "Ambush!";
        }
     }

    private List<GameObject> _enemies = new List<GameObject>();

    private MapLocator _mapLocator;

    private GameObject _player;

    private void Start()
    {
        _mapLocator = ServiceLocator.GetService<MapLocator>();
        if (_mapLocator == null)
        {
            Debug.LogWarning("Cannot find map locator.");
        }
    }

    protected override void Initiate()
    {
        _player = _mapLocator.GetPlayer();
        if (_player == null)
        {
            Debug.LogWarning("Cannot find the player.");
            IsFinished = true;
            return;
        }

        float angle = 90f;
        List<Vector3> points = NavMeshSampler.FindPointsInRadius(_player.transform.position, -_player.transform.forward, _ambushDistance, angle, Mathf.Min(10, _enemiesCount * 2));

        if (points.Count == 0)
        {
            Debug.Log("Cannot spawn nearby.");
            IsFinished = true;
            return;
        }

        for (int i = 0; i < _enemiesCount; i++)
        {
            Vector3 spawnPoint = points[i % points.Count];
            GameObject enemy = EnemyFactory.Instance.SpawnEnemy(spawnPoint, Quaternion.identity);
            _enemies.Add(enemy);
        }
        StartCoroutine(DurationCoroutine(_duration));
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

    private IEnumerator DurationCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        Cleanup();
        IsFinished = true;
    }
}
