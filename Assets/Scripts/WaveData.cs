using UnityEngine;

[System.Serializable]
public class WaveData 
{
    public EnemyData[] enemies;
    public float restTime = 5f;
    public int repeats = 0;
    public int enemyId = 0;
    public bool finished = false;

    private float lastTime = -1;
    private int[] counts = null;
    private int repeatsDone = 0;

    public void Setup(bool resetRepeats = false)
    {
        enemyId = 0;
        lastTime = -1;
        if(resetRepeats) 
            repeatsDone = 0;

        //first init call
        if(counts == null)
        {
            counts = new int[enemies.Length];
            for (int i = 0; i < enemies.Length; i++)
            {
                counts[i] = enemies[i].count;
            }
        }
        //subsequent init call
        else
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].count = counts[i];
            }
        }
    }

    public bool IsReady(float time)
    {
        if (lastTime == -1)
            return true;

        float diff = time - lastTime;
        EnemyData data = enemies[enemyId];
        if(data.count > 0)
        {
            TowerUI.instance.skipButton.gameObject.SetActive(false);
            return diff > data.time;
        }
        else
        {
            TowerUI.instance.skipButton.gameObject.SetActive(true);
            return diff > restTime;
        }
    }

    public EnemyData Spawn(float time)
    {
        if(enemyId < 0 || enemyId >= enemies.Length)
        {
            Debug.Log("Tried to access id: " + enemyId);   
        }
        EnemyData currentEnemy = enemies[enemyId];
        if(currentEnemy.count > 0)
        {
            currentEnemy.count -= 1;
            lastTime = time;
            return currentEnemy;
        }
        else
        {
            enemyId++;
            if(enemyId >= enemies.Length)
            {
                if(repeatsDone < repeats)
                {
                    repeatsDone++;
                    Setup();
                    return Spawn(time);
                }
                else
                {
                    finished = true;
                    return null;
                }
            }
            else
            {
                currentEnemy = enemies[enemyId];
                currentEnemy.count -= 1;
                lastTime = time;
                return currentEnemy;
            }
        }
    }
}
