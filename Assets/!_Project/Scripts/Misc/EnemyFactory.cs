using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private Transform _enemiesFolder;

    public static EnemyFactory Instance => _instance;

    private static EnemyFactory _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public GameObject SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject enemy;
        if (_enemiesFolder == null)
        {
            enemy = Instantiate(_enemyPrefab, position, rotation);
        }
        else
        {
            enemy = Instantiate(_enemyPrefab, position, rotation, _enemiesFolder);
        }

        return enemy;
    }
}
