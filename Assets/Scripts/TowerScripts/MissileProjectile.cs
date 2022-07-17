using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : Projectile
{
    public GameObject hitParticle;
    public float explodeRadius = 0.5f;
    private MeshRenderer meshRenderer;
    private bool isDead = false;
    private float deadTime = 0f;
    //explode in large radius, dealing damage to all enemies hit
    public override void OnEnemyCollide(Enemy enemy)
    {
        if (isDead)
            return;

        Collider[] colliders = Physics.OverlapSphere(enemy.transform.position, explodeRadius, 1 << LayerMask.NameToLayer("Enemy"));
        foreach (Collider collider in colliders)
        {
            AkSoundEngine.PostEvent("MissileHit", gameObject);

            Enemy splashEnemy = collider.GetComponent<Enemy>();
            if (splashEnemy == null)
                continue;

            splashEnemy.GetHit(data.damage);
        }
        Instantiate(hitParticle, transform);
        isDead = true;
        meshRenderer.enabled = false;
        deadTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("MissileShoot", gameObject);

        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isDead)
        {
            deadTime += Time.deltaTime;
            if (deadTime > 1f)
            {
                Destroy(this.gameObject);
            }
            return;
        }

        this.transform.position = this.transform.position + (initialDirection) * flySpeed * Time.deltaTime;
        base.Update();
    }
}
