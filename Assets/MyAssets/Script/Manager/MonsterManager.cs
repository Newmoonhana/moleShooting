using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    //GameManager
    MapManager mapM_scp;

    //전역 변수. 
    public GameObject Monster_obj;  //몬스터 부모 오브젝트.
    public List<GameObject> MonPre_list = new List<GameObject>();   //출현 몬스터 종류 프리팹.
    public List<GameObject> BulPre_list = new List<GameObject>();   //탄막 종류 프리팹.
    public List<GameObject> MonObj_list = new List<GameObject>();

    //몬스터 필드 내 위치 리스트 사이즈 변경.
    public void MonObjListResize()
    {
        mapM_scp = GameManager.inst.mapM;
        MonObj_list.Clear();
        for (int i = 0; i < mapM_scp.TileXY.x * mapM_scp.TileXY.y; i++)
            MonObj_list.Add(null);
    }

    //몬스터 생성 실행 함수.
    public void CreateMon()
    {
        MonsterClass mon = SelectMonster();
        AddMon(mon);
    }

    //몬스터 랜덤 선택.
    MonsterClass SelectMonster()
    {
        MonsterClass mon = RandMon();
        return mon;
    }

    //몬스터 생성.
    void AddMon(MonsterClass monClass)
    {

        Point monPart = monClass.PartenOn();
        if (monPart == new Point(-1, -1))
            return;

        int index = mapM_scp.TilePointToId(monPart);
        GameObject mon = Instantiate<GameObject>(monClass.mon_obj);
        mon.transform.SetParent(Monster_obj.transform);
        mon.transform.position = mapM_scp.MonTileObj_list[index].transform.position;
        mon.name = MonReName(mon, monPart);

        Monster monScp = mon.GetComponent<Monster>();
        monScp.monClass = monClass;
        monScp.monPart = monClass.mon_part;
        MonObj_list[index] = mon;
        monScp.StartCoroutine("BulletShoot");
    }

    //몬스터 선택(확률).
    MonsterClass RandMon()
    {
        MonsterClass mon = null;
        while (mon == null)
        {
            int rand = Random.Range(0, 100);
            if (rand < 1)
            {
                mon = new Mon00(MonPre_list[0], BulPre_list[0]);
                if (mon.mon_part.row[0] != -1)
                    break;
                else
                    mon = null;
            }
            else if (rand < 2)
            {
                mon = new Mon01(MonPre_list[1], BulPre_list[1]);
                if (mon.mon_part.row[0] != -1)
                    break;
                else
                    mon = null;
            }
            else if (rand < 3)
            {
                mon = new Mon02(MonPre_list[2], BulPre_list[2]);
                if (mon.mon_part.row[0] != -1)
                    break;
                else
                    mon = null;
            }
            else if (rand < 4)
            {
                mon = new Mon04(MonPre_list[4], BulPre_list[4]);
                if (mon.mon_part.row[0] != -1)
                    break;
                else
                    mon = null;
            }
            else if (rand < 5)
            {
                mon = new Mon05(MonPre_list[5], BulPre_list[5]);
                if (mon.mon_part.row[0] != -1)
                    break;
                else
                    mon = null;
            }
            else
            mon = null;
        }

        return mon;
    }

    //몬스터 오브젝트 이름 재설정.
    public string MonReName(GameObject obj, Point XY)
    {
        string name = obj.name.Substring(0, 6) + (XY.x) + (XY.y) + "_obj";
        return name;
    }
}
