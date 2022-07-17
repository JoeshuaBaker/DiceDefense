using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public static TowerUI instance;

    public Canvas canvas;
    public DiceText baseText;
    public DiceText spiresText;
    public DiceText parapetsText;

    private void Start()
    {
        if(instance != null)
        {
            Debug.LogWarning("There are two TowerUI objects.");
        }

        instance = this;
    }

    public void ShowText(int bottom = -1, int spire = -1, int parapet = -1)
    {
        baseText.ShowText(bottom);
        spiresText.ShowText(spire);
        parapetsText.ShowText(parapet);
    }
}
