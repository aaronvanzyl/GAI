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
        return owner.EquippedWeapon != null;
    }

    //public override string ToString()
    //{
    //    return $"entity:{ownerId}\nitem:{itemType}";
    //}
}
