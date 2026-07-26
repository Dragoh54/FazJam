using Sounds;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _MusicSource;
    [SerializeField] private AudioSource _BackgroundSource;
    [SerializeField] private AudioSource _WalkingSource;
    [SerializeField] private AudioSource[] _SFXChannels;

    [SerializeField] private MusicData MusicClip;
    [SerializeField] private SoundData BackgroundClip;
    [SerializeField] private SoundData WalkingClip;

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
        StartCoroutine(MusicLoop());
        
        PlayLoop(_WalkingSource, WalkingClip);

        _WalkingSource.mute = true;
    }
    
    public void PlaySFX(SoundData sound)
    {
        if (sound == null)
            return;

        var clip = sound.GetRandomClip();

        if (clip == null)
            return;

        foreach (var channel in _SFXChannels)
        {
            if (channel.isPlaying)
                continue;

            channel.pitch = sound.GetRandomPitch();
            channel.PlayOneShot(clip, sound.Volume);

            return;
        }
        
        _SFXChannels[0].pitch = sound.GetRandomPitch();
        _SFXChannels[0].PlayOneShot(clip, sound.Volume);
    }
    
    
    public void PlayBackgroundAmbience(SoundData sound, bool loop = true)
    {
        if (sound == null)
            return;

        var clip = sound.GetRandomClip();

        if (clip == null)
            return;

        _BackgroundSource.clip = clip;
        _BackgroundSource.pitch = sound.GetRandomPitch();
        _BackgroundSource.volume = sound.Volume;
        _BackgroundSource.loop = loop;
        _BackgroundSource.Play();
    }

    public void StartWalkingSound()
    {
        _WalkingSource.mute = false;
    }

    public void StopWalkingSound()
    {
        _WalkingSource.mute = true;
    }
    
    private void PlayLoop(AudioSource source, SoundData sound)
    {
        if (sound == null)
            return;

        var clip = sound.GetRandomClip();

        if (clip == null)
            return;

        source.clip = clip;
        source.pitch = sound.GetRandomPitch();
        source.volume = sound.Volume;
        source.loop = true;
        source.Play();

        StartCoroutine(CheckMusicEnd(clip.length));
    }
    
    private IEnumerator MusicLoop()
    {
        while (true)
        {
            var clip = MusicClip.GetRandomTrack();

            if (clip == null)
                yield break;

            _MusicSource.clip = clip;
            _MusicSource.volume = 0;
            _MusicSource.Play();

            yield return StartCoroutine(FadeMusic(0, MusicClip.Volume, MusicClip.FadeDuration));

            var waitTime = clip.length - MusicClip.FadeDuration * 2f;

            if (waitTime > 0)
                yield return new WaitForSeconds(waitTime);

            yield return StartCoroutine(FadeMusic(MusicClip.Volume, 0, MusicClip.FadeDuration));

            _MusicSource.Stop();
        }
    }
    
    private IEnumerator FadeMusic(float from, float to, float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            _MusicSource.volume = Mathf.Lerp(
                from,
                to,
                timer / duration);

            yield return null;
        }

        _MusicSource.volume = to;
    }

    private IEnumerator CheckMusicEnd(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        PlayLoop(_MusicSource, MusicClip);
    }
}
