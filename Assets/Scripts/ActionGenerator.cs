using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionGenerator
{
    struct BuyOption {
        public Merchant merchant;
        public float dist;
        public int saleEntryIndex;

        public BuyOption(Merchant merchant, float dist, int saleEntryIndex)
        {
            this.merchant = merchant;
            this.dist = dist;
            this.saleEntryIndex = saleEntryIndex;
        }
    }

    public static List<Action> Satisfy(Condition condition, IWorldState worldState, int maxActions) {
        switch (condition) {
            case PossessCondition possess:
                return Satisfy(possess, worldState, maxActions);
            case LocationCondition location:
                return Satisfy(location, worldState, maxActions);
            default:
                Debug.LogWarning($"Action generator received unknown condition: {condition}");
                return new List<Action>();
        }
    }

    public static List<Action> Satisfy(PossessCondition condition, IWorldState worldState, int maxActions) {
        Debug.Log("Satisfying possessCond");

        List<BuyOption> options = new List<BuyOption>();
        IEntity owner = worldState.GetEntity(condition.ownerId);
        foreach (Merchant m in worldState.GetMerchants()) {
            int bestIndex = -1;
            for (int i = 0; i < m.saleEntries.Count; i++) {
                if (condition.itemFilter.Satisfied(m.saleEntries[i].itemTemplate.Expected()))
                {
                    if (bestIndex == -1 || m.saleEntries[bestIndex].price < m.saleEntries[i].price) {
                        bestIndex = i;
                    }
                }
            }
            if (bestIndex != -1)
            {
                float dist = PathFinder.EstimateDistance(owner.Position, m.position);
                options.Add(new BuyOption(m, dist, bestIndex));
            }
        }
        Debug.Log($"Found {options.Count} options");

        options.Sort((x, y) => x.dist.CompareTo(y.dist));
        List<Action> actions = new List<Action>();
        for (int i = 0; i < Mathf.Min(maxActions, options.Count); i++) {
            BuyAction buyAction = new BuyAction(worldState, condition.ownerId, options[i].saleEntryIndex, options[i].merchant.id);
            actions.Add(buyAction);
        }
        return actions;
    }

    public static List<Action> Satisfy(LocationCondition condition, IWorldState worldState, int maxActions)
    {
        List<Action> actions = new List<Action>(); 
        MoveAction moveAction = new MoveAction(worldState, condition.entityID, condition.position);
        actions.Add(moveAction);
        return actions;
    }
}
