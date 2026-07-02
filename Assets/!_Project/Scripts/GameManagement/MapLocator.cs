using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MapLocator : MonoBehaviour
{
    [SerializeField] private Transform _enemiesFolder;

    [SerializeField] private string _enemyTag = "Enemy";

    [SerializeField] private float _refreshCooldown = 10f;

    public UnityEvent NoEnemiesLeftEvent;

    public Transform EnemiesFolder => _enemiesFolder;

    private List<GameObject> _enemies = new List<GameObject>();

    private GameObject _player;

    private void Start()
    {
        StartCoroutine(RefreshCoroutine(_refreshCooldown));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public List<GameObject> GetEnemiesList()
    {
        if (_enemies.Count == 0)
        {
            RefreshEnemiesList();
        }
        return new List<GameObject>(_enemies);
    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    public void AddEnemy(GameObject enemy)
    {
        if (enemy == null || _enemies.Contains(enemy))
        {
            return;
        }

        _enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemy == null || !_enemies.Contains(enemy))
        {
            return;
        }

        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
        {
            NoEnemiesLeftEvent.Invoke();
        }
    }

    public void SetPlayerLink(GameObject player)
    {
        _player = player;
    }

    public void RefreshEnemiesList()
    {
        List<GameObject> newEnemiesList = new List<GameObject>();

        if (_enemiesFolder == null)
        {
            GameObject.FindGameObjectsWithTag(_enemyTag, newEnemiesList);
        }
        else
        {
            Transform[] allChildren = _enemiesFolder.GetComponentsInChildren<Transform>(true);

            foreach (Transform child in allChildren)
            {
                if (child.CompareTag(_enemyTag))
                {
                    newEnemiesList.Add(child.gameObject);
                }
            }
        }

        _enemies = new List<GameObject>(newEnemiesList);

        if (_enemies.Count == 0)
        {
            NoEnemiesLeftEvent.Invoke();
        }
    }

    public void FindPlayer()
    {
        if (_player != null)
        {
            return;
        }
        _player = GameObject.FindWithTag("Player");
    }

    private IEnumerator RefreshCoroutine(float delay)
    {
        while (true)
        {
            RefreshEnemiesList();
            FindPlayer();
            yield return new WaitForSeconds(delay);
        }
    }
}
