using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCondition : Condition
{
    public int entityId;

    public WeaponCondition(int entityId)
    {
        this.entityId = entityId;
    }

    override public bool Satisfied(IWorldState worldState)
    {
        IEntity owner = worldState.GetEntity(entityId);
        foreach (Item i in owner.Inventory)
        {
            if (i.type == itemType)
            {
                return true;
            }
        }
        return false;
    }

    public override string ToString()
    {
        return $"entity:{ownerId}\nitem:{itemType}";
    }
}
