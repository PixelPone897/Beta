using Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Scripts.Constants;

namespace Assets.Scripts.Items
{

    [Serializable]
    public abstract class ItemInstanceComponent
    {
        public abstract void Initialize(ItemInstance owner);
    }

    [Serializable]
    public class KeyItem : ItemInstanceComponent
    {
        public override void Initialize(ItemInstance owner) { }
    }

    [Serializable]
    public class Condition : ItemInstanceComponent
    {
        [field: SerializeField]
        public Resource CurrentCondition { get; private set; }

        public Condition()
        {
            if(CurrentCondition == null)
            {
                CurrentCondition = new Resource(75, 0, 120);
            }
        }

        public override void Initialize(ItemInstance owner)
        {

        }

        public bool IsBroken => CurrentCondition.CurrentValue <= 0;

        public ItemConditionTier GetTier()
        {
            float percent = CurrentCondition.CurrentValue / CurrentCondition.MaxValue.GetValue();

            if(percent == 0)
            {
                return ItemConditionTier.BROKEN;
            }
            else if(percent <= 25)
            {
                return ItemConditionTier.POOR;
            }
            else if (percent <= 50)
            {
                return ItemConditionTier.HEAVILY_USED;
            }
            else if (percent <= 75)
            {
                return ItemConditionTier.USED;
            }
            else if (percent <= 100)
            {
                return ItemConditionTier.GOOD;
            }
            else
            {
                return ItemConditionTier.PERFECT;
            }
        }

        public void UpdateCondition(float amount)
        {
            CurrentCondition.Add(amount);
        }
    }

    [Serializable]
    public class ItemInstance
    {
        [field: SerializeField]
        public string UniqueId { get; private set; }

        // Should I do something similar copy variables over from SO to use as defaults like with Perks?
        public ItemData itemData;
        [field: SerializeReference, SubclassSelector]
        public List<ItemInstanceComponent> Components { get; private set; }

        public ItemInstance()
        {
            if (Components == null)
                Components = new List<ItemInstanceComponent>();
            UniqueId = string.Empty;
        }

        public ItemInstance(ItemData itemData)
        {
            Components = new List<ItemInstanceComponent>();
            this.itemData = itemData;

            if (!itemData.CanStack)
            {
                UniqueId = Guid.NewGuid().ToString();
            }
        }

        public void AddInstanceComponent(ItemInstanceComponent componentToAdd)
        {
            componentToAdd.Initialize(this);
            Components.Add(componentToAdd);
        }

        public T GetInstanceComponent<T>() where T : ItemInstanceComponent
        {
            return Components.FirstOrDefault(state => state.GetType() == typeof(T)) as T;
        }

        public void RemoveInstanceComponent<T>() where T : ItemInstanceComponent
        {
            Components.Remove(GetInstanceComponent<T>());
        }
    }
}