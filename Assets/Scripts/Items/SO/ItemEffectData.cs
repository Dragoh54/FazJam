using UnityEngine.Serialization;

namespace Items
{
    [System.Serializable]
    public class ItemEffectData
    {
        [FormerlySerializedAs("effect")] public ItemEffectType effectType;
        public int value;
    }
}