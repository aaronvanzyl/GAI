using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleEntry
{
    public ItemTemplate itemTemplate;
    public float price;

    public SaleEntry() { }

    public SaleEntry(ItemTemplate itemTemplate, float price)
    {
        this.itemTemplate = itemTemplate;
        this.price = price;
    }


    public SaleEntry Clone()
    {
        SaleEntry clone = new SaleEntry();
        clone.itemTemplate = itemTemplate;
        clone.price = price;
        return clone;
    }
}
