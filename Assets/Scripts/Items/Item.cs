using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemSlot { Weapon, Armor }
public class Item
{
    public static readonly int numItemSlots = 2;

    public string name = "";
    public float foodAmount = 0;
    public ItemSlot slot = 0;
    public bool equippable = false;
    public float power = 0;

    //public Item(string name, float foodAmount = 0, float power = 0, ItemSlot slot = 0, bool equippable = false) {
    //    this.name = name;
    //    this.foodAmount = foodAmount;
    //    this.power = power;
    //    this.slot = slot;
    //    this.equippable = equippable;
    //}

    //public Item(string name)
    //{
    //    this.name = name;
    //}

    public virtual Item Clone()
    {
        Item clone = new Item();
        clone.name = name;
        clone.foodAmount = foodAmount;
        clone.slot = slot;
        clone.equippable = equippable;
        clone.power = power;
        return clone;
    }
}
