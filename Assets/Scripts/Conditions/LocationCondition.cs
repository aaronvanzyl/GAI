using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCondition : Condition
{
    public int entityID;
    public Vector2Int position;

    public LocationCondition(int entityID, Vector2Int position)
    {
        this.entityID = entityID;
        this.position = position;
    }

    public override bool Satisfied(IWorldState worldState)
    {
        IEntity entity = worldState.GetEntity(entityID);
        return entity.Position == position;
    }

    public override string ToString()
    {
        return $"entity:{entityID}\npos:{position}";
    }

    public override List<Action> GenerateSatisfyingActions(IWorldState worldState, int maxActions)
    {
        List<Action> actions = new List<Action>();
        MoveAction moveAction = new MoveAction(worldState, entityID, position);
        actions.Add(moveAction);
        return actions;
    }
}
