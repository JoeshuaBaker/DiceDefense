using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea
{
    public float rangeModifier = 1.0f;
    public float attackSpeedModifier = 1.0f;

    public void Init(int number)
    {
        switch(number)
        {
            case 1:
                rangeModifier = .25f;
                attackSpeedModifier = 2.0f;
                break;
            case 2:
                rangeModifier = .66f;
                attackSpeedModifier = 1.25f;
                break;

            case 3:
                rangeModifier = .9f;
                attackSpeedModifier = 1.05f;
                break;

            case 4:
                rangeModifier = 1.25f;
                attackSpeedModifier = .9f;
                break;

            case 5:
                rangeModifier = 1.75f;
                attackSpeedModifier = .8f;
                break;

            case 6:
                rangeModifier = 2.5f;
                attackSpeedModifier = .6f;
                break;

            default:
                break;
        }
    }
}
