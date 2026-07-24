using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace ItemGeneration
{
    [System.Serializable]
    public class ItemGenerationData
    {
        public ItemData item;
        
        [Min(0)]
        [SerializeField] public int maxCount = 1;
        
        //todo: random weights for future
    }
}