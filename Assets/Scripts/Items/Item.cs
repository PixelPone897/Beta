using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IItemStateComponent
{
    public abstract void Initialize(ItemInstance owner);
}

[Serializable]
public class AmmoState : IItemStateComponent
{
    public void Initialize(ItemInstance owner)
    {
        
    }
}

[Serializable]
public class ItemInstance
{
    public ItemData itemData;
    public int quantity;
    [field: SerializeReference, SubclassSelector]
    public List<IItemStateComponent> Components { get; private set; }

    public ItemInstance() 
    {
        if(Components == null)
            Components = new List<IItemStateComponent>();
        quantity = 1;
    }

    public ItemInstance(ItemData itemData, int quantity = 1)
    {
        Components = new List<IItemStateComponent>();
        this.itemData = itemData;
        this.quantity = quantity;
    }

    public void AddStateComponent<T>(T componentToAdd) where T : class, IItemStateComponent
    {
        componentToAdd.Initialize(this);
        Components.Add(componentToAdd);
        //components[typeof(T)] = componentToAdd;
    }

    public T GetStateComponent<T>() where T : class, IItemStateComponent
    {
        //components.TryGetValue(typeof(T), out var component);
        return Components.Find(state => state.GetType() == typeof(T)) as T;
        //return component as T;
    }

    public void RemoveStateComponent<T>() where T : class, IItemStateComponent
    {
        Components.Remove(GetStateComponent<T>());
        //components.Remove(typeof(T));
    }

}