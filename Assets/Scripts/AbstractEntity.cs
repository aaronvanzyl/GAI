using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEntity : IEntity
{
    IEntity parentEntity;
    public int Id { get; set; }
    public float Money { get; set; }
    public Vector2Int Position { get; set; }
    List<Item> inventory;

    public Weapon EquippedWeapon
    {
        get
        {
            return equippedWeapon;
        }
        set
        {
            equippedWeapon = value;
            AddInventoryItem(value);
        }
    }
    Weapon equippedWeapon;

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

    public IReadOnlyCollection<Item> GetInventory() {
        if (inventory != null)
        {
            return inventory;
        }
        else {
            return parentEntity.GetInventory();
        }
    }

    public void AddInventoryItem(Item item) {
        foreach (Item i in GetInventory()) {
            if (i == item) {
                return;
            }
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
}
