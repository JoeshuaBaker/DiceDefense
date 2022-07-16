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

    public ProjectileType type;
    public int damage;
    public int range;
    public int fireRate;
    public int numEnemiesPierce;

    public abstract void OnEnemyCollide(Enemy enemy);
}
