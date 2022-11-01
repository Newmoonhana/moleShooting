using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PartenInfo
{
    public int []row = { 0, 0, 0 };
    public int partenId;
    public WaitForSeconds startTime;
    public WaitForSeconds endTime;
}

public class Parten : PartenInfo
{
    public Parten(int id)
    {
        partenId = id;
        switch (partenId)
        {
            case 0:
                startTime = new WaitForSeconds(2);
                endTime = new WaitForSeconds(1);
                break;
            case 5:
                startTime = new WaitForSeconds(2);
                endTime = new WaitForSeconds(9999);
                break;
        }
    }
    public Parten(Parten p)
    {
        partenId = p.partenId;
        startTime = p.startTime;
        endTime = p.endTime;
    }

    public Point PartenOn()  //패턴값 설정.
    {
        switch(partenId)
        {
            case 0:
                return Parten00();
        }
        return new Point(-1, -1);
    }

    //사용할 가로를 받아서 생성할 포인트를 찾는 패턴.
    Point Parten00()
    {
        Point xy;
        MapManager mapM = GameManager.inst.mapM;
        MonsterManager monM = GameManager.inst.monM;
        //사용 가능한 x값 설정.
        int xMax = mapM.TileXY.x;
        int yMax = mapM.TileXY.y;
        int[] rowX = new int[xMax];
        for (int i = 0; i < xMax; i++)
            rowX[i] = 1;
        for (int j = 0; j < xMax; j++)
            for (int i = 0; i < yMax; i++)
            {
                int index = mapM.TilePointToId(new Point(j, i));
                //세로에 빈 칸이 한 칸이라도 있으면 다음 가로 검사.
                if (monM.MonObj_list[index] == null)
                    break;

                //세로에 빈 칸이 없을 시 가로 사용 불가.
                if (i + 1 == yMax)
                    rowX[j] = 0;
            }
        if (rowX[0] == 0)
            row[0] = 0;
        if (rowX[1] == 0 && rowX[2] == 0)
            row[1] = 0;
        for (int i = 3; i < rowX.Length; i++)
            if (rowX[i] == 0)
                row[2] = 0;

        if (row[0] == 0)
            rowX[0] = 0;
        if (row[1] == 0)
        {
            rowX[1] = 0;
            rowX[2] = 0;
        }
        if (row[2] == 0)
            for (int i = 3; i < rowX.Length; i++)
                rowX[i] = 0;

        //생성 받을 열 할당 안받을 시.
        if (row[0] == 0)
            if (row[1] == 0)
                if (row[2] == 0)
                    return new Point(-1, -1);

        //x값 설정.
        while (true)
        {
            xy.x = Random.Range(0, xMax);
            if (rowX[xy.x] == 1)
                break;
        }
        //y값 설정.
        while (true)
        {
            xy.y = Random.Range(0, yMax);
            int index = mapM.TilePointToId(xy);
            if (monM.MonObj_list[index] == null)
                break;
        }
        return xy;
    }
}

public class MonRow00
{
    List<int[]> rowList = new List<int[]>();
    public MonRow00()
    {
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
    }
    public int[] rowSelect()
    {
        return rowList[globalVar.inst.selectStage];
    }
}
public class MonRow01
{
    List<int[]> rowList = new List<int[]>();
    public MonRow01()
    {
        rowList.Add(new int[3] { 0, 1, 1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { 0, 1, 1 });
        rowList.Add(new int[3] { 0, 0, 1 });
        rowList.Add(new int[3] { 0, 0, 1 });
        rowList.Add(new int[3] { 0, 0, 1 });
        rowList.Add(new int[3] { 0, 0, 1 });
        rowList.Add(new int[3] { 0, 0, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
    }
    public int[] rowSelect()
    {
        return rowList[globalVar.inst.selectStage];
    }
}
public class MonRow02
{
    List<int[]> rowList = new List<int[]>();
    public MonRow02()
    {
        rowList.Add(new int[3] { 0, 1, 1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { 0, 1, 1 });
        rowList.Add(new int[3] { 0, 1, 1 });
        rowList.Add(new int[3] { 0, 1, 1 });
        rowList.Add(new int[3] { 0, 1, 1 });
        rowList.Add(new int[3] { 0, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
    }
    public int[] rowSelect()
    {
        return rowList[globalVar.inst.selectStage];
    }
}
public class MonRow04
{
    List<int[]> rowList = new List<int[]>();
    public MonRow04()
    {
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
    }
    public int[] rowSelect()
    {
        return rowList[globalVar.inst.selectStage];
    }
}
public class MonRow05
{
    List<int[]> rowList = new List<int[]>();
    public MonRow05()
    {
        rowList.Add(new int[3] { 0, 0, 1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { -1, -1, -1 });
        rowList.Add(new int[3] { 0, 0, 1 });
        rowList.Add(new int[3] { 0, 0, 1 });
        rowList.Add(new int[3] { 1, 1, 1 });
    }
    public int[] rowSelect()
    {
        return rowList[globalVar.inst.selectStage];
    }
}