using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Projectile : MonoBehaviour
{
    public enum ProjectileType
    {
        Bullet,
        Ice,
        Missile,
        Boomerang,
        Laser,
        Homing
    }

    public ProjectileType projectileType;
    public float damage;
    public float range;
    public float fireRate;
    public float flySpeed;
    public int numEnemiesPierce;
    public ProjectileData data;
    protected float distanceTraveled;
    protected float timer;
    protected Vector3 initialDirection;

    public virtual void Init(ProjectileData data)
    {
        this.data = data;
        transform.LookAt(transform.position + data.initialDirection * flySpeed);
        if(projectileType == ProjectileType.Boomerang)
        {
            transform.Rotate(new Vector3(0, 0, 90));
        }
        initialDirection = data.initialDirection;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            OnEnemyCollide(enemy);
        }
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        distanceTraveled = timer * flySpeed;
        if(distanceTraveled > data.range/2)
        {
            Destroy(gameObject);
        }
    }

    public abstract void OnEnemyCollide(Enemy enemy);
}
