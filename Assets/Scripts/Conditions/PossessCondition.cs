using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessCondition : Condition
{
    struct BuyOption
    {
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

    public override List<Action> GenerateSatisfyingActions(IWorldState worldState, int maxActions)
    {
        List<BuyOption> options = new List<BuyOption>();
        IEntity owner = worldState.GetEntity(ownerId);
        foreach (Merchant m in worldState.GetMerchants())
        {
            int bestIndex = -1;
            for (int i = 0; i < m.saleEntries.Count; i++)
            {
                if (itemFilter.Satisfied(m.saleEntries[i].itemTemplate.Expected()))
                {
                    if (bestIndex == -1 || m.saleEntries[bestIndex].price < m.saleEntries[i].price)
                    {
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
        //Debug.Log($"Found {options.Count} options");

        options.Sort((x, y) => x.dist.CompareTo(y.dist));
        List<Action> actions = new List<Action>();
        for (int i = 0; i < Mathf.Min(maxActions, options.Count); i++)
        {
            BuyAction buyAction = new BuyAction(worldState, ownerId, options[i].saleEntryIndex, options[i].merchant.id);
            actions.Add(buyAction);
        }
        return actions;
    }
}