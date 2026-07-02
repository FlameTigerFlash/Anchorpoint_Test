using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabs;

    [SerializeField] private Transform _spawnPoint;

    [SerializeField, Range(1f, float.MaxValue)] private float _minSpawnCooldown = 10f;
    [SerializeField, Range(1f, float.MaxValue)] private float _maxSpawnCooldown = 20f;

    private GameObject _curCollectible;

    private bool _isInProcess = false;

    private void Awake()
    {
        if (_minSpawnCooldown > _maxSpawnCooldown)
        {
            (_minSpawnCooldown, _maxSpawnCooldown) = (_maxSpawnCooldown, _minSpawnCooldown);
        }
    }

    private void Update()
    {
        if (!_isInProcess && _curCollectible == null)
        {
            float delay = Random.Range(_minSpawnCooldown, _maxSpawnCooldown);
            StartCoroutine(SpawnCoroutine(delay));
        }
    }

    private void SpawnCollectible()
    {
        if (_prefabs.Count == 0)
        {
            Debug.LogWarning("Could not find collectible prefabs for spawning.");
            return;
        }

        int ind = Random.Range(0, _prefabs.Count);
        _curCollectible = Instantiate(_prefabs[ind], _spawnPoint.position, _spawnPoint.rotation);
    }

    private IEnumerator SpawnCoroutine(float delay)
    {
        _isInProcess = true;
        yield return new WaitForSeconds(delay);
        SpawnCollectible();
        _isInProcess = false;
    }
}
