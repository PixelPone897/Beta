using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Scripts.Constants;

namespace Scripts
{
    [System.Serializable]
    public class Stat
    {
        [field: SerializeField]
        public float BaseValue { get; set; }
        [field: SerializeField]
        public List<StatModifier> PlusModifiers { get; private set; }
        [field: SerializeField]
        public List<StatModifier> MultiplyModifiers { get; private set; }

        public Stat(float baseValue)
        {
            BaseValue = baseValue;
            PlusModifiers = new List<StatModifier>();
            MultiplyModifiers = new List<StatModifier>();
        }

        public void AddPlusModifier(StatModifier modifier) => PlusModifiers.Add(modifier);

        public void AddMultiplyModifier(StatModifier modifier) => MultiplyModifiers.Add(modifier);

        public void RemovePlusModifier(StatModifier modifier) => PlusModifiers.Remove(modifier);

        public void RemoveMultiplyModifier(StatModifier modifier) => MultiplyModifiers.Remove(modifier);

        public float GetValue()
        {
            float value = BaseValue;
            foreach (StatModifier modifier in PlusModifiers)
            {
                value += modifier.Value;
            }
            foreach (StatModifier modifier in MultiplyModifiers)
            {
                value *= modifier.Value;
            }
            return value;
        }

        public float GetPermanentValue()
        {
            // Use LINQ method for practice
            // DOUBLE CHECK THIS!
            float value = BaseValue;
            float plusValue = PlusModifiers.Where(modifier => modifier.IsPermanent)
                .Select(modifier => modifier.Value).Sum();
            value += plusValue;

            float multiplyValue = MultiplyModifiers.Where(modifier => modifier.IsPermanent)
                .Select(modifier => modifier.Value).Aggregate(1f, (currentProd, nextValue) => currentProd * nextValue);

            value *= multiplyValue;
            return value;
        }

        public void ClearTemporaryModifiers()
        {
            // Remove non-permanent modifiers
            PlusModifiers.RemoveAll(modifier => !modifier.IsPermanent);
            MultiplyModifiers.RemoveAll(modifier => !modifier.IsPermanent);
        }

        public override string ToString()
        {
            string representation =
                $"Base Value: {BaseValue}, Permenant Value: {GetPermanentValue()} Value: {GetValue()}";
            return representation;
        }
    }

    [System.Serializable]
    public class StatModifier
    {
        [field: SerializeField]
        public float Value { get; private set; }
        [field: SerializeField]
        public bool IsPermanent { get; private set; }

        public StatModifier(float value, bool isPermanent)
        {
            Value = value;
            IsPermanent = isPermanent;
        }
    }

    [System.Serializable]
    public class Resource
    {
        [field: SerializeField]
        public Stat CurrentValue { get; set; }
        [field: SerializeField]
        public Stat MaxValue { get; set; }
        [field: SerializeField]
        public Stat MinValue { get; set; }

        public Resource()
        {
            MinValue = new Stat(RESOURCE_MIN_VALUE);
            MaxValue = new Stat(RESOURCE_MAX_VALUE);
            CurrentValue = new Stat(0);
        }

        public Resource(float currentValue)
        {
            MinValue = new Stat(RESOURCE_MIN_VALUE);
            MaxValue = new Stat(RESOURCE_MAX_VALUE);
            CurrentValue = new Stat(currentValue);
        }
    }
}
