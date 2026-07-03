using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAlarm : MonoBehaviour
{
    [SerializeField] private NPCVision _vision;

    [SerializeField, Min(float.Epsilon)] private float _updateDelay = 1f;

    public UnityEvent PlayerDetectedEvent;

    private GameObject _detectedPlayer;

    private void Start()
    {
        StartCoroutine(UpdateCoroutine(_updateDelay));
    }

    public void OnPlayerEnteredZone(GameObject player)
    {
        _detectedPlayer = player;
    }

    public void OnPlayerLeftZone(GameObject player)
    {
        if (player == _detectedPlayer)
        {
            _detectedPlayer = null;
        }
    }

    private void UpdateInfo()
    {
        if (_vision.GetTargetRayCollision(_detectedPlayer, out var hitPos, float.MaxValue))
        {
            PlayerDetectedEvent.Invoke();
            StopAllCoroutines();
        }
    }

    private IEnumerator UpdateCoroutine(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (_detectedPlayer != null)
            {
                UpdateInfo();
            }
        }
    }
}
