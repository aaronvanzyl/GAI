using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFilter
{
    // The min power needed depending on which slot the item can be equipped
    // If the item is not equippable, this is ignored
    public float[] minPower = new float[Item.numItemSlots];
    public bool requireEquippable;
    public float minFoodAmount = 0;

    public bool Satisfied(Item item) {
        if (item.foodAmount < minFoodAmount) {
            return false;
        }
        if (requireEquippable && !item.equippable) {
            return false;
        }
        if (item.equippable && item.power < minPower[(int)item.slot]) {
            return false;
        }
        return true;
    }
}
