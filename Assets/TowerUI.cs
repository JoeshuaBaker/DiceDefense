using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUI : MonoBehaviour
{
    public static TowerUI instance;

    public Canvas canvas;
    public DiceText baseText;
    public DiceText spiresText;
    public DiceText parapetsText;
    public Dicebag dicebag;
    public Button skipButton;
    public Image hpBar;
    public TextMeshPro hpText;
    public Image diceBar;
    public TextMeshPro diceText;
    public int hp = 100;
    public int maxHp = 100;
    public int diceScore = 0;
    public int diceMax = 25;

    private void Start()
    {
        if(instance != null)
        {
            Debug.LogWarning("There are two TowerUI objects.");
        }

        instance = this;
    }

    private void Update()
    {
        hpText.text = hp + " / " + maxHp;
        hpBar.fillAmount = (float)hp / (float)maxHp;

        diceText.text = diceScore + " / " + diceMax;
        diceBar.fillAmount = (float)diceScore / (float)diceMax;
    }

    public void ShowText(int bottom = -1, int spire = -1, int parapet = -1)
    {
        baseText.ShowText(bottom);
        spiresText.ShowText(spire);
        parapetsText.ShowText(parapet);
    }

    public void DealDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    public bool GetDiceScore(int hp)
    {
        diceScore += hp;
        if (diceScore > diceMax)
        {
            dicebag.SpawnDice();
            diceScore -= diceMax;
            diceMax = (int)(diceMax * 1.1);
            return true;
        }
        else return false;
    }

    public void Die()
    {

    }
}
