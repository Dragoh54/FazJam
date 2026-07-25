using UnityEngine;

namespace Items.SO.Effects
{
    public abstract class ItemEffectData : ScriptableObject
    {
        public abstract void Apply(StepManager stepManager);
    }
}