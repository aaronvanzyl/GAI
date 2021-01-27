using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public interface IEntity
{
    Vector2Int Position { get; set; }
    ReadOnlyCollection<Item> GetInventory();
    void AddInventoryItem(Item item);
    void RemoveInventoryItem(Item item);
    void EquipItem(Item item, ItemSlot slot, bool destroyExisting = false);
    void UnequipItem(Item item);
    void UnequipSlot(ItemSlot slot);
    bool TryGetEquippedItem(ItemSlot slot, out Item item);
    int Id { get; set; }
    float Money { get; set; }
    float GetPower();
}