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

    [SerializeField] private SoundData MusicClip;
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
        PlayLoop(_MusicSource, MusicClip);
        PlayLoop(_WalkingSource, WalkingClip);

        _WalkingSource.mute = true;

        //PlayBackgroundAmbience(background);
    }

    // private void Start()
    // {
    //     _MusicSource.clip = MusicClip;
    //     _MusicSource.Play();
    //
    //     _WalkingSource.clip = WalkingClip;
    //     _WalkingSource.Play();
    //     _WalkingSource.mute = true;
    //
    //     //PlayBackgroundAmbience(BackgroundClip);
    // }
    
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

    // public void PlaySFX(AudioClip audioClip)
    // {
    //     foreach (var channel in _SFXChannels)
    //     {
    //         if (!channel.isPlaying)
    //         {
    //             channel.PlayOneShot(audioClip);
    //
    //             return;
    //         }
    //     }
    // }
    
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

    // public void PlayBackgroundAmbience(AudioClip clip, bool loop = true)
    // {
    //     if (_BackgroundSource.clip != clip)
    //     {
    //         _BackgroundSource.clip = clip;
    //     }
    //
    //     _BackgroundSource.loop = loop;
    //     _BackgroundSource.Play();
    // }

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

    private IEnumerator CheckMusicEnd(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        PlayLoop(_MusicSource, MusicClip);
    }
}
