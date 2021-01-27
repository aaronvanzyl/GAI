using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Merchant
{
    public int id;
    public Vector2Int position;
    public List<SaleEntry> saleEntries;

    public Merchant(Vector2Int position)
    {
        this.position = position;
        saleEntries = new List<SaleEntry>();
    }

    public Merchant(Vector2Int position, List<SaleEntry> saleEntries) {
        this.position = position;
        this.saleEntries = saleEntries;
    }

    public Merchant Clone() {
        Merchant clone = new Merchant(position, new List<SaleEntry>());
        foreach (SaleEntry entry in saleEntries) {
            clone.saleEntries.Add(entry.Clone());
        }
        clone.id = id;
        return clone;
    }
}
