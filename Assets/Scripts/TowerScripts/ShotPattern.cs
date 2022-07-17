using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShotPattern : MonoBehaviour
{
    public enum PatternType
    {
        Single,
        FrontBack,
        Tri,
        Aimed,
        Random,
        Radial
    }

    public PatternType patternType;
    public float damageMultiplier = 1.0f;
    public float rangeMultiplier = 1.0f;
    public float fireRateMultiplier = 1.0f;

    public abstract bool Shoot(Projectile projectile);
}
