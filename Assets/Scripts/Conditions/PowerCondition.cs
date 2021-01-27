using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCondition : Condition
{
    public int entityId;
    public float minPower;

    public PowerCondition(int entityId, float power)
    {
        this.entityId = entityId;
    }

    override public bool Satisfied(IWorldState worldState)
    {
        IEntity owner = worldState.GetEntity(entityId);
        return owner.GetPower() >= minPower;
    }

    //public override string ToString()
    //{
    //    return $"entity:{ownerId}\nitem:{itemType}";
    //}
}
