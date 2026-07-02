using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private LogsManager _logsManager;

    [SerializeField, Range(1f, float.MaxValue)] private float _minInterval = 10f;
    [SerializeField, Range(1f, float.MaxValue)] private float _maxInterval = 20f;

    [SerializeField] private List<BaseEvent> EventsAtBase = new List<BaseEvent>();
    [SerializeField] private List<BaseEvent> EventsOutsideBase = new List<BaseEvent>();

    [SerializeField] private bool _waitUntilFinished = true;

    private BaseEvent _currentEvent;

    private bool _atBase = false;

    private void Awake()
    {
        if (_minInterval > _maxInterval)
        {
            (_minInterval, _maxInterval) = (_maxInterval, _minInterval);
        }
    }

    private void Start()
    {
        StartCoroutine(EventCooldown());
    }

    private void OnDestroy()
    {
        if (_currentEvent != null)
        {
            _currentEvent.EventFinishedEvent.RemoveListener(SetFinished);
        }
        StopAllCoroutines();
    }

    public void SetAtBase()
    {
        _atBase = true;
    }

    public void SetAwayFromBase()
    {
        _atBase = false;
    }

    public void SetFinished()
    {
        if (_waitUntilFinished)
        {
            StopCoroutine(EventCooldown());
            StartCoroutine(EventCooldown());
        }
    }

    private BaseEvent RandomFromList(List<BaseEvent> eventsList)
    {
        if (eventsList.Count == 0)
        {
            Debug.LogWarning("Events list empty.");
            return null;
        }

        int ind = Random.Range(0, eventsList.Count);
        return eventsList[ind];
    }

    private bool TryChooseNextEvent()
    {
        if (_atBase)
        {
            BaseEvent nextEvent = RandomFromList(EventsAtBase);
            if (nextEvent == null)
            {
                Debug.LogWarning("Events at base list empty.");
                return false;
            }
            SetEvent(nextEvent);
        }
        else
        {
            BaseEvent nextEvent = RandomFromList(EventsOutsideBase);
            if (nextEvent == null)
            {
                Debug.LogWarning("Events outside base list empty.");
                return false;
            }
            SetEvent(nextEvent);
        }

        if (!_waitUntilFinished)
        {
            StopCoroutine(EventCooldown());
            StartCoroutine(EventCooldown());
        }

        return true;
    }

    private void SetEvent(BaseEvent newEvent)
    {
        if (newEvent == null)
        {
            return;
        }
        if (_currentEvent != null)
        {
            _currentEvent.EventFinishedEvent.RemoveListener(SetFinished);
        }
        _currentEvent = newEvent;
        _currentEvent.EventFinishedEvent.AddListener(SetFinished);
        _currentEvent.OnInitiate();
        _logsManager.AddLog(_currentEvent.EventDescription);
    }

    private IEnumerator EventCooldown()
    {
        float delay = Random.Range(_minInterval, _maxInterval);

        bool flag = false;
        while (!flag)
        {
            yield return new WaitForSeconds(delay);
            flag = TryChooseNextEvent();
        }
    }
}
