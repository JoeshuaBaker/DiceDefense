using System.Collections;
[System.Serializable]
public class EnemyData
{
    public EnemyTypes type;
    public float hp = 10;
    public float speed = 1;
    public int count = 1;
    public float time = 1;
    [System.NonSerialized] public BezierCurve path;
}
