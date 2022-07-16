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
        Artillery,
        Radial
    }

    public PatternType pattern;

}
