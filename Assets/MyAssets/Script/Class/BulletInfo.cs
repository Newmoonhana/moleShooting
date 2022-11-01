using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class BulM
{
    int bulletID;
    float movespeed;
    GameObject bullet_obj;
    float BulletPosition;

    public BulM(int bulID, float MSpeed)
    {
        // playerXY = GameObject.Find("Player_obj");
        bulletID = bulID;
        movespeed = MSpeed;
    }
    public void BulMove(GameObject bulObj, Vector3 playerXY, Vector3 BulXY)
    {
        switch (bulletID)
        {
            case 0:
                BulMove00(bulObj);
                break;
            case 1:
                BulMove01(bulObj, playerXY, BulXY);
                break;
            case 2:
                BulMove02(bulObj);
                break;
        }
    }
    void BulMove00(GameObject bulObj)
    {
        float MoveX = movespeed * Time.deltaTime;
        bulObj.transform.Translate(-MoveX, 0, 0);
        //Vector3 roll = Vector3.zero;
        //roll.z += movespeed * Time.deltaTime;
        //bulObj.transform.Rotate(roll);
        //Debug.Log(bulObj.transform.rotation);
    }

    void BulMove01(GameObject bulObj, Vector3 playerXY, Vector3 BulXY)
    {
        Vector3 angle = (playerXY - BulXY).normalized;
        float MoveX = movespeed * Time.deltaTime;
        float MoveY = angle.y * movespeed * Time.deltaTime;

        bulObj.transform.Translate(new Vector3(-MoveX, MoveY));
    }

    void BulMove02(GameObject bulObj)
    {
        float MoveX = movespeed * Time.deltaTime;
        bulObj.transform.Translate(-MoveX, 0, 0);
        Vector3 roll = Vector3.zero;

        int rand = Random.Range(0, 2);
        if (rand == 0)
            roll.z = bulObj.transform.rotation.z + movespeed * Time.deltaTime;
        else if(rand == 1)
            roll.z = bulObj.transform.rotation.z - movespeed * Time.deltaTime;

        //if (bulObj.transform.position.y >= 450)
        //    roll.z = bulObj.transform.rotation.z + movespeed * Time.deltaTime * 2;
        //else if (bulObj.transform.position.y <= 150)
        //    roll.z = bulObj.transform.rotation.z - movespeed * Time.deltaTime * 2;

        bulObj.transform.Rotate(roll);
    }
}
