using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDice : MonoBehaviour
{
    public GameObject[] dice;
    public DiceSprite diceSprite;

    public void ShowFace(int i)
    {
        if(i-1 < dice.Length)
        {
            for(int j = 0; j < dice.Length; j++)
            {
                if((i-1)==j)
                {
                    dice[j].SetActive(true);
                }
                else
                {
                    dice[j].SetActive(false);
                }
            }
        }
            
    }

    public void OnMouseDrag()
    {
        AkSoundEngine.PostEvent("Play_Pickup", gameObject);

        diceSprite.OnDrag();
    }

    public void OnMouseUp()
    {
        AkSoundEngine.PostEvent("Play_Drop", gameObject);

        diceSprite.OnEndDrag();
    }

    public void OnDisable()
    {
        if(diceSprite != null)
        {
            diceSprite.continueDrag = true;
        }
    }
}
