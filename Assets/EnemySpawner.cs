using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] EnemyBase = new Enemy[12];
    public BezierCurve path;
    public WaveData[] waves;
    public bool spawning;
    public int waveId = -1;
    private WaveData currentWave = null;
    private bool blackWhite = true;

    // Start is called before the first frame update
    void Start()
    {
        if(waves == null || waves.Length == 0|| path == null)
        {
            Debug.LogWarning("Fill up Enemy spawner with wave data or path data.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spawning)
        {
            if(currentWave == null || currentWave.finished)
            {
                waveId++;
                if(waveId >= waves.Length)
                {
                    spawning = false;
                    waveId = -1;
                    return;
                }
            }
        }
    }
}
