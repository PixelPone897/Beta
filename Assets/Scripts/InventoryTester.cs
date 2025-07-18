using Assets.Scripts.Items;
using Scripts;
using Scripts.Actors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTester : MonoBehaviour
{
    public ActorInventory inventory;
    public InventoryListUI ui;
    public ItemInfo dummyItem;
    public ItemInfo uniqueItem;

    public Button addItemButton;
    public Button addUniqueItemButton;
    public TMP_InputField inputField;
    public Button removeUniqueItemButton;
    public Button removeItemButton;
    public Button clearInventoryButton;

    void Start()
    {
        addItemButton.onClick.AddListener(AddTestItem);
        addUniqueItemButton.onClick.AddListener(AddUniqueTestItem);
        removeItemButton.onClick.AddListener(RemoveLastItem);
        removeUniqueItemButton.onClick.AddListener(RemoveUniqueItem);
        clearInventoryButton.onClick.AddListener(ClearInventory);
    }

    void AddTestItem()
    {
        inventory.AddItem(dummyItem);
        ui.RefreshList();
    }

    void AddUniqueTestItem()
    {
        ItemInfo testing = Instantiate(uniqueItem);
        inventory.AddItem(testing);
        ui.RefreshList();
    }

    void RemoveLastItem()
    {
        if(inventory.InventorySlots.Count > 0)
            inventory.InventorySlots.RemoveAt(inventory.InventorySlots.Count - 1);

        ui.RefreshList();
    }

    void RemoveUniqueItem()
    {
        if(inventory.InventorySlots.Count > 0)
            inventory.RemoveItem(inputField.text);

        ui.RefreshList();
    }

    void ClearInventory()
    {
        inventory.ClearInventory();
        ui.RefreshList();
    }
}
