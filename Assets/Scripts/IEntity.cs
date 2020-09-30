using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    Vector2Int Position { get; set; }
    IReadOnlyCollection<Item> GetInventory();
    void AddInventoryItem(Item item);
    void RemoveInventoryItem(Item item);
    Weapon EquippedWeapon { get; set; }
    int Id { get; set; }
    float Money { get; set; }
}