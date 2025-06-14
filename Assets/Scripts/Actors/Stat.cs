using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [System.Serializable]
    public class Stat
    {
        [field: SerializeField]
        public float BaseValue { get; set; }
        public List<(float, bool)> PlusModifiers { get; private set; }
        public List<(float, bool)> MultiplyModifiers { get; private set; }

        public Stat(float baseValue)
        {
            BaseValue = baseValue;
            PlusModifiers = new List<(float, bool)>();
            MultiplyModifiers = new List<(float, bool)>();
        }

        public void AddPlusModifier((float value, bool isPermanent) modifier) => PlusModifiers.Add(modifier);

        public void AddMultiplyModifier((float value, bool isPermanent) modifier) => MultiplyModifiers.Add(modifier);

        public void RemovePlusModifier((float value, bool isPermanent) modifier) => PlusModifiers.Remove(modifier);

        public void RemoveMultiplyModifier((float value, bool isPermanent) modifier) => MultiplyModifiers.Remove(modifier);

        public float GetValue()
        {
            float value = BaseValue;
            foreach ((float value, bool isPermanent) modifier in PlusModifiers)
            {
                value += modifier.value;
            }
            foreach ((float value, bool isPermanent) modifier in MultiplyModifiers)
            {
                value *= modifier.value;
            }
            return value;
        }

        public void ClearTemporaryModifiers()
        {
            // Remove non-permanent modifiers
            PlusModifiers.RemoveAll(modifier => !modifier.Item2);
            MultiplyModifiers.RemoveAll(modifier => !modifier.Item2);
        }
    }
}
