using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Scripts.Constants;

namespace Scripts
{
    /// <summary>
    /// Logic class used for handling stats in system.
    /// </summary>
    /// <remarks>
    /// Given that stats in this system can be modified in a variety of ways
    /// through temporary and permanent modifiers, this class helps to organize,
    /// store, and handle that logic.
    /// </remarks>
    [System.Serializable]
    public class Stat
    {
        /// <summary>
        /// Base value of this stat, meaning this value without any permanent
        /// or temporary modifiers.
        /// </summary>
        [field: SerializeField]
        public float BaseValue { get; set; }

        /// <summary>
        /// Any addition or subtraction modifiers associated with this stat.
        /// </summary>
        [field: SerializeField]
        public List<StatModifier> PlusModifiers { get; private set; }

        /// <summary>
        /// Any multiplication or division modifiers associated with this stat.
        /// </summary>
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

        public void RemovePlusModifier(float value, bool isPermanentValue)
        {
            StatModifier found =
                PlusModifiers.FirstOrDefault(modifier => modifier.Value == value && modifier.IsPermanent == isPermanentValue);

            if (found != null)
            {
                PlusModifiers.Remove(found);
            }
        }

        /// <summary>
        /// Calculates and returns the value of this stat, including any permanent
        /// and temporary modifiers that are associated with it.
        /// </summary>
        /// <returns>The value of this value after applying any modifiers associated with it.</returns>
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

        /// <summary>
        /// Calculates and returns a value after applying all of the current
        /// modifiers associated with this stat.
        /// </summary>
        /// <param name="value">The base value being used.</param>
        /// <returns>The value after all current modifiers have been applied to it.</returns>
        public float ApplyModifiers(float value)
        {
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

        /// <summary>
        /// Calculates and returns the value of this stat, only accounting
        /// for any permanent modifiers associated with it.
        /// </summary>
        /// <returns>The value of this value after applying any
        /// permanent modifiers associated with it.</returns>s
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

        /// <summary>
        /// Clears out all temporary modifiers associated with this stat.
        /// </summary>
        /// <remarks>
        /// This is useful for temporary effects like status effects or temporary buffs.
        /// </remarks>
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

    /// <summary>
    /// Modifier for a Stat.
    /// </summary>
    /// <seealso cref="Stat"/>
    [System.Serializable]
    public class StatModifier
    {
        /// <summary>
        /// The value in which this modifier should change the Stat in question.
        /// </summary>
        [field: SerializeField]
        public float Value { get; private set; }

        /// <summary>
        /// If this modifier is considered "permanent" or not.
        /// </summary>
        /// <remarks>
        /// This is important with regards to calculating a Stat's permanent or temporary
        /// value (or even including both).
        /// </remarks>
        [field: SerializeField]
        public bool IsPermanent { get; private set; }

        public StatModifier(float value, bool isPermanent)
        {
            Value = value;
            IsPermanent = isPermanent;
        }
    }

    /// <summary>
    /// Represents a Stat that is part of defined range (Health, AP, etc).
    /// </summary>
    /// <seealso cref="Stat"/>
    /// <seealso cref="StatModifier"/>
    [System.Serializable]
    public class Resource
    {
        /// <summary>
        /// The current value of this Resource.
        /// </summary>
        [field: SerializeField]
        public float CurrentValue { get; private set; }

        /// <summary>
        /// The minimum value that this Resource can be.
        /// </summary>
        [field: SerializeField]
        public Stat MinValue { get; set; }

        /// <summary>
        /// The maximum value that this Resource can be.
        /// </summary>
        [field: SerializeField]
        public Stat MaxValue { get; set; }

        public Resource()
        {
            MinValue = new Stat(RESOURCE_MIN_VALUE);
            MaxValue = new Stat(RESOURCE_MAX_VALUE);
            CurrentValue = 0;
        }

        public Resource(float currentValue)
        {
            MinValue = new Stat(RESOURCE_MIN_VALUE);
            MaxValue = new Stat(RESOURCE_MAX_VALUE);
            CurrentValue = currentValue;
        }

        public Resource(float currentValue, float minValue, float maxValue)
        {
            MinValue = new Stat(minValue);
            MaxValue = new Stat(maxValue);
            CurrentValue = Mathf.Clamp(currentValue, MinValue.GetValue(), MaxValue.GetValue());
        }

        /// <summary>
        /// Adds/Subtracts a value to the current value of this Resource.
        /// </summary>
        /// <param name="value">The amount to be added/subtracted to this Resource.</param>
        public void Add(float value)
        {
            float possibleNewValue = CurrentValue + value;
            CurrentValue = Mathf.Clamp(possibleNewValue, MinValue.GetValue(), MaxValue.GetValue());
        }

        /// <summary>
        /// Multiplies/Divides a value to the current value of this Resource.
        /// </summary>
        /// <param name="value">The amount to be multiplied/divided to this Resource.</param>
        public void Multiply(float value)
        {
            float possibleNewValue = CurrentValue * value;
            CurrentValue = Mathf.Clamp(possibleNewValue, MinValue.GetValue(), MaxValue.GetValue());
        } 

        public override string ToString()
        {
            string minValue = MinValue.ToString();
            string maxValue = MaxValue.ToString();
            string currentValue = CurrentValue.ToString();

            return $"Resource:\nMin Value:{minValue}\nCurrent Value: {currentValue}\nMax Value: {maxValue}";
        }
    }
}
