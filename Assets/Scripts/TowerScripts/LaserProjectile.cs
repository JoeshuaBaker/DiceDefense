using UnityEngine;

public class LaserProjectile : Projectile
{
    public GameObject hitParticle;
    bool hasMadeParticle = false;
    GameObject particle = null;
    //reduce numEnemiesPierced by 1
    public override void OnEnemyCollide(Enemy enemy)
    {
        enemy.GetHit(data.damage);
        if(!hasMadeParticle)
        {
            particle = Instantiate(hitParticle, transform.parent);
            particle.transform.position = this.transform.position;
            hasMadeParticle = true;
            Destroy(particle, 1.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("PierceShoot", gameObject);

        hasMadeParticle = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        this.transform.position = this.transform.position + (initialDirection) * flySpeed * Time.deltaTime;
        base.Update();
    }
}