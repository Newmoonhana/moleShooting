using UnityEngine;

[System.Serializable]

class Mon00 : MonsterClass
{
    int id = 0;
    string name = "Mon00";
    Parten part;
    MonRow00 rowL = new MonRow00();
    int power = -10;
    public BulM bulM;
    public Mon00(GameObject objPre, GameObject bulObj)
    {
        mon_id = id;
        mon_name = name;
        mon_obj = objPre;
        bul_obj = bulObj;
        part = new Parten(0);
        mon_part = part;
        mon_part.row = rowL.rowSelect();
        mon_power = power;
        bulM = new BulM(0, 170);
        mon_bulM = bulM;
    }
}

class Mon01 : MonsterClass
{
    int id = 1;
    string name = "Mon01";
    Parten part;
    MonRow01 rowL = new MonRow01();
    int power = -10;
    public BulM bulM;
    public Mon01(GameObject objPre, GameObject bulObj)
    {
        mon_id = id;
        mon_name = name;
        mon_obj = objPre;
        bul_obj = bulObj;
        part = new Parten(0);
        mon_part = part;
        mon_part.row = rowL.rowSelect();
        mon_power = power;
        bulM = new BulM(0, 300);
        mon_bulM = bulM;
    }
}

class Mon02 : MonsterClass
{
    int id = 2;
    string name = "Mon02";
    Parten part;
    MonRow02 rowL = new MonRow02();
    int power = -10;
    public BulM bulM;
    public Mon02(GameObject objPre, GameObject bulObj)
    {
        mon_id = id;
        mon_name = name;
        mon_obj = objPre;
        bul_obj = bulObj;
        part = new Parten(0);
        mon_part = part;
        mon_part.row = rowL.rowSelect();
        mon_power = power;
        bulM = new BulM(0, 170);
        mon_bulM = bulM;
    }
}

class Mon04 : MonsterClass
{
    int id = 4;
    string name = "Mon04";
    Parten part;
    MonRow04 rowL = new MonRow04();
    int power = 10;
    public BulM bulM;
    public Mon04(GameObject objPre, GameObject bulObj)
    {
        mon_id = id;
        mon_name = name;
        mon_obj = objPre;
        bul_obj = bulObj;
        part = new Parten(0);
        mon_part = part;
        mon_part.row = rowL.rowSelect();
        mon_power = power;
        bulM = new BulM(1, 170);
        mon_bulM = bulM;
    }
}

class Mon05 : MonsterClass
{
    int id = 5;
    string name = "Mon05";
    Parten part;
    MonRow05 rowL = new MonRow05();
    int power = -10;
    public BulM bulM;
    public Mon05(GameObject objPre, GameObject bulObj)
    {
        mon_id = id;
        mon_name = name;
        mon_obj = objPre;
        bul_obj = bulObj;
        part = new Parten(0);
        mon_part = part;
        mon_part.row = rowL.rowSelect();
        mon_power = power;
        bulM = new BulM(2, 170);
        mon_bulM = bulM;
    }
}