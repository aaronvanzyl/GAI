using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemSlot { Weapon, Armor }
public class Item
{
    public static readonly int numItemSlots = 2;

    public string name;
    public float foodAmount = 0;
    public bool[] equippable = new bool[numItemSlots];
    public float power = 0;

    public Item(string name, float foodAmount = 0, float power = 0) {
        this.name = name;
        this.foodAmount = foodAmount;
        this.power = power;
    }

    public virtual Item Clone() {
        Item clone = new Item(name);
        clone.foodAmount = foodAmount;
        for (int i = 0; i < numItemSlots; i++) {
            clone.equippable[i] = equippable[i];
            clone.power = power;
        }
        return clone;
    }
}
