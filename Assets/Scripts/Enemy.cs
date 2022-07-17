using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    Animator animator;
    public GameObject diceParticle;

    public bool finished = false;
    public float slowPercent = 0.0f;
    public float slowDuration = 0.0f;

    private float timer = 0f;
    private bool initialized = false;
    private int maxHp = 0;
    public float pathLength = 0f;
    public float timeToComplete = 0f;
    public float progress;

    public void Init(EnemyData data)
    {
        this.data = data;
        maxHp = (int) data.hp;
        initialized = true;
        pathLength = data.path.length;
        timeToComplete = pathLength / data.speed;
        timer = 0;
        this.animator = GetComponent<Animator>();
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
                animator.speed = 1.0f - slowPercent;
            }
            else if(slowDuration < 0f)
            {
                slowDuration = 0f;
                slowPercent = 0f;
                animator.speed = 1.0f;
            }

            progress = timer / timeToComplete;
            if (progress >= 1)
            {
                finished = true;
                DealDamage();
                return;
            }

            Vector3 point = data.path.GetPointAt(progress);
            this.transform.parent.position = point;
        }
    }

    public void GetHit(float damage)
    {
        AkSoundEngine.PostEvent("EnemyHit", gameObject);

        data.hp -= damage;
        if(data.hp <= 0)
        {
<<<<<<< Updated upstream
            AkSoundEngine.PostEvent("EnemyDie", gameObject);
=======
            if(TowerUI.instance.GetDiceScore(this.maxHp*(int)data.speed))
            {
                GameObject dp = Instantiate(diceParticle, transform.parent.parent);
                Destroy(dp, 1.5f);
            }

>>>>>>> Stashed changes
            Destroy(transform.parent.gameObject);
        }
    }

    public void GetSlowed(float slow, float slowDuration)
    {
        slowPercent = slow;
        this.slowDuration = slowDuration;
    }

    public void DealDamage()
    {
        int damage = (int)data.type + 1;
        if (data.type == EnemyTypes.Queen || data.type == EnemyTypes.King)
        {
            damage *= 2;
        }
        TowerUI.instance.DealDamage(damage);
        GetHit(data.hp);
    }
}
