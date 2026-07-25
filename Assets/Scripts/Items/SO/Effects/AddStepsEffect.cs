using UnityEngine;

namespace Items.SO.Effects
{
    [CreateAssetMenu(menuName = "Items/Effects/Add Steps")]
    public class AddStepsEffect : ItemEffectData
    {
        [Min(0)]
        public int value;

        public override void Apply(StepManager stepManager)
        {
            stepManager.AddSteps(value);
        }
    }
}