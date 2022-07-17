using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : Projectile
{
    public float degreesPerSecond = 180f;
    public float minSpeed = 1f;
    public AnimationCurve travelCurve;
    private bool outIn = true;
    private float distFromTower = 0f;

    //reduce numEnemiesPierced by 1
    public override void OnEnemyCollide(Enemy enemy)
    {
        enemy.GetHit(data.damage);
        Debug.Log("hit " + enemy.name);
    }

    public override void Init(ProjectileData data)
    {
        base.Init(data);
    }

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("RangShoot", gameObject);
    }

    // Update is called once per frame
    protected override void Update()
    {
        distFromTower = Vector3.Distance(transform.position, data.tower.transform.position);
        float t = Mathf.Clamp(distFromTower / (data.range/2), 0, 1);

        if(t == 1)
        {
            outIn = false;
            initialDirection = -initialDirection;
        }

        Vector3 speed = initialDirection * travelCurve.Evaluate(t) * Time.deltaTime * flySpeed;
        if(speed.magnitude < minSpeed*Time.deltaTime)
        {
            speed = speed.normalized * minSpeed * Time.deltaTime;
        }
        this.transform.position = this.transform.position + speed;
        
        if(t < 0.1f && outIn == false)
        {
            data.tower.fireSignal = true;
            Destroy(this.gameObject);
        }

        this.transform.Rotate(Time.deltaTime * degreesPerSecond, 0, 0);
    }
}
