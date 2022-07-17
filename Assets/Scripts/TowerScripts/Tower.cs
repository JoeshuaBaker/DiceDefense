using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Projectile[] projectiles;
    public AttackArea attackArea;
    public ShotPattern[] patterns;

    public Projectile.ProjectileType projectileType;
    public AttackArea.AreaType areaType;
    public ShotPattern.PatternType patternType;

    public SpriteRenderer radius;
    public SphereCollider sphereCollider;
    public List<Enemy> enemiesInRange;

    private Projectile currentProjectile;
    private AttackArea currentArea;
    private ShotPattern currentPattern;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        currentProjectile = projectiles[(int)projectileType];
        currentArea = new AttackArea();
        currentArea.Init(areaType);
        currentPattern = patterns[(int)patternType];
    }

    // Update is called once per frame
    void Update()
    {
        CheckTypes();
        timer += Time.deltaTime;

        float finalFireRate = currentProjectile.fireRate;
        finalFireRate /= currentArea.fireRateMultiplier;
        finalFireRate /= currentPattern.fireRateMultiplier;

        if(timer > finalFireRate)
        {
            if(currentPattern.Shoot(currentProjectile))
            {
                timer = 0;
            }
        }
    }

    private void CheckTypes()
    {
        if(projectileType != currentProjectile.projectileType)
        {
            currentProjectile = projectiles[(int)projectileType];
        }
        
        if(areaType != currentArea.areaType)
        {
            currentArea.Init(areaType);
        }

        if(patternType != currentPattern.patternType)
        {
            currentPattern = patterns[(int)patternType];
        }
    }
}
