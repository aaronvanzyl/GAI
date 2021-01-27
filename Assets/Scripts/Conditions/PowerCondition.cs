using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCondition : Condition
{
    public int entityId;
    public float minPower;

    public PowerCondition(int entityId, float minPower)
    {
        this.entityId = entityId;
        this.minPower = minPower;
    }

    public override List<Action> GenerateSatisfyingActions(IWorldState worldState, int maxActions)
    {
        List<Action> actions = new List<Action>();

        IEntity entity = worldState.GetEntity(entityId);


        float powerDiff = minPower - entity.GetPower();
        if (powerDiff <= 0) {
            return actions;
        }

        ItemFilter filter = new ItemFilter();
        filter.requireEquippable = true;
        for (int i = 0; i < Item.numItemSlots; i++) {
            if (entity.TryGetEquippedItem((ItemSlot)i, out Item item))
            {
                filter.minPower[i] = powerDiff + item.power;
            }
            else {
                filter.minPower[i] = powerDiff;
            }
        }
        EquipAction equipAction = new EquipAction(worldState, entityId, filter);
        actions.Add(equipAction);
        return actions;
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
