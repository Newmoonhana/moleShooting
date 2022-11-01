using UnityEngine;

[System.Serializable]
public class MonsterClass
{
    public int mon_id;
    public string mon_name;
    public GameObject mon_obj;
    public GameObject bul_obj;
    public Parten mon_part;
    public int mon_power;
    public BulM mon_bulM;

    public Point PartenOn()
    {
        return mon_part.PartenOn();
    }
}