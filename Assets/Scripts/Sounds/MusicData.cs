using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(menuName = "Sounds/Music Data")]
    public class MusicData : SoundData
    {
        [Header("Music")]
        [SerializeField] private AudioClip[] musicTracks;
        
        [Header("Transition")]
        [SerializeField] private float fadeDuration = 2f;
        
        public AudioClip GetRandomTrack()
        {
            if (musicTracks == null || musicTracks.Length == 0)
                return null;

            return musicTracks[Random.Range(0, musicTracks.Length)];
        }

        public float FadeDuration => fadeDuration;
    }
}