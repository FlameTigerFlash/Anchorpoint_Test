using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class SoundBucket : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField, NotNull] private List<AudioClip> _clips;

    [SerializeField, Min(float.Epsilon)] private float _delay = 0.5f;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        IsActive = false;
    }

    public void StartPlaying()
    {
        if (!IsActive)
        {
            IsActive = true;
            StartCoroutine(SoundCoroutine(_delay));
        }
    }

    public void StopPlaying()
    {
        if (IsActive)
        {
            IsActive = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator SoundCoroutine(float delay)
    {
        while (true)
        {
            PlayRandomClip();
            yield return new WaitForSeconds(delay);
        }
    }

    private void PlayRandomClip()
    {
        AudioClip clip = _clips[Random.Range(0, _clips.Count)];
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
