using UnityEngine;

namespace Items.SO.Effects
{
    [CreateAssetMenu(menuName = "Items/Effects/Restore To Max Steps")]
    public class RestoreToMaxStepsEffect : ItemEffectData
    {
        public override void Apply(StepManager stepManager)
        {
            stepManager.RestoreToMaxSteps();
        }
    }
}