using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAction : Action
{
    public int buyerId;
    public int saleEntryIndex;
    public int merchantId;

    public BuyAction(IWorldState worldState, int buyerId, int saleEntryIndex, int merchantId) {
        this.buyerId = buyerId;
        this.saleEntryIndex = saleEntryIndex;
        this.merchantId = merchantId;
        GenerateConditions(worldState);
    }

    public override float EstimateCost(IWorldState worldState)
    {
        Merchant merchant = worldState.GetMerchant(merchantId);
        float salePrice = merchant.saleEntries[saleEntryIndex].price;
        return salePrice;
    }

    public override ActionOutcome Execute(IWorldState worldState, float duration)
    {
        ExecuteImmediate(worldState);
        return ActionOutcome.Complete;
    }

    public override void ExecuteImmediate(IWorldState worldState) 
    {
        IEntity buyer = worldState.GetEntity(buyerId);
        Merchant merchant = worldState.GetMerchant(merchantId);
        float salePrice = merchant.saleEntries[saleEntryIndex].price;
        if (buyer.Money > salePrice)
        {
            Item item = merchant.saleEntries[saleEntryIndex].itemTemplate.Generate();
            buyer.AddInventoryItem(item);
            buyer.Money -= salePrice;
        }
    }

    protected override void GenerateConditions(IWorldState worldState)
    {
        Merchant merchant = worldState.GetMerchant(merchantId);
        conditions = new List<Condition>();
        MoneyCondition moneyCond = new MoneyCondition(buyerId, merchant.saleEntries[saleEntryIndex].price);
        conditions.Add(moneyCond);
        LocationCondition locationCond = new LocationCondition(buyerId, merchant.position);
        conditions.Add(locationCond);
        
    }

    public override string ToString()
    {
        return $"buyer:{buyerId}\nitem:{saleEntryIndex}\nmerchant:{merchantId}";
    }
}
