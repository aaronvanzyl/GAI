using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealizedEntity : IEntity
{
    List<Item> inventory = new List<Item>();
    public Vector2Int Position { get; set; }
    public int Id { get; set; }
    public float Money { get; set; }

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

    public RealizedEntity() {
    }

    public RealizedEntity(Vector2Int position) {
        this.Position = position;
    }

    public IReadOnlyCollection<Item> GetInventory() {
        return inventory.AsReadOnly();
    }

    public void AddInventoryItem(Item item) {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
        }
    }

    public void RemoveInventoryItem(Item item) {
        if (item == equippedWeapon) {
            equippedWeapon = null;
        }
        inventory.Remove(item);
    }

    

}
