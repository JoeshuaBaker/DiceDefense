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
    private float distanceTraveled;

    public abstract void OnEnemyCollide(Enemy enemy);
}
