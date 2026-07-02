using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ResourcesFoundEvent : BaseEvent
{
    [SerializeField] private List<GameObject> _resourcesPrefabs;

    [SerializeField, Range(1f, 20f)] private float _spawnDistance = 10f;
    [SerializeField, Range(5f, float.MaxValue)] float _duration = 10f;

    public override string EventDescription
    {
        get
        {
            return "You have found some hidden resources!";
        }
    }

    private GameObject _resource;

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

        float angle = 180f;
        List<Vector3> points = NavMeshSampler.FindPointsInRadius(_player.transform.position, _player.transform.forward, _spawnDistance, angle, 10);

        if (points.Count == 0)
        {
            Debug.Log("Cannot spawn nearby.");
            IsFinished = true;
            return;
        }

        Vector3 spawnPoint = points[Random.Range(0, points.Count)];
        _resource = Instantiate(_resourcesPrefabs[Random.Range(0, _resourcesPrefabs.Count)], spawnPoint, Quaternion.identity);

        StartCoroutine(DurationCoroutine(_duration));
    }

    private void Cleanup()
    {
        if (_resource != null)
        {
            Destroy(_resource);
        }
    }

    private IEnumerator DurationCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        Cleanup();
        IsFinished = true;
    }
}
