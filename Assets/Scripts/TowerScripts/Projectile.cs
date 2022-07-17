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
    private float distanceTraveled;
    private float timer;
    protected Vector3 initialDirection;

    public virtual void Init(ProjectileData data)
    {
        this.data = data;
        transform.LookAt(data.target.transform);
        if(projectileType != ProjectileType.Boomerang)
        {
            transform.Rotate(new Vector3(90, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 90));
        }
        initialDirection = data.target.transform.position - this.transform.position;
        initialDirection = initialDirection.normalized;
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
        if(distanceTraveled > data.range)
        {
            Destroy(gameObject);
        }
    }

    public abstract void OnEnemyCollide(Enemy enemy);
}
