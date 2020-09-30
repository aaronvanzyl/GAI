using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealizedEntity : IEntity
{
    List<Item> inventory = new List<Item>();
    public RealizedEntity() {
    }

    public RealizedEntity(Vector2Int position) {
        this.Position = position;
    }

    public Vector2Int Position { get; set; }

    public IReadOnlyCollection<Item> GetInventory() {
        return inventory.AsReadOnly();
    }

    public void AddInventoryItem(Item item) {
        inventory.Add(item);
    }

    public void RemoveInventoryItem(Item item) {
        if (item == EquipedWeapon) {

        }
        inventory.Remove(item);
    }

    public void OnEquip() { }

    public int Id { get; set; }
    public float Money { get; set; }
    public Weapon EquippedWeapon {
        get;
        set {
            if()
             }; }
    Weapon equipedWeapon;

}
