using Scripts.Actors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class InventoryListUI : MonoBehaviour
    {
        public GameObject itemSlotPrefab;
        public Transform contentPanel;
        public ActorInventory inventoryRef;

        void Start()
        {
            RefreshList();
        }

        public void RefreshList()
        {
            foreach (Transform child in contentPanel)
                Destroy(child.gameObject);

            foreach (var slot in inventoryRef.InventorySlots)
            {
                
                GameObject uiSlot = Instantiate(itemSlotPrefab, contentPanel);
                uiSlot.transform.Find("Icon").GetComponent<Image>().sprite = slot.Item.Icon;
                uiSlot.transform.Find("Name").GetComponent<TMP_Text>().text = slot.Item.Name;
                uiSlot.transform.Find("Quantity").GetComponent<TMP_Text>().text = "x" + slot.Amount.ToString();
            }
        }
    }
}
