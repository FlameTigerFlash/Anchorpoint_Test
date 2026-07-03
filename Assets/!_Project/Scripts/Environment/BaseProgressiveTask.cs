using UnityEngine;
using UnityEngine.Events;

public abstract class BaseProgressiveTask : MonoBehaviour
{
    [SerializeField, Min(float.Epsilon)] private float _completeionTime;

    public UnityEvent<float> ProgressChangedEvent;
    public UnityEvent TaskFinishedEvent;

    public float Progress
    {
        get => _progress;
        protected set
        {
            float temp = _progress;
            _progress = value;
            if (temp != _progress)
            {
                ProgressChangedEvent.Invoke(_progress / _completeionTime);
            }
            if (_progress >= _completeionTime)
            {
                Finish();
            }
        }
    }

    private float _progress = 0;

    protected virtual void Start()
    {
        Progress = 0;
        ProgressChangedEvent.Invoke(Progress / _completeionTime);
    }

    protected virtual void Finish()
    {
        TaskFinishedEvent.Invoke();
        Progress = 0;
    }
}
