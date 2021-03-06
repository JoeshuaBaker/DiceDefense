using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    //destroy this projectile
    public override void OnEnemyCollide(Enemy enemy)
    {
        enemy.GetHit(data.damage);
        Destroy(gameObject);
    }

    // Update is called once per frame
    protected override void Update()
    {
        this.transform.position = this.transform.position + (initialDirection) * flySpeed * Time.deltaTime;
        base.Update();
    }

    private void Start()
    {
        AkSoundEngine.PostEvent("StdShoot", gameObject);
    }
}
