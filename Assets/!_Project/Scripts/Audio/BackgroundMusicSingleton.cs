using UnityEngine;

public class BackgroundMusicSingleton : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public static BackgroundMusicSingleton Instance => _instance;

    private static BackgroundMusicSingleton _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartPlaying();
    }

    public void StartPlaying()
    {
        _audioSource.Play();
    }

    public void StopPlaying()
    {
        _audioSource.Stop();
    }
}
