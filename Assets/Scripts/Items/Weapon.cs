using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int damage;

    public Weapon(int type, int damage) {
        this.type = type;
        this.damage = damage;
    }

    override public Item Clone() {
        return new Weapon(type, damage);
    }
}
