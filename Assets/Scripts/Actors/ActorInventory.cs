using Assets.Scripts.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Scripts.Actors
{
    [RequireComponent(typeof(ActorSpecialStats))]
    public class ActorInventory : MonoBehaviour
    {
        private ActorSpecialStats specialStats;

        private int numberOfBottleCaps;
        private Stat carryWeight;
        [field: SerializeField]
        public List<InventorySlot> InventorySlots { get; private set; }

        public void Awake()
        {
            //InventorySlots = new List<InventorySlot>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            specialStats = GetComponent<ActorSpecialStats>();

            numberOfBottleCaps = 0;
            int carryCalcuation = Mathf.FloorToInt(25 + 25 * specialStats.Strength.GetValue());
            carryWeight = new Stat(carryCalcuation);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ClearInventory()
        {
            InventorySlots.Clear();
        }

        // For non-stackable items, track using GUID. For stackable, use their name

        public void AddItem(ItemInfo itemToAdd)
        {
            float inventoryWeight = GetTotalWeight();
            if (inventoryWeight + itemToAdd.Weight > carryWeight.GetValue()) return;

            if(!itemToAdd.CanStack)
            {
                InventorySlots.Add(new InventorySlot(itemToAdd));
            }
            else
            {
                InventorySlot slot = InventorySlots.FirstOrDefault(slot => slot.Item.Name == itemToAdd.Name);
                if (slot != null)
                {
                    slot.Amount++;
                }
                else
                {
                    InventorySlots.Add(new InventorySlot(itemToAdd));
                }
            }
        }

        public void RemoveItem(string id)
        {
            InventorySlots.RemoveAll(slot => slot.Item.UniqueID == id);
        }

        public void RemoveItem(ItemInfo itemToRemove, int quantity = 1)
        {
            InventorySlot slot = InventorySlots.Find(slot => slot.Item.Name ==  itemToRemove.Name);
            if(slot == null) return;

            slot.Amount -= quantity;
            if(slot.Amount <= 0)
            {
                InventorySlots.Remove(slot);
            }
        }

        public float GetTotalWeight()
        {
            float totalWeight = 0;
            foreach(var inventorySlot in InventorySlots)
            {
                ItemInfo basicInfo = inventorySlot.Item.GetComponent<ItemInfo>();
                float slotWeight = basicInfo.Weight * inventorySlot.Amount;
                totalWeight += slotWeight;
            }
            return totalWeight;
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        [field: SerializeField]
        public ItemInfo Item { get; set; }
        [field: SerializeField]
        public int Amount { get; set; }

        public InventorySlot() { }

        public InventorySlot(ItemInfo item)
        {
            Item = item;
            Amount = 1;
        }
    }
}