using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEvent : MonoBehaviour
{
    public UnityEvent EventFinishedEvent;

    public virtual string EventDescription 
    { 
        get
        {
            return "Some event.";
        }
    }

    public bool IsFinished
    {
        get
        {
            return _isFinished;
        }
        set
        {
            if (_isFinished == value)
            {
                return;
            }
            _isFinished = value;
            if (_isFinished)
            {
                EventFinishedEvent.Invoke();
            }
        }
    }

    private bool _isFinished = false;

    public void OnInitiate()
    {
        IsFinished = false;
        Initiate();
    }

    protected abstract void Initiate();
}
