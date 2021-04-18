using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlanGenerator
{
    public static ConditionNode GeneratePlan(Condition condition, IWorldState worldState, int depth, int width)
    {
        LinkedList<Condition> stack = new LinkedList<Condition>();
        stack.AddFirst(condition);
        ConditionNode topNode = FillTree(stack, worldState, depth, width);
        return topNode;
    }

    // ConditionNode(s) + ActionNode
    // Returns top node
    static ConditionNode FillTree(LinkedList<Condition> unsatisfied, IWorldState worldState, int depth, int width)
    {
        Debug.Log($"Filling Tree: depth: {depth}, unsatisfied: {unsatisfied.Count}");
        if (depth == 0 || unsatisfied.Count == 0)
        {
            return null;
        }

        // Get the next condition that we need to satisfy
        Condition nextCond = unsatisfied.First.Value;
        unsatisfied.RemoveFirst();
        ConditionNode nextCondNode = new ConditionNode(nextCond);
        // Top node will always point to the first pulled condition, it is the root node of the subtree
        ConditionNode topNode = nextCondNode;
        while (nextCond.Satisfied(worldState))
        {
            // All conditions are satisfied, return
            if (unsatisfied.Count == 0)
            {
                return topNode;
            }
            nextCond = unsatisfied.First.Value;
            unsatisfied.RemoveFirst();
            // Create next node and link
            ConditionNode tempNode = new ConditionNode(nextCond);
            tempNode.outNode = nextCondNode;
            nextCondNode.inNodes.Add(tempNode);
            nextCondNode = tempNode;
        }

        Debug.Log($"Condition of type {nextCond.GetType()}");

        List<Action> actions = nextCond.GenerateSatisfyingActions(worldState, width);
        Debug.Log($"Evaluating {actions.Count} possible actions");
        foreach (Action a in actions)
        {
            Debug.Log($"Action of type {a.GetType()} with {a.conditions.Count} conditions");
            // Pointer to the following node in sequence

            bool conflictingConds = false;

            foreach (Condition c in a.conditions)
            {
                if (c.Satisfied(worldState))
                {
                    continue;
                }
                Node tempOutNode = nextCondNode;
                // Check for conflicts with following conditions
                while (tempOutNode != null)
                {
                    if (tempOutNode is ConditionNode)
                    {
                        Condition futureCond = ((ConditionNode)tempOutNode).condition;
                        if (futureCond.Conflicts(c))
                        {
                            conflictingConds = true;
                            break;
                        }
                    }
                    tempOutNode = tempOutNode.outNode;
                }
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


            // Create a node for this action and link
            ActionNode aNode = new ActionNode(a);
            aNode.outNode = nextCondNode;
            nextCondNode.inNodes.Add(aNode);

            // Clone the unsatisfied conditions and add any new conditions
            LinkedList<Condition> newConds = new LinkedList<Condition>(unsatisfied);
            if (a.conditions != null)
            {
                foreach (Condition c in a.conditions)
                {
                    foreach (Condition existing in newConds)
                    {
                        if (c.CanMerge(existing)) {
                            c.Absorb(existing);
                            newConds.Remove(existing);
                        }
                    }
                    newConds.AddFirst(c);
                }
            }
            if (newConds.Count > 0)
            {
                ConditionNode following = FillTree(newConds, worldState, depth - 1, width);
                if (following != null)
                {
                    aNode.inNodes.Add(following);
                    following.outNode = aNode;
                }
            }
        }
        Debug.Log($"Returning {topNode.GetType()}, in: {topNode.inNodes.Count}, out: {topNode.outNode != null}");
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
