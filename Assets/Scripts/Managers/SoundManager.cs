using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _MusicSource;
    [SerializeField] private AudioSource _BackgroundSource;
    [SerializeField] private AudioSource _WalkingSource;
    [SerializeField] private AudioSource[] _SFXChannels;

    [SerializeField] private AudioClip MusicClip;
    [SerializeField] private AudioClip BackgroundClip;
    [SerializeField] private AudioClip WalkingClip;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _MusicSource.clip = MusicClip;
        _MusicSource.Play();

        _WalkingSource.clip = WalkingClip;

        //PlayBackgroundAmbience(BackgroundClip);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        foreach (var channel in _SFXChannels)
        {
            if (!channel.isPlaying)
            {
                channel.PlayOneShot(audioClip);

                return;
            }
        }
    }

    public void PlayBackgroundAmbience(AudioClip clip, bool loop = true)
    {
        if (_BackgroundSource.clip != clip)
        {
            _BackgroundSource.clip = clip;
        }

        _BackgroundSource.loop = loop;
        _BackgroundSource.Play();
    }

    public void StartWalkingSound()
    {
        _WalkingSource.Play();
    }

    public void StopWalkingSound()
    {
        Debug.Log("Stop");
        _WalkingSource.Stop();
    }
}
