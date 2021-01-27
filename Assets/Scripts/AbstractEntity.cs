using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class AbstractEntity : IEntity
{
    IEntity parentEntity;
    public int Id { get; set; }
    public float Money { get; set; }
    public Vector2Int Position { get; set; }
    List<Item> inventory;
    Item[] equippedItems; 

    public AbstractEntity(IEntity parentEntity)
    {
        this.parentEntity = parentEntity;
        Position = parentEntity.Position;
        Id = parentEntity.Id;
        Money = parentEntity.Money;
    }

    public AbstractEntity GetAbstract()
    {
        AbstractEntity clone = new AbstractEntity(this);
        return clone;
    }

    public ReadOnlyCollection<Item> GetInventory() {
        if (inventory != null)
        {
            return inventory.AsReadOnly();
        }
        else {
            return parentEntity.GetInventory();
        }
    }

    public void AddInventoryItem(Item item) {
        if (GetInventory().Contains(item)) {
            return;
        }

        if (inventory == null) {
            CopyInventory();
        }
        inventory.Add(item);
    }

    public void RemoveInventoryItem(Item item) {
        if (inventory == null)
        {
            CopyInventory();
        }
        inventory.Remove(item);
    }

    void CopyInventory() {
        inventory = new List<Item>();
        IReadOnlyCollection<Item> parentInventory = parentEntity.GetInventory();
        foreach (Item i in parentInventory)
        {
            inventory.Add(i.Clone());
        }
    }

    public void EquipItem(Item item, ItemSlot slot, bool destroyExisting = false)
    {
        if (destroyExisting)
        {
            if (equippedItems[(int)slot] != null)
            {
                RemoveInventoryItem(equippedItems[(int)slot]);
            }
        }
        equippedItems[(int)slot] = item;
        AddInventoryItem(item);
    }

    public void UnequipSlot(ItemSlot slot)
    {
        equippedItems[(int)slot] = null;
    }

    public void UnequipItem(Item item)
    {
        for (int i = 0; i < Item.numItemSlots; i++)
        {
            if (equippedItems[i].Equals(item))
            {
                equippedItems[i] = null;
            }
        }
    }

    public bool TryGetEquippedItem(ItemSlot slot, out Item item)
    {
        if (equippedItems[(int)slot] != null) {
            item = equippedItems[(int)slot];
            return true;
        }
        item = null;
        return false;
    }

    public float GetPower() {
        float power = 0;
        for (int i = 0; i < equippedItems.Length; i++) {
            if (equippedItems[i] != null) {
                power += equippedItems[i].power;
            }
        }
        return power;
    }
}
