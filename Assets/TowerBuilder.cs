using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public SpriteRenderer validRangeIndicator;
    private static int droppedCount = 0;
    private int droppedId = 0;
    private Vector3 lastPosition = Vector3.zero;

    private void Update()
    {
        if (validRangeIndicator.enabled && TowerManager.instance.CheckValid(this.transform.position))
        {
            if(TowerManager.instance.CheckGroup(this).Count >= 3)
            {
                validRangeIndicator.color = Color.blue;
            }
            else
            {
                validRangeIndicator.color = Color.green;
            }
        }
        else
        {
            validRangeIndicator.color = Color.red;
        }

        if(lastPosition != this.transform.position)
        {
            TowerManager.instance.RemoveTowerBuilder(this);
            lastPosition = this.transform.position;
        }

    }

    private void OnMouseEnter()
    {
        validRangeIndicator.enabled = true;
    }

    private void OnMouseExit()
    {
        if(validRangeIndicator.color == Color.green)
        {
            validRangeIndicator.enabled = false;
        }
    }

    public void Dropped()
    {
        AkSoundEngine.PostEvent("Play_Build", gameObject);

        droppedId = droppedCount++;

        if(TowerManager.instance.CheckValid(this.transform.position))
        {
            TowerManager.instance.RegisterTowerBuilder(this);

            List<TowerBuilder> group = TowerManager.instance.CheckGroup(this);
            if(group.Count >= 3)
            {
                List<TowerBuilder> newTower = group.GetRange(0, 3);
                
                Vector3 position = newTower[0].transform.position;
                TowerBuilder parapets = newTower[0];
                TowerBuilder spire;
                TowerBuilder bottom;
                if(newTower[1].droppedId > newTower[2].droppedId)
                {
                    spire = newTower[1];
                    bottom = newTower[2];
                }
                else
                {
                    spire = newTower[2];
                    bottom = newTower[1];
                }

                TowerManager.instance.BuildTower(position, bottom, spire, parapets);
            }
            else if (group.Count == 2)
            {
                this.transform.position = group[1].transform.position + new Vector3(0, 0.75f, 0);
                lastPosition = this.transform.position;
            }            
        }
        else
        {
            validRangeIndicator.enabled = true;
            validRangeIndicator.color = Color.red;
        }
    }
}
