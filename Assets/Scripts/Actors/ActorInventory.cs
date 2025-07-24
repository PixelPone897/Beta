using Assets.Scripts.Items;
using Scripts;
using Scripts.Actors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actors
{
    [RequireComponent(typeof(ActorSpecialStats))]
    public class ActorInventory: MonoBehaviour
    {
        private ActorSpecialStats specialStats;

        private int numberOfBottleCaps;
        private Stat carryWeight;
        [field: SerializeField]
        private List<InventorySlot> inventorySlots;

        public RangeWeaponData weaponData;
        private void Awake()
        {
            if(inventorySlots == null)
                inventorySlots = new List<InventorySlot>();

        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            specialStats = GetComponent<ActorSpecialStats>();

            numberOfBottleCaps = 0;
            int carryCalcuation = Mathf.FloorToInt(25 + 25 * specialStats.Strength.GetValue());
            carryWeight = new Stat(carryCalcuation);

            ItemInstance testing = new ItemInstance(weaponData);
            testing.AddInstanceComponent(new Ammo(testing));
            AddItem(testing);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ClearInventory()
        {
            inventorySlots.Clear();
        }

        // For non-stackable items, track using GUID. For stackable, use their name

        public void AddItem(ItemInstance itemToAdd)
        {
            float inventoryWeight = GetTotalWeight();
            if (inventoryWeight + itemToAdd.itemData.Weight > carryWeight.GetValue()) return;

            if (!itemToAdd.itemData.CanStack)
            {
                inventorySlots.Add(new InventorySlot(itemToAdd));
            }
            else
            {
                InventorySlot slot = inventorySlots.FirstOrDefault(slot => 
                slot.Item.itemData.Name == itemToAdd.itemData.Name);
                if (slot != null)
                {
                    slot.Amount++;
                }
                else
                {
                    inventorySlots.Add(new InventorySlot(itemToAdd));
                }
            }
        }

        public void RemoveItem(string id)
        {
            inventorySlots.RemoveAll(slot => slot.Item.UniqueId == id);
        }

        public void RemoveItem(ItemInstance itemToRemove, int quantity = 1)
        {
            InventorySlot slot = inventorySlots.Find(slot => 
            slot.Item.itemData.Name == itemToRemove.itemData.Name);
            if (slot == null) return;

            if(slot.Item.GetInstanceComponent<KeyItem>() != null)
            {
                return;
            }

            slot.Amount -= quantity;
            if (slot.Amount <= 0)
            {
                inventorySlots.Remove(slot);
            }
        }

        public float GetTotalWeight()
        {
            float totalWeight = 0;
            foreach (var inventorySlot in inventorySlots)
            {
                ItemInstance item = inventorySlot.Item;
                float slotWeight = item.itemData.Weight * inventorySlot.Amount;
                totalWeight += slotWeight;
            }
            return totalWeight;
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        [field: SerializeField]
        public ItemInstance Item { get; set; }
        [field: SerializeField]
        public int Amount { get; set; }

        public InventorySlot() { }

        public InventorySlot(ItemInstance item)
        {
            Item = item;
            Amount = 1;
        }
    }
}
