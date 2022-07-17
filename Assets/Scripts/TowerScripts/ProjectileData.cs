using UnityEngine;

public class ProjectileData
{
    public Enemy target;
    public Vector3 initialDirection;
    public Transform aimTarget;
    public Tower tower;
    public float damage;
    public float range;

    public ProjectileData Copy()
    {
        ProjectileData copy = new ProjectileData();
        copy.target = target;
        copy.initialDirection = new Vector3(initialDirection.x, initialDirection.y, initialDirection.z);
        copy.tower = tower;
        copy.damage = damage;
        copy.range = range;
        copy.aimTarget = aimTarget;

        return copy;
    }
}
