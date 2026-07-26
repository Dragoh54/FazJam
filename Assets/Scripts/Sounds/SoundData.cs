using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(menuName = "Sounds/Sound Data")]
    public class SoundData : ScriptableObject
    {
        [Header("Clips")]
        [SerializeField] private AudioClip[] clips;

        [Header("Settings")]
        [SerializeField] private Vector2 pitchRange = Vector2.one;
        
        [SerializeField] 
        [Range(0f, 1f)] 
        private float volume = 1f;

        public AudioClip GetRandomClip()
        {
            if (clips == null || clips.Length == 0)
                return null;

            return clips[Random.Range(0, clips.Length)];
        }

        public float GetRandomPitch()
        {
            return Random.Range(pitchRange.x, pitchRange.y);
        }

        public float Volume => volume;
    }
}