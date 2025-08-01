using Assets.Scripts.Items;
using Scripts;
using Scripts.Actors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actors
{
    /// <summary>
    /// Represents the inventory of an Actor.
    /// </summary>
    [RequireComponent(typeof(ActorSpecialStats))]
    public class ActorInventory: MonoBehaviour
    {
        /// <summary>
        /// Reference to Actor's Special Stats.
        /// </summary>
        private ActorSpecialStats specialStats;

        private float numberOfBottleCaps;
        private Stat carryWeight;
        
        /// <summary>
        /// Inventory associated with an Actor.
        /// </summary>
        /// <remarks>
        /// The total number of Inventory Slots can vary as it is dependent
        /// on the maximum weight that an Actor can carry.
        /// </remarks>
        /// <seealso cref="InventorySlot"/>
        [field: SerializeField]
        private List<InventorySlot> inventorySlots;

        private void Awake()
        {
            inventorySlots ??= new List<InventorySlot>();

        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            specialStats = GetComponent<ActorSpecialStats>();

            numberOfBottleCaps = 0;
            int carryCalcuation = Mathf.FloorToInt(25 + 25 * specialStats.Strength.GetValue());
            carryWeight = new Stat(carryCalcuation);
            foreach(InventorySlot editorSlots in inventorySlots)
            {
                editorSlots.Item.Initialize();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Removes all Inventory Slots from Inventory
        /// </summary>
        public void ClearInventory()
        {
            inventorySlots.Clear();
        }

        // For non-stackable items, track using GUID. For stackable, use their name

        /// <summary>
        /// Adds item to inventory if possible.
        /// </summary>
        /// <remarks>Either an item gets added to an existing Inventory Slot if it is 
        /// stackable and one associated with it already exists, or a new one is created
        /// for it.</remarks>
        /// <param name="itemToAdd">The item to be added to the inventory.</param>
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

        /// <summary>
        /// Calculates and returns the total weight of all items in an Actor's inventory.
        /// </summary>
        /// <returns>The total weight of all items in an Actor's inventory.</returns>
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

    /// <summary>
    /// Represents a slot in an Actor's inventory.
    /// </summary>
    [System.Serializable]
    public class InventorySlot
    {
        /// <summary>
        /// Item being stored at this slot.
        /// </summary>
        [field: SerializeField]
        public ItemInstance Item { get; set; }

        /// <summary>
        /// Amount of this specific item stored at this slot.
        /// </summary>
        /// <remarks>For non-stackable Items, this value should be one.
        /// For stackable Items, it can be a variable amount.</remarks>
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
