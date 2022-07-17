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
    public AreaType areaType = AreaType.Fast;
    public float rangeModifier = 1.0f;
    public float fireRateMultiplier = 1.0f;

    public void Init(AreaType type)
    {
        switch((int)type + 1)
        {
            case 1:
                rangeModifier = .25f;
                fireRateMultiplier = 2.0f;
                break;
            case 2:
                rangeModifier = .66f;
                fireRateMultiplier = 1.25f;
                break;

            case 3:
                rangeModifier = .9f;
                fireRateMultiplier = 1.05f;
                break;

            case 4:
                rangeModifier = 1.25f;
                fireRateMultiplier = .9f;
                break;

            case 5:
                rangeModifier = 1.75f;
                fireRateMultiplier = .8f;
                break;

            case 6:
                rangeModifier = 2.5f;
                fireRateMultiplier = .6f;
                break;

            default:
                return;
        }
        areaType = type;
    }
}
