using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour
{
    public NodeTreeRenderer treeRenderer;
    

    private void Start()
    {
        RealizedWorldState worldState = new RealizedWorldState();

        RealizedEntity entity = new RealizedEntity();
        entity.Money = 1000; 
        worldState.AddEntity(entity);

        Merchant merchant = new Merchant(new Vector2Int(20,30));
        Item apple = new Item("apple", foodAmount: 10);
        ItemTemplate appleTemplate = new ItemTemplate(apple);
        merchant.saleEntries.Add(new SaleEntry(appleTemplate, 6));
        worldState.AddMerchant(merchant);

        ItemFilter filter = new ItemFilter();
        filter.minFoodAmount = 1;
        Condition possessCond = new PossessCondition(entity.Id, filter);
        ConditionNode topNode = PlanGenerator.GeneratePlan(possessCond, worldState, 5, 5);

        treeRenderer.RenderTree(topNode);
    }
}
    