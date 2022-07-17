using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceText : MonoBehaviour
{
    public enum TextType
    {
        Base,
        Spire,
        Parapets
    }
    public DiceSprite sprite;
    public Tower towerPrefab;
    public TextType type;
    public int face = 0;
    private TextMeshPro tmp;
    private string originalText;

    // Start is called before the first frame update
    void Start()
    {
        face = 0;
        tmp = GetComponent<TextMeshPro>();
        originalText = tmp.text;
        ShowText(face);
        sprite.ShowFace(face);
    }

    public void ShowText(int face)
    {
        Debug.Log(name + " text showing face " + face);

        if(face > 0 && face <= 6)
        {
            sprite.ShowFace(face);
        }
        string text = originalText.Clone() as string;
        Debug.Log(text);
        switch (type)
        {
            case TextType.Base:
                if(face <= 0 || face > 6)
                {
                    text = "The Tower Base\n" +
                        "controls the\n" +
                        "projectile type\n" +
                        "and base stats\n" +
                        "of the tower.";
                    break;
                }

                Projectile projectile = towerPrefab.projectiles[face - 1];

                text = text.Replace("{0}", projectile.projectileType.ToString());
                text = text.Replace("{1}", projectile.damage.ToString("n2"));
                text = text.Replace("{2}", projectile.range.ToString("n2"));
                text = text.Replace("{3}", (projectile.fireRate < 1f ? (1/projectile.fireRate).ToString("n2") : projectile.fireRate.ToString("n2")) + "/s");
                text = text.Replace("{4}", projectile.flySpeed.ToString("n2"));
                text = text.Replace("{5}", projectile.numEnemiesPierce > 0 ? "Yes" : "No");
                break;

            case TextType.Spire:
                if (face <= 0 || face > 6)
                {
                    text = "The tower spire\n" +
                        "modifies range\n" +
                        "and attack speed\n" +
                        "inversely from\n" +
                        "one another.";
                    break;
                }
                text = text.Replace("{0}", "x" + AttackArea.GetMultipliers(face).Item1.ToString("n2"));
                text = text.Replace("{1}", "x" + AttackArea.GetMultipliers(face).Item2.ToString("n2"));
                break;

            case TextType.Parapets:
                if(face <= 0 || face > 6)
                {
                    text = "The Parapets\n" +
                        "modify the attack\n" +
                        "pattern and base\n" +
                        "damage of the\n" +
                        "tower.";
                        break;
                }
                ShotPattern pattern = towerPrefab.patterns[face - 1];

                text = text.Replace("{0}", pattern.patternType.ToString());
                text = text.Replace("{1}", "x" + pattern.damageMultiplier.ToString("n2"));
                break;
        }

        this.face = face;
        tmp.text = text;
    }
}
