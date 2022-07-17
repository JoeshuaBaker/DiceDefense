using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialPattern : ShotPattern
{
    public override bool Shoot(Projectile projectile, ProjectileData projectileData)
    {
        for(int i = 0; i < 6; i++)
        {
            ProjectileData data = projectileData.Copy();
            Projectile projInstance = Instantiate(projectile, bulletParent);
            data.initialDirection = data.target.transform.position - data.tower.transform.position;
            data.initialDirection = Quaternion.AngleAxis(60f*i, Vector3.up) * data.initialDirection;
            data.initialDirection = data.initialDirection.normalized;
            projInstance.Init(data);
        }
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
