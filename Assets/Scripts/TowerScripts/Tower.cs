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
    public Transform projectileParent;
    public List<Enemy> enemiesInRange;
    public Transform aimTarget;

    public AnyDice baseDice;
    public AnyDice spireDice;
    public AnyDice parapetDice;

    private Projectile currentProjectile;
    private AttackArea currentArea;
    private ShotPattern currentPattern;
    private bool aimToggle = false;

    public float timer;
    public float failCheckTimeout = 0.25f;
    public bool waitToFire = false;
    public bool fireSignal = false;

    public float finalRange = 1f;
    public float finalFireRate = 1f;
    public float finalDamage = 1f;

    public Enemy[] lastFoundEnemies;

    // Start is called before the first frame update
    void Start()
    {
        currentProjectile = projectiles[(int)projectileType];
        currentArea = new AttackArea();
        currentArea.Init(areaType);
        currentPattern = patterns[(int)patternType];
        currentPattern.bulletParent = projectileParent;
        waitToFire = currentProjectile.projectileType == Projectile.ProjectileType.Boomerang;
        fireSignal = waitToFire;
        if (patternType == ShotPattern.PatternType.Aimed)
        {
            Vector3 closestPathPoint = TowerManager.instance.GetClosestPointOnPath(transform.position);
            aimTarget.position = closestPathPoint;
        }

        SetFaces((int)projectileType + 1, (int)areaType + 1, (int)patternType + 1);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTypes();
        timer += Time.deltaTime;

        if (patternType == ShotPattern.PatternType.Aimed && aimToggle)
        {
            aimTarget.gameObject.SetActive(true);
            Vector3 worldpoint;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(TowerUI.instance.canvas.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out worldpoint);
            aimTarget.transform.position = worldpoint;
            aimTarget.transform.position = new Vector3(aimTarget.transform.position.x, this.transform.position.y, aimTarget.transform.position.z);
            if (Input.GetMouseButtonUp(0))
            {
                aimToggle = false;
                aimTarget.gameObject.SetActive(false);
            }
        }

        finalRange = currentProjectile.range;
        finalRange *= currentArea.rangeModifier;
        finalRange *= currentPattern.rangeMultiplier;

        radius.transform.localScale = new Vector3(finalRange, finalRange, 1);

        finalFireRate = currentProjectile.fireRate;
        finalFireRate /= currentArea.fireRateMultiplier;
        finalFireRate /= currentPattern.fireRateMultiplier;

        finalDamage = currentProjectile.damage;
        finalDamage *= currentPattern.damageMultiplier;

        if(waitToFire)
        {
            if(!fireSignal)
            {
                timer = 0;
                return;
            }
        }

        if(timer > finalFireRate)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, finalRange/2, 1 << LayerMask.NameToLayer("Enemy"));
            if(colliders.Length == 0)
            {
                timer -= failCheckTimeout;
                return;
            }

            Enemy closestEnemy = null;
            float maxProgress = -1f;
            List<Enemy> allFoundEnemies = new List<Enemy>();

            foreach(Collider collider in colliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy == null)
                    continue;

                allFoundEnemies.Add(enemy);

                if(enemy.progress > maxProgress)
                {
                    closestEnemy = enemy;
                    maxProgress = enemy.progress;
                }
            }

            lastFoundEnemies = allFoundEnemies.ToArray();
            ProjectileData shootData = new ProjectileData
            {
                target = closestEnemy,
                damage = finalDamage,
                range = finalRange,
                tower = this,
                aimTarget = aimTarget
            };

            if (currentPattern.Shoot(currentProjectile, shootData))
            {
                timer = 0;
                if(waitToFire)
                {
                    fireSignal = false;
                }
            }
            else
            {
                timer -= failCheckTimeout;
            }
        }
    }

    private void CheckTypes()
    {
        if(projectileType != currentProjectile.projectileType)
        {
            currentProjectile = projectiles[(int)projectileType];
            waitToFire = currentProjectile.projectileType == Projectile.ProjectileType.Boomerang;
            fireSignal = waitToFire;
        }
        
        if(areaType != currentArea.areaType)
        {
            currentArea.Init(areaType);
        }

        if(patternType != currentPattern.patternType)
        {
            currentPattern = patterns[(int)patternType];
            currentPattern.bulletParent = projectileParent;
        }
    }

    public void SetFaces(int bottom, int spire, int parapet)
    {
        baseDice.ShowFace(bottom);
        spireDice.ShowFace(spire);
        parapetDice.ShowFace(parapet);

        if(patternType == ShotPattern.PatternType.Aimed)
        {
            Vector3 closestPathPoint = TowerManager.instance.GetClosestPointOnPath(transform.position);
            aimTarget.position = closestPathPoint;
        }
    }

    public void OnMouseOver()
    {
        TowerUI.instance.ShowText((int)projectileType + 1, (int)areaType + 1, (int)patternType + 1);
        if(patternType == ShotPattern.PatternType.Aimed && !aimToggle)
        {
            aimTarget.gameObject.SetActive(true);
        }
        radius.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        radius.gameObject.SetActive(false);
        if (patternType == ShotPattern.PatternType.Aimed && !aimToggle)
        {
            aimTarget.gameObject.SetActive(false);
        }
        TowerUI.instance.ShowText();
    }

    public void OnMouseDown()
    {
        if (patternType == ShotPattern.PatternType.Aimed)
        {
            aimToggle = true;
        }
    }
}
