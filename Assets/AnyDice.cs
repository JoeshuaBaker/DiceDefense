using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDice : MonoBehaviour
{
    public GameObject[] dice;
    public DiceSprite diceSprite;
    private bool isPickedUp = false;

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
        if (!isPickedUp)
        {
            AkSoundEngine.PostEvent("Play_Pickup", gameObject);
            isPickedUp = true;
        }

        diceSprite.OnDrag();
    }

    public void OnMouseUp()
    {
        AkSoundEngine.PostEvent("Play_Drop", gameObject);

        diceSprite.OnEndDrag();
        isPickedUp = false;
    }

    public void OnDisable()
    {
        if(diceSprite != null)
        {
            diceSprite.continueDrag = true;
        }
    }
}
