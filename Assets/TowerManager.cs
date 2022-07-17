using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    public Tower towerPrefab;
    public Transform towersParent;
    public EnemySpawner enemySpawner;
    public float pathRadius = 2.0f;

    private List<Tower> towers;
    private List<TowerBuilder> builders;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple tower managers in the scene.");
        }
        instance = this;
        towers = new List<Tower>();
        builders = new List<TowerBuilder>();

        Tower[] preplacedTowers = FindObjectsOfType(typeof(Tower)) as Tower[];
        towers.AddRange(preplacedTowers);
    }

    public bool CheckValid(Vector3 position)
    {
        foreach(Tower tower in towers)
        {
            if(Vector3.Distance(tower.transform.position, position) <= 1.0f)
            {
                return false;
            }
        }
        Vector3 closestPathPoint = GetClosestPointOnPath(position);
        if (Vector3.Distance(closestPathPoint, position) < pathRadius)
            return false;

        return true;
    }

    public List<TowerBuilder> CheckGroup(TowerBuilder member)
    {
        List<TowerBuilder> pairs = new List<TowerBuilder>();
        pairs.Add(member);

        foreach (TowerBuilder builder in builders)
        {
            if (builder == member)
                continue;

            float nextDist = Vector3.Distance(builder.transform.position, member.transform.position);
            if (nextDist <= 1.0f)
            {
                pairs.Add(builder);
            }
        }

        return pairs;
    }

    public void RegisterTowerBuilder(TowerBuilder towerBuilder)
    {
        builders.Add(towerBuilder);
    }

    public void RemoveTowerBuilder(TowerBuilder towerBuilder)
    {
        builders.Remove(towerBuilder);
    }

    public void BuildTower(Vector3 position, TowerBuilder bottom, TowerBuilder spire, TowerBuilder parapets)
    {
        //remove towerbuilder from list and destroy, instantiate tower at its location, add to towersParent and towers list
        RemoveTowerBuilder(bottom);
        RemoveTowerBuilder(spire);
        RemoveTowerBuilder(parapets);

        DiceSprite bottomSprite = bottom.GetComponent<AnyDice>().diceSprite;
        DiceSprite spireSprite = spire.GetComponent<AnyDice>().diceSprite;
        DiceSprite parapetsSprite = parapets.GetComponent<AnyDice>().diceSprite;

        int bottomNum = bottomSprite.face;
        int spireNum = spireSprite.face;
        int parapetsNum = parapetsSprite.face;

        Destroy(bottomSprite.gameObject);
        Destroy(spireSprite.gameObject);
        Destroy(parapetsSprite.gameObject);

        Tower newTower = Instantiate(towerPrefab, position, Quaternion.Euler(0, 0, 0), towersParent);
        newTower.projectileType = (Projectile.ProjectileType)(bottomNum - 1);
        newTower.areaType = (AttackArea.AreaType)(spireNum - 1);
        newTower.patternType = (ShotPattern.PatternType)(parapetsNum - 1);

        newTower.SetFaces(bottomNum, spireNum, parapetsNum);

    }

    public Vector3 GetClosestPointOnPath(Vector3 position)
    {
        BezierCurve path = enemySpawner.path;

        Vector3 closestPoint = Vector3.zero;
        float dist = 1000000f;

        for(int i = 0; i < path.resolution; i++)
        {
            float t = (float)i / (float)path.resolution;
            Vector3 point = path.GetPointAt(t);
            float distToPos = Vector3.Distance(point, position);
            if (distToPos < dist)
            {
                closestPoint = point;
                dist = distToPos;
            }
        }

        return closestPoint;
    }
}
