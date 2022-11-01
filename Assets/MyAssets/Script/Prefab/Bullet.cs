using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bullet : MonoBehaviour
{
    public float moveSpeed = 5f;
    public MonsterClass monClass;
    Vector3 playerXY;
    Vector3 BulXY;
    void Awake()
    {
        playerXY = GameManager.inst.playerM.player_obj.transform.position;
        BulXY = gameObject.transform.position;
        StartCoroutine(UpdateCor());
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Shild")
        {
            GameManager.inst.playerM.shild_scp.SCB();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "House")
        {
            if (monClass.mon_power < 10)
                GameManager.inst.SoundM.Play(GameManager.inst.SoundM.HousePlayer, GameManager.inst.SoundM.HouseAudio);
            else
                GameManager.inst.SoundM.Play(GameManager.inst.SoundM.HousePlayer, GameManager.inst.SoundM.HouseHealAudio);
            GameManager.inst.House_HPscp.InDecreaseHP(monClass.mon_power);
            Destroy(gameObject);
        }
    }

    IEnumerator UpdateCor()
    {
        Vector3 pos = transform.position;
        monClass.mon_bulM.BulMove(this.gameObject, playerXY, BulXY);
        if (pos.x < -50 || pos.x > 1330 || pos.y < -50 || pos.y > 770)
        {
            Destroy(gameObject);
        }
        yield return null;
        StartCoroutine(UpdateCor());
    }
}
