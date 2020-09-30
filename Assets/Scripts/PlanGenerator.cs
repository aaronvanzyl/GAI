using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlanGenerator
{
    public static ConditionNode GeneratePlan (Condition condition, IWorldState worldState, int depth, int width) 
    { 
        Stack<Condition> stack = new Stack<Condition>();
        stack.Push(condition);
        ConditionNode topNode = FillTree(stack, worldState, depth, width);
        return topNode;
    }

    // ConditionNode(s) + ActionNode
    // Returns top node
    static ConditionNode FillTree(Stack<Condition> unsatisfied, IWorldState worldState, int depth, int width)
    {
        Debug.Log($"Filling Tree: depth: {depth}, unsatisfied: {unsatisfied.Count}");
        if (depth == 0 || unsatisfied.Count == 0)
        {
            return null;
        }

        Condition nextCond = unsatisfied.Pop();
        ConditionNode nextCondNode = new ConditionNode(nextCond);
        ConditionNode topNode = nextCondNode;
        while (nextCond.Satisfied(worldState))
        {
            if(unsatisfied.Count==0)
            {
                return topNode;
            }
            nextCond = unsatisfied.Pop();
            ConditionNode tempNode = new ConditionNode(nextCond);
            tempNode.outNode = nextCondNode;
            nextCondNode.inNodes.Add(tempNode);
            nextCondNode = tempNode;
        }

        Debug.Log($"Condition of type {nextCond.GetType()}");

        List<Action> actions = ActionGenerator.Satisfy(nextCond, worldState, width);
        Debug.Log($"Evaluating {actions.Count} possible actions");
        foreach (Action a in actions)
        {
            Debug.Log($"Action of type {a.GetType()} with {a.conditions.Count} conditions");
            Node tempOutNode = nextCondNode;
            bool conflictingConds = false;
            while (tempOutNode != null) {
                if (tempOutNode is ConditionNode) {
                    Condition futureCond = ((ConditionNode)tempOutNode).condition;
                    foreach (Condition c in a.conditions) {
                        if (futureCond.Conflicts(c)) {
                            conflictingConds = true;
                            break;
                        }
                    }
                }
                tempOutNode = tempOutNode.outNode;
                if (conflictingConds)
                {
                    break;
                }
            }
            if (conflictingConds)
            {
                Debug.Log($"Rejecting because of conflicting conditions");
                continue;
            }
            ActionNode aNode = new ActionNode(a);
            aNode.outNode = nextCondNode;
            nextCondNode.inNodes.Add(aNode);

            Stack<Condition> newConds = new Stack<Condition>(unsatisfied);
            if (a.conditions!=null)
            {
                //Stack<ConditionNode> newConds = new Stack<ConditionNode>(unsatisfied);
                //ConditionNode[] tempNodes = new ConditionNode[a.conditions.Count];
                //for (int i = 0; i < a.conditions.Count; i++)
                //{
                //    tempNodes[i] = new ConditionNode(a.conditions[i]);
                //}
                //for (int i = 0; i < a.conditions.Count; i++)
                //{
                //    newConds.Push(tempNodes[i]);
                //    if (i == a.conditions.Count - 1)
                //    {
                //        tempNodes[i].outNode = aNode;
                //        aNode.inNodes.Add(tempNodes[i]);
                //    }
                //    else
                //    {
                //        tempNodes[i].outNode = tempNodes[i + 1];
                //        tempNodes[i + 1].inNodes.Add(tempNodes[i]);
                //    }
                //}
                //FillTree(tempNodes[0], newConds, worldState, depth - 1, width);
                
                foreach (Condition c in a.conditions) {
                    newConds.Push(c);
                }
            }
            if (newConds.Count > 0)
            {
                ConditionNode following = FillTree(newConds, worldState, depth - 1, width);
                if (following != null) {
                    aNode.inNodes.Add(following);
                    following.outNode = aNode;
                }
            }
        }
        Debug.Log($"Returning {topNode.GetType()}, in: {topNode.inNodes.Count}, out: {topNode.outNode!=null}");
        return topNode;
    }

    //List<ActionChain> SatisfyCondition(Condition condition, AbstractWorldState worldState, int depth)
    //{
    //    if (depth == 0)
    //    {
    //        return null;
    //    }
    //    List<Action> actions = actionGenerator.Satisfy(condition, worldState);
    //    //List<List<Tuple<List<Action>, AbstractWorldState>>> actionChains = new List<List<Tuple<List<Action>, AbstractWorldState>>>();
    //    foreach (Action action in actions)
    //    {
    //        List<ActionChain> actionChains = new List<ActionChain>();
    //        ActionChain start = new ActionChain();
    //        start.endWorldState = worldState;

    //        foreach (Condition cond in action.conditions)
    //        {
    //            foreach (ActionChain chain in actionChains)
    //            {
    //                if (!cond.Satisfied(chain.endWorldState))
    //                {
    //                    List<ActionChain> options = SatisfyCondition(cond, chain.endWorldState, depth - 1);
    //                    if (options != null && options.Count > 0)
    //                    {
    //                        foreach (ActionChain option in options) {

    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return null;
    //}



    //public void GenerateTree(List<NodeAction> tree, AbstractWorldState worldState, Condition condition, int depth)
    //{
    //    List<Action> actions = actionGenerator.Satisfy(condition, worldState);

    //}
}
