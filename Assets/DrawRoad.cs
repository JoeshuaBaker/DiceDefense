using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRoad : MonoBehaviour
{
    public SpriteRenderer roadSprite;
    public int resolution;
    private BezierCurve path;
    public Vector3 offset = new Vector3(.25f, 0, .25f);
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<BezierCurve>();
        for(int i = 0; i < resolution; i++)
        {
            float t = Mathf.Clamp((float)i / (float)resolution, 0.0f, 1.0f);
            float nextT = Mathf.Clamp((float)(i + 1) / (float)resolution, 0.0f, 1.0f);
            Vector3 pos = path.GetPointAt(t);
            Vector3 nextPos = path.GetPointAt(nextT);
            Vector3 drawPos = pos + new Vector3(Random.Range(-offset.x, offset.x), 0, Random.Range(-offset.z, offset.z));
            SpriteRenderer sr = Instantiate(roadSprite, drawPos, Quaternion.Euler(90, 0, 0), this.transform);
            //sr.transform.forward = (pos - nextPos);
            //sr.transform.Rotate(new Vector3(90, 0, 0));
        }
    }
}
