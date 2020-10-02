using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public IReadOnlyCollection<Item> GetInventory();
    public void AddInventoryItem(Item item);
    public void RemoveInventoryItem(Item item);

    public List<Item> Filter(ItemFilter filter) {
        if()
    }
}
