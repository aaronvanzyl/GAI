using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAction : Action
{
    public int entityId;
    public ItemFilter itemFilter;


    public EquipAction(IWorldState worldState, int entityId, ItemFilter itemFilter)
    {
        this.entityId = entityId;
        this.itemFilter = itemFilter;
        GenerateConditions(worldState);
    }

    public override float EstimateCost(IWorldState worldState)
    {
        return 0;
    }

    public override ActionOutcome Execute(IWorldState worldState, float duration)
    {
        ExecuteImmediate(worldState);
        return ActionOutcome.Complete;
    }

    public override void ExecuteImmediate(IWorldState worldState)
    {
        IEntity entity = worldState.GetEntity(entityId);
        foreach (Item item in entity.GetInventory())
        {
            if (itemFilter.Satisfied(item) && item.equippable)
            {
                entity.EquipItem(item, item.slot);
                return;
            }
        }
    }

    protected override void GenerateConditions(IWorldState worldState)
    {
        conditions = new List<Condition>();
        PossessCondition possessCond = new PossessCondition(entityId, itemFilter);
        conditions.Add(possessCond);
    }
}
