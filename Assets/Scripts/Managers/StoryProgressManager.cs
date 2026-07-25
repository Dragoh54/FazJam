using System.Collections.Generic;
using Story;
using UnityEngine;

namespace Managers
{
    public class StoryProgressManager : MonoBehaviour
    {
        [Header("Requirements")]
        private readonly HashSet<StoryFlag> _flags = new();

        public void AddFlag(StoryFlag flag)
        {
            _flags.Add(flag);
            Debug.Log($"Adding flag {flag}");
        }

        public void RemoveFlag(StoryFlag flag)
        {
            _flags.Remove(flag);
        }

        public bool HasFlag(StoryFlag flag)
        {
            return _flags.Contains(flag);
        }
    }
}
