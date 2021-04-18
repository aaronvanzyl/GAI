using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job
{
    public int id;

    [SerializeField]
    float baseYield;

    [SerializeField]
    float baseCost;

    public float GetYield(IEntity entity) {
        return baseYield;
    }

    public float GetCost(IEntity entity)
    {
        return baseCost;
    }

}
