using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player_obj;
    public BoxCollider player_col;
    public GameObject mu_obj;
    public Animator mu_ani;
    public GameObject attack_obj;
    public GameObject attack00_pre;
    public GameObject shild_obj;
    public Shild shild_scp;
    public Animator player_anim;
    //GameManager
    MouseManager mouseM_scp;
    MapManager mapM_scp;
    MonsterManager monM_scp;

    //변수.
    public float player_speed = 10.0f;
    public Joystick joystick;

    void Awake()
    {
        //    player_anim = GetComponent<Animator>();//게임 메니저의 구성요소는 의미없음.
        mouseM_scp = GameManager.inst.mouseM;
        mapM_scp = GameManager.inst.mapM;
        monM_scp = GameManager.inst.monM;
    }

    public void GameRun()
    {
        StartCoroutine("UpdateCor");
    }

    public IEnumerator UpdateCor()
    {
        if (!GameManager.inst.pauseM.paused)
        {
            Player_Move();
            ObjMouseClick();
            StartCoroutine("AniStop");
        }

        yield return null;
        StartCoroutine("UpdateCor");
    }

    //플레이어 이동 함수.
    void Player_Move()
    {
        //Vector3 startVec = player_col.transform.position;
        float time = Time.deltaTime * 60;
        Vector3 vec = Vector3.zero;
        float hor, ver;
#if UNITY_ANDROID
        hor = joystick.Horizontal > 0 ? 1 : -1;
        ver = joystick.Vertical > 0 ? 1 : -1;
        if (joystick.Horizontal == 0)
            hor = 0;
        if (joystick.Vertical == 0)
            ver = 0;
#else
            hor = Input.GetAxisRaw("Horizontal");
            ver = Input.GetAxisRaw("Vertical");
#endif
        vec.Set(hor * player_speed * time,
           ver * player_speed * time, 0);

        //플레이어 애니메이션 관여
        if (hor != 0 || ver != 0)
            player_anim.SetBool("Walk_bool", true);
        else
            player_anim.SetBool("Walk_bool", false);
        //

        Vector3 temppos = player_obj.transform.position;
        temppos = vec + temppos;

        float XMin = 200;
        float XMax = 400;
        float YMin = 90;
        float YMax = 550;
        if (temppos.x < XMin)
            temppos.x = XMin;
        if (temppos.x > XMax)
            temppos.x = XMax;
        if (temppos.y < YMin)
            temppos.y = YMin;
        if (temppos.y > YMax)
            temppos.y = YMax;

        player_obj.transform.position = temppos;

        /*
        if (Input.GetAxisRaw("Horizontal") == 1)
            startVec.x += 80;

        //충돌 여부.
        RaycastHit[] hit = Physics.RaycastAll(startVec, vec, 100 - player_speed);
        for (int i = 0; i < hit.Length; i++)
            if (hit[i].collider != null)
                if (hit[i].transform.parent != null)
                    if (hit[i].transform.parent.name == "Wall")
                    {
                        if (hit[i].transform.gameObject.name == "WallCor_Left" || hit[i].transform.gameObject.name == "WallCor_Right")
                            vec.x = 0;
                        if (hit[i].transform.gameObject.name == "WallCor_Up" || hit[i].transform.gameObject.name == "WallCor_Down")
                            vec.y = 0;
                    }
        
        player_obj.transform.position += vec;
        */
    }

    //마우스 클릭 체크.
    void ObjMouseClick()
    {
        if (mouseM_scp.ispush == 0 && mouseM_scp.dontClick == false)  //왼마우스 다운 & 더블클릭 아닐 때.
        {
            Point clickXY = mapM_scp.TileMouseClick();
            clickXY = mapM_scp.TileMouseClick();
            if (clickXY.x == -1 && clickXY.y == -1)
                return;
            StopCoroutine("Attack01");
            StartCoroutine("Attack01", clickXY);
        }
    }

    //플레이어 공격01.
    bool attackCorRun = false;
    IEnumerator Attack01(Point clickXY)
    {
        attackCorRun = true;
        mu_ani.SetBool("AttackOn", true);
        //워프 모션.
        while (!mu_ani.GetCurrentAnimatorStateInfo(0).IsName("Mu_warp"))
            yield return null;
        while (mu_ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        //---공격.
        yield return StartCoroutine("CreateAttack00", clickXY.x * mapM_scp.TileXY.x + clickXY.y);
        attackCorRun = false;
    }
    IEnumerator CreateAttack00(int index) //공격 오브젝트 생성.
    {
        GameObject attack = Instantiate<GameObject>(attack00_pre);
        Animator attack_ani = attack.transform.GetChild(0).GetComponent<Animator>();

        while (!attack_ani.GetCurrentAnimatorStateInfo(0).IsName("Mu_Attack"))
            yield return null;

        if (monM_scp.MonObj_list[index] == null)
            GameManager.inst.SoundM.Play(GameManager.inst.SoundM.ClickPlayer, GameManager.inst.SoundM.NotAttackAudio);

        attack.transform.SetParent(attack_obj.transform);
        attack.transform.position = mapM_scp.MonTileObj_list[index].transform.position;
        while (attack_ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
        
        Destroy(attack);
    }
    IEnumerator AniStop()
    {
        if (attackCorRun == false)
        {
            mu_ani.SetBool("AttackOn", false);
            //   워프 역 모션.
            while (!mu_ani.GetCurrentAnimatorStateInfo(0).IsName("Mu_warpReverse"))
                yield return null;
            while (mu_ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                yield return null;
        }
    }
}
