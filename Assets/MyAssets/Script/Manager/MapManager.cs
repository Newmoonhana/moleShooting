using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //전역 변수.
    public GameObject MonsterTile_obj;  //몬스터타일 부모 오브젝트.
    public GameObject MonsterTile_pre;  //생성할 타일 프리팹.
    public Point TileXY = new Point(5, 5);
    public List<GameObject> MonTileObj_list = new List<GameObject>();
    public List<MonTile> MonTileScp_list = new List<MonTile>();
    int tilemapSize = 118 * 5;
    public int tileSprSize; //타일 스프라이트 당 사이즈 값.

    //GameManager
    MouseManager mouseM_scp;
    MonsterManager monM_scp;

    void Awake()
    {
        mouseM_scp = GameManager.inst.mouseM;
        monM_scp = GameManager.inst.monM;

        if (globalVar.inst.selectStage == 0)
            TileXY = new Point(5, 5);
        else if (globalVar.inst.selectStage <= 3)
            TileXY = new Point(4, 4);
        else if (globalVar.inst.selectStage <= 6)
            TileXY = new Point(5, 5);
        else if (globalVar.inst.selectStage <= 9)
            TileXY = new Point(6, 6);
        tileSprSize = tilemapSize / TileXY.x;
        MonsterTile_obj.transform.position = new Vector3(1280 - tileSprSize / 2 - 65, tileSprSize / 2 + 15, 0);
        //타일 배치.
        for (int i = 0; i < TileXY.x * TileXY.y; i++)
            AddTile(i);
        monM_scp.MonObjListResize();
    }

    //게임 시작.
    public void GameRun()
    {
        //게임 돌아가는 함수.
        StartCoroutine("UpdateCor");
    }


    //타일 생성.
    public void AddTile(int i)
    {
        GameObject tile = Instantiate<GameObject>(MonsterTile_pre);
        MonTile tileScp = tile.GetComponent<MonTile>();

        tileScp.thisXY = new Point(i / TileXY.x, i % TileXY.x);

        tile.transform.SetParent(MonsterTile_obj.transform);
        tile.transform.localScale = new Vector3(tileSprSize, tileSprSize, 1);
        tile.transform.localPosition = new Vector3(-tileSprSize * ((TileXY.x - 1) - (i / TileXY.x)), tileSprSize * ((TileXY.y - 1) - (i % TileXY.x)), 0);
        tile.name = TileReName(tile, tileScp.thisXY);

        MonTileObj_list.Add(tile);
        MonTileScp_list.Add(tileScp);
    }
    //타일 오브젝트 이름에서 좌표 배열값 추출.
    public Point TileXYInt(GameObject obj)
    {
        string nameX = obj.name.Substring(11, 12);
        string nameY = obj.name.Substring(12, 13);
        return new Point(int.Parse(nameX), int.Parse(nameY));
    }
    //타일 오브젝트 이름 재설정.
    public string TileReName(GameObject obj, Point XY)
    {
        string name = obj.name.Substring(0, 11) + (XY.x) + (XY.y) + "_obj";
        return name;
    }
    //타일 포인트 값을 인덱스 화.
    public int TilePointToId(Point p)
    {
        return p.x * TileXY.x + p.y;
    }

    public IEnumerator UpdateCor()
    {
        monM_scp.CreateMon();
        yield return GameManager.inst.stageWait;
        StartCoroutine("UpdateCor");
    }

    public Point TileMouseClick()   //타일 클릭 체크.
    {
        for (int i = 0; i < MonTileObj_list.Count; i++)
            if (MonTileObj_list[i] == mouseM_scp.hit_obj)
                return MonTileScp_list[i].thisXY;
                
        return new Point(-1, -1);
    }
}
