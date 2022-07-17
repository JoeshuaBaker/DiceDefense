using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dicebag : MonoBehaviour
{
    (float, float)[] points = new (float, float)[]
    {
        (0f, 0f), (1.5f, 0f), (3f, 0f),
        (0f, 1.5f), (1.5f, 1.5f), (3f, 1.5f),
        (0f, 3f), (1.5f, 3f), (3f, 3f)
    };

    public int index = 0;
    public GameObject diceSprite;

    private void Start()
    {
        SpawnDice();
        SpawnDice();
        SpawnDice();
        SpawnDice();
        SpawnDice();
        SpawnDice();
        SpawnDice();
        SpawnDice();
        SpawnDice();
    }

    public void SpawnDice()
    {
        (float, float) spawnPos = points[index++];
        index %= points.Length;
        Instantiate(diceSprite, this.transform.position + new Vector3(spawnPos.Item1, 0, spawnPos.Item2), Quaternion.Euler(90, 0, 0), transform);
    }
}
