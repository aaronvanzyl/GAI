﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public abstract bool Satisfied(IWorldState worldState);
    public abstract List<Action> GenerateSatisfyingActions(IWorldState worldState, int maxActions);

    public virtual void Absorb(Condition other)
    {

    }

    public virtual bool CanMerge(Condition other)
    {
        return false;
    }

    public virtual bool Conflicts(Condition other)
    {
        return false;
    }
}
