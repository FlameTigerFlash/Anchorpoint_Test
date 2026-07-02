using System.Collections.Generic;
using UnityEngine;

public class LogsManager : MonoBehaviour
{
    [SerializeField] private GameObject _logPrefab;

    [SerializeField] private Transform _container;

    [SerializeField] int _maxLogsNum = 5;

    private Queue<GameObject> _logs = new Queue<GameObject>();

    private void Awake()
    {
        _maxLogsNum = Mathf.Max(0, _maxLogsNum);
    }

    private void Update()
    {
        while (_logs.Count > 0 && _logs.Peek() == null)
        {
            _logs.Dequeue();
        }
    }

    public void AddLog(string text)
    {
        GameObject log = Instantiate(_logPrefab, _container);
        LogDisplay display = log.GetComponent<LogDisplay>();

        display.Display(text);

        while (_logs.Count >= _maxLogsNum)
        {
            GameObject top = _logs.Dequeue();
            Destroy(top);
        }

        _logs.Enqueue(log);
    }
}
