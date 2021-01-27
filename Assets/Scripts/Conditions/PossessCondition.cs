using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessCondition : Condition
{
    public int ownerId;
    public ItemFilter itemFilter;

    public PossessCondition(int ownerId, ItemFilter itemFilter)
    {
        this.ownerId = ownerId;
        this.itemFilter = itemFilter;
    }

    override public bool Satisfied(IWorldState worldState) {
        IEntity owner = worldState.GetEntity(ownerId);
        foreach (Item i in owner.GetInventory()) {
            if (itemFilter.Satisfied(i)) {
                return true;
            }
        }
        return false;
    }

    public override string ToString()
    {
        return $"entity:{ownerId}\nitem:{itemFilter}";
    }
}