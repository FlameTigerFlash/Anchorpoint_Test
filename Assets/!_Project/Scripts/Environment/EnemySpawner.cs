using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private Transform _spawnPoint;

    private Transform _enemiesFolder;

    private void Start()
    {
        MapLocator mapLocator = ServiceLocator.GetService<MapLocator>();
        if (mapLocator != null)
        {
            _enemiesFolder = ServiceLocator.GetService<MapLocator>().EnemiesFolder;
        }
    }

    public GameObject SpawnEnemy()
    {
        GameObject enemy;
        if (_enemiesFolder != null)
        {
            enemy = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation, _enemiesFolder);
        }
        else
        {
            enemy = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
            Debug.LogWarning("No enemies folder specified.");
        }

        return enemy;
    }
}
