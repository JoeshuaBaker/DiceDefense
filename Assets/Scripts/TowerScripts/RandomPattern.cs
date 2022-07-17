using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPattern : ShotPattern
{
    public override bool Shoot(Projectile projectile, ProjectileData projectileData)
    {
        Projectile projInstance = Instantiate(projectile, bulletParent);
        projectileData.initialDirection = projectileData.target.transform.position - projectileData.tower.transform.position;
        projectileData.initialDirection = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * projectileData.initialDirection;
        projectileData.initialDirection = projectileData.initialDirection.normalized;
        projInstance.Init(projectileData);
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
