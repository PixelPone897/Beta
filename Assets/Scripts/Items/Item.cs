using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ItemInstanceComponent
{
    public abstract void Initialize(ItemInstance owner);
}

[Serializable]
public class AmmoState : ItemInstanceComponent
{
    [SerializeField]
    private int amountOfAmmo;

    public override void Initialize(ItemInstance owner)
    {
        
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
        if(Components == null)
            Components = new List<ItemInstanceComponent>();
        UniqueId = string.Empty;
    }

    public ItemInstance(ItemData itemData)
    {
        Components = new List<ItemInstanceComponent>();
        this.itemData = itemData;
        
        if(!itemData.CanStack)
        {
            UniqueId = Guid.NewGuid().ToString();
        }
    }

    public void AddStateComponent<T>(T componentToAdd) where T : ItemInstanceComponent
    {
        componentToAdd.Initialize(this);
        Components.Add(componentToAdd);
        //components[typeof(T)] = componentToAdd;
    }

    public T GetStateComponent<T>() where T : ItemInstanceComponent
    {
        //components.TryGetValue(typeof(T), out var component);
        return Components.Find(state => state.GetType() == typeof(T)) as T;
        //return component as T;
    }

    public void RemoveStateComponent<T>() where T : ItemInstanceComponent
    {
        Components.Remove(GetStateComponent<T>());
        //components.Remove(typeof(T));
    }

}