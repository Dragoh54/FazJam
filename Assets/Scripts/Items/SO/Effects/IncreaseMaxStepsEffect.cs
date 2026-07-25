using UnityEngine;

namespace Items.SO.Effects
{
    [CreateAssetMenu(menuName = "Items/Effects/Increase Max Steps")]
    public class IncreaseMaxStepsEffect : ItemEffectData
    {
        [Min(0)]
        public int value;

        public override void Apply(StepManager stepManager)
        {
            stepManager.IncreaseMaxSteps(value);
        }
    }
}