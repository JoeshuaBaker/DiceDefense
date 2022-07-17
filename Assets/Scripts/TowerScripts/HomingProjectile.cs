using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    public float turnSpeed = 1.0f;
    private Vector3 lastDirection;
    //reduce numEnemiesPierced by 1
    public override void OnEnemyCollide(Enemy enemy)
    {
        enemy.GetHit(data.damage);
        Destroy(gameObject);
    }

    // Update is called once per frame
    protected override void Update()
    {
        Vector3 homingDirection;
        if(data.target == null)
        {
            homingDirection = lastDirection;
        }
        else
        {
            homingDirection = (data.target.transform.position - this.transform.position);
            homingDirection = homingDirection.normalized;
            lastDirection = homingDirection;
        }

        Vector3 nextDirection = this.transform.position +
            initialDirection * flySpeed * Time.deltaTime * (1.0f - Mathf.Min((distanceTraveled / (data.range * 1.5f)), 1.0f)) +
            homingDirection * flySpeed * Time.deltaTime * Mathf.Min((distanceTraveled / (data.range * 1.5f)), 1.0f);

        transform.LookAt(nextDirection);
        transform.Rotate(new Vector3(90, 0, 0));

        this.transform.position = new Vector3(nextDirection.x, 0, nextDirection.z);

        float distFromTower = Vector3.Distance(this.transform.position, data.tower.transform.position);

        timer += Time.deltaTime;
        distanceTraveled = timer * flySpeed;
        if (distFromTower > data.range / 2)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AkSoundEngine.PostEvent("HomeShot", gameObject);
    }
}
