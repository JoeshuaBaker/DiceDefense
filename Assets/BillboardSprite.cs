using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BillboardSprite : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.forward = -Camera.main.transform.forward;
    }
}
