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

        // Create Merchant
        Merchant merchant = new Merchant(new Vector2Int(20, 30));

        Item apple = new Item() { 
            name = "apple", 
            foodAmount = 10 
        };
        ItemTemplate appleTemplate = new ItemTemplate(apple);
        merchant.saleEntries.Add(new SaleEntry(appleTemplate, 6));

        Item sword = new Item() {
            name = "sword",
            equippable = true,
            slot = ItemSlot.Weapon,
            power = 10
        };
        ItemTemplate swordTemplate = new ItemTemplate(sword);
        merchant.saleEntries.Add(new SaleEntry(swordTemplate, 25));

        worldState.AddMerchant(merchant);

        // Create condition
        //ItemFilter filter = new ItemFilter();
        //filter.minFoodAmount = 1;
        //Condition possessCond = new PossessCondition(entity.Id, filter);
        Condition powerCond = new PowerCondition(entity.Id, 10);
        ConditionNode topNode = PlanGenerator.GeneratePlan(powerCond, worldState, 5, 5);

        treeRenderer.RenderTree(topNode);
    }
}
