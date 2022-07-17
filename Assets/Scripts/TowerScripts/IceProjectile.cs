using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : Projectile
{
    public GameObject hitParticle;
    public float explodeRadius = 0.5f;
    public float slowPercentage = 0.6f;
    public float slowDuration = 5f;
    public float spikePeriod = 1f;
    private bool pingpong = true;
    private float spikeValue = 0f;
    private SkinnedMeshRenderer meshRenderer;
    private bool isDead = false;
    private float deadTime = 0f;

    private void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    //explode in a small radius, applying slows to all enemies hit with new collider
    public override void OnEnemyCollide(Enemy enemy)
    {
        if (isDead)
            return;

        Collider[] colliders = Physics.OverlapSphere(enemy.transform.position, explodeRadius, 1 << LayerMask.NameToLayer("Enemy"));
        foreach(Collider collider in colliders)
        {
            Enemy splashEnemy = collider.GetComponent<Enemy>();
            if (splashEnemy == null)
                continue;

            splashEnemy.GetHit(data.damage);
            splashEnemy.GetSlowed(slowPercentage, slowDuration);
        }
        Instantiate(hitParticle, transform);
        isDead = true;
        meshRenderer.enabled = false;
        deadTime = 0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(isDead)
        {
            deadTime += Time.deltaTime;
            if(deadTime > 1f)
            {
                Destroy(this.gameObject);
            }
            return;
        }

        this.transform.position = this.transform.position + (initialDirection) * flySpeed * Time.deltaTime;
        if (pingpong)
        {
            spikeValue += Time.deltaTime * 100f;
        }
        else
        {
            spikeValue -= Time.deltaTime * 100f;
        }

        spikeValue = Mathf.Clamp(spikeValue, 0f, 100f);
        meshRenderer.SetBlendShapeWeight(0, spikeValue);

        if (spikeValue == 100f || spikeValue == 0f)
        {
            pingpong = !pingpong;
        }

        base.Update();
    }
}
