using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    List<Item> items = new List<Item>();

    public IReadOnlyCollection<Item> GetItems() {
        return items.AsReadOnly();
    }

    public void AddItem(Item item) {
        items.Add(item);
    }

    public void RemoveItem(Item item) {
        items.Remove(item);
    }

    public List<Item> Filter(ItemFilter filter) {
        List<Item> filtered = new List<Item>();
        foreach (Item i in items) {
            if (filter.Satisfied(i)) {
                filtered.Add(i);
            }
        }
        return filtered;
    }
}
