using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
    public EnemySpawner enemySpawner;

    public void SkipWave()
    {
        enemySpawner.Skip();
    }
}
