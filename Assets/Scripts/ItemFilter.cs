using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFilter : MonoBehaviour
{
    public float[] minPower = new float[Item.numItemSlots];
    public float minFoodAmount = 0;

    public bool Satisfied(Item item) {
        if (item.foodAmount < minFoodAmount) {
            return false;
        }
        bool satisfiesPower = false;
        for (int i = 0; i < Item.numItemSlots; i++) {
            if (item.equippable[i] && item.power[i] >= minPower[i]) {
                satisfiesPower = true;
                break;
            }
        }
        if (!satisfiesPower) {
            return false;
        }
        return true;
    }
}
