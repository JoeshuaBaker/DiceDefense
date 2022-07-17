using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea
{
    public enum AreaType
    {
        Fastest,
        Faster,
        Fast,
        Range,
        Ranger,
        Rangest
    }

    public static (float, float)[] multipliers = new (float, float)[]
    {
        (.5f, 2.0f),
        (.75f, 1.25f),
        (.95f, 1.05f),
        (1.25f, .9f),
        (1.75f, .8f),
        (2.5f, .65f)
    };
    public AreaType areaType = AreaType.Fast;
    public float rangeModifier = 1.0f;
    public float fireRateMultiplier = 1.0f;

    public void Init(AreaType type)
    {
        rangeModifier = multipliers[(int)(type)].Item1;
        fireRateMultiplier = multipliers[(int)(type)].Item2;
        areaType = type;
    }

    public static (float, float) GetMultipliers(int face)
    {
        return multipliers[face-1];
    }
}
