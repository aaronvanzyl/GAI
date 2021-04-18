using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCondition : Condition
{
    int entityId;
    float money;

    public MoneyCondition(int entityId, float money) 
    {
        this.entityId = entityId;
        this.money = money;
    }

    public override bool Satisfied(IWorldState worldState)
    {
        IEntity entity = worldState.GetEntity(entityId);
        return entity.Money > money;
    }

    public override string ToString()
    {
        return $"entity:{entityId}\nmoney:{money}";
    }

    public override List<Action> GenerateSatisfyingActions(IWorldState worldState, int maxActions)
    {
        return new List<Action>();
    }

    public override bool CanMerge(Condition other)
    {
        if(other is MoneyCondition cond) {
            if (cond.entityId == entityId) {
                return true;
            }
        }
        return false;
    }

    public override void Absorb(Condition other)
    {
        money += (other as MoneyCondition).money;
    }

    public override bool Conflicts(Condition other)
    {
        if (other is MoneyCondition cond)
        {
            if (cond.entityId == entityId)
            {
                return true;
            }
        }
        return false;
    }
}
