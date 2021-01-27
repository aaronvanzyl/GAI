using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class RealizedEntity : IEntity
{
    List<Item> inventory = new List<Item>();
    public Vector2Int Position { get; set; }
    public int Id { get; set; }
    public float Money { get; set; }
    Item[] equippedItems = new Item[Item.numItemSlots];

    public RealizedEntity() {
    }

    public RealizedEntity(Vector2Int position) {
        this.Position = position;
    }

    public ReadOnlyCollection<Item> GetInventory() {
        return inventory.AsReadOnly();
    }

    public void AddInventoryItem(Item item) {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
        }
    }

    public void RemoveInventoryItem(Item item) {
        UnequipItem(item);
        inventory.Remove(item);
    }

    public void EquipItem(Item item, ItemSlot slot, bool destroyExisting = false)
    {
        if (destroyExisting)
        {
            if (equippedItems[(int)slot] != null)
            {
                inventory.Remove(equippedItems[(int)slot]);
            }
        }
        equippedItems[(int)slot] = item;
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
        }
    }

    public bool TryGetEquippedItem(ItemSlot slot, out Item item)
    {
        if (equippedItems[(int)slot] != null)
        {
            item = equippedItems[(int)slot];
            return true;
        }
        item = null;
        return false;
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

    public float GetPower()
    {
        float power = 0;
        for (int i = 0; i < equippedItems.Length; i++)
        {
            if (equippedItems[i] != null)
            {
                power += equippedItems[i].power;
            }
        }
        return power;
    }

}
