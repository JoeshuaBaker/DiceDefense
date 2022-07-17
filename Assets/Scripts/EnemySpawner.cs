using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemyBase = new Enemy[12];
    public BezierCurve path;
    public Transform enemyParent;
    public WaveData[] waves;
    public bool spawning;
    public int waveId = -1;
    private WaveData currentWave = null;
    private bool whiteBlack = true;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(waves == null || waves.Length == 0|| path == null)
        {
            Debug.LogWarning("Fill up Enemy spawner with wave data or path data.");
        }
        timer = 0f;

        Wave[] childWaves = GetComponentsInChildren<Wave>();
        waves = new WaveData[childWaves.Length];
        for(int i = 0; i < childWaves.Length; i++)
        {
            waves[i] = childWaves[i].data;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spawning)
        {
            timer += Time.deltaTime;
            //Handle moving to the next wave...
            if(currentWave == null || currentWave.finished)
            {
                waveId++;
                if(waveId >= waves.Length)
                {
                    spawning = false;
                    waveId = -1;
                    return;
                }

                currentWave = waves[waveId];
                currentWave.Setup();
            }

            //Process current wave
            if(currentWave.IsReady(timer))
            {
                EnemyData enemy = currentWave.Spawn(timer);
                if (enemy == null)
                {
                    //null enemy is returned at the end of the wave...
                    return;
                }

                EnemyTypes type = enemy.type;
                int prefabIndex = ((int)type) * 2 + ((whiteBlack) ? 0 : 1);
                enemy.path = path;
                whiteBlack = !whiteBlack;
                GameObject prefabParent = new GameObject();
                prefabParent.transform.parent = enemyParent;
                prefabParent.transform.localPosition = Vector3.zero;
                prefabParent.transform.localRotation = Quaternion.Euler(0, 0, 0);
                prefabParent.transform.localScale = Vector3.one;
                Enemy prefab = Instantiate(enemyBase[prefabIndex], prefabParent.transform, false);
                prefabParent.name = prefab.name + " Parent";
                prefab.Init(enemy);
                
            }
        }
    }
}
