using Items.SO.Effects;
using UnityEngine;

namespace Items
{
    public abstract class EffectItemData : ItemData
    {
        [Header("Effect Item Data")]
        [SerializeField] private ItemEffectData[] effects;

        protected void ApplyEffects(StepManager stepManager)
        {
            foreach (var effect in effects)
            {
                effect.Apply(stepManager);
            }
        }
    }
}