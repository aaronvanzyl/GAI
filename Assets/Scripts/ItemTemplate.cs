using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTemplate
{
    Item item;

    public ItemTemplate(Item item) {
        this.item = item;
    }

    public Item Generate() {
        return item.Clone();
    }

    public Item Expected() {
        return item;
    }
}
