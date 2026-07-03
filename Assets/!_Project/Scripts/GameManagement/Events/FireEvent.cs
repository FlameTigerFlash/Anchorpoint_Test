using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireEvent : BaseEvent
{
    [SerializeField] private GameObject _firePrefab;

    [SerializeField] private List<Transform> _points = new List<Transform>();

    [SerializeField, Range(0f, float.MaxValue)] private float _burningTime = 30f;

    public UnityEvent BaseBurnedDownEvent;

    private GameObject _fire;

    public override string EventDescription
    {
        get
        {
            return "Your base is on fire! Extinguish the fire before it is too late!";
        }
    }

    public void OnFireExtinguished()
    {
        if (_fire == null)
        {
            StopCoroutine(DurationCoroutine(_burningTime));
            IsFinished = true;
        }
    }

    protected override void Initiate()
    {
        Transform point = _points[Random.Range(0, _points.Count)];

        _fire = Instantiate(_firePrefab, point.position, point.rotation);
        _fire.GetComponent<ExtinguishableObject>().ExtinguishedEvent.AddListener(OnFireExtinguished);

        StartCoroutine(DurationCoroutine(_burningTime));
    }

    private IEnumerator DurationCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        if (_fire != null)
        {
            BaseBurnedDownEvent.Invoke();
        }

        IsFinished = true;
    }
}
