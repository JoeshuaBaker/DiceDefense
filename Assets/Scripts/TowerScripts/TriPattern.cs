using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriPattern : ShotPattern
{
    public override bool Shoot(Projectile projectile, ProjectileData projectileData)
    {
        Projectile projInstance = Instantiate(projectile, bulletParent);
        projectileData.initialDirection = projectileData.target.transform.position - projectileData.tower.transform.position;
        projectileData.initialDirection = projectileData.initialDirection.normalized;
        projInstance.Init(projectileData);

        Projectile leftShot = Instantiate(projectile, bulletParent);
        ProjectileData left = projectileData.Copy();
        left.initialDirection = Quaternion.AngleAxis(30, Vector3.up) * left.initialDirection;
        leftShot.Init(left);

        Projectile rightShot = Instantiate(projectile, bulletParent);
        ProjectileData right = projectileData.Copy();
        right.initialDirection = Quaternion.AngleAxis(-30, Vector3.up) * right.initialDirection;
        rightShot.Init(right);
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
