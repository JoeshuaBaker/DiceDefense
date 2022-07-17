using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;

    public bool finished = false;
    public float slowPercent = 0.0f;
    public float slowDuration = 0.0f;

    private float timer = 0f;
    private bool initialized = false;
    public float pathLength = 0f;
    public float timeToComplete = 0f;
    public float progress;

    public void Init(EnemyData data)
    {
        this.data = data;
        initialized = true;
        pathLength = data.path.length;
        timeToComplete = pathLength / data.speed;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(initialized && !finished)
        {
            timer += Time.deltaTime*(1.0f - slowPercent);

            if(slowDuration > 0f)
            {
                slowDuration -= Time.deltaTime;
            }
            else if(slowDuration < 0f)
            {
                slowDuration = 0f;
                slowPercent = 0f;
            }

            progress = timer / timeToComplete;
            if (progress >= 1)
            {
                finished = true;
                return;
            }

            Vector3 point = data.path.GetPointAt(progress);
            this.transform.parent.position = point;
        }
    }

    public void GetHit(float damage)
    {
        data.hp -= damage;
        if(data.hp <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void GetSlowed(float slow, float slowDuration)
    {
        slowPercent = slow;
        this.slowDuration = slowDuration;
    }
}
