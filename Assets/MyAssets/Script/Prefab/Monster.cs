using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int shootNum = 1;
    public float shootDelay;
    public MonsterClass monClass;
    public Parten monPart;
    public GameObject anim;
    SphereCollider monsterCollider;
    bool shootBul;

    int ani_trigers;


    void Awake()
    {
        monsterCollider = GetComponent<SphereCollider>();
        anim = this.transform.GetChild(0).gameObject;
        StartCoroutine(CheckAnimationState());
    }

    IEnumerator CheckAnimationState()
    {

        while (!anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("mole"))
        {
            // anim.GetComponent<Animator>().SetBool("Bool_idle", false);
            //전환 중일 때 실행되는 부분
            yield return null;
        }

        while (anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0)
        .normalizedTime < 0.8f)
        {
            // anim.GetComponent<Animator>().SetBool("Bool_idle", false);
            //애니메이션 재생 중 실행되는 부분
            yield return null;
        }
        anim.GetComponent<Animator>().SetBool("Bool_idle", true);
        //애니메이션 완료 후 실행되는 부분

    }

    void Start()
    {
        shootBul = true;
        //anim.GetComponent<Animator>().SetBool("Bool_idle", false);
        //anim.GetComponent<Animator>().SetBool("Bool", false);//지금 mole이 무시되고 있는 것을 확인
    }
    //void Update()
    //{

         


    //   //// AnimatorStateInfo animInfo = anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

    //    //if (!anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("mole"))  //0 = base layer
    //    //anim.GetComponent<Animator>().SetBool("Bool_idle", true);

    //    //// if (animInfo.normalizedTime <  0.09f)  anim.GetComponent<Animator>().SetBool("Bool_idle", true);
        
    //    //if ( anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("mole") == false)
    //    //anim.GetComponent<Animator>().SetBool("Bool_idle", true);

    //} 

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack" && anim.GetComponent<Animator>().GetBool("Bool_idle") == true)
        {
            shootBul = false;
            monsterCollider.enabled = false;
            if (gameObject.tag == "Angel")
            {
                GameManager.inst.House_HPscp.InDecreaseHP(-monClass.mon_power);
                GameManager.inst.SoundM.Play(GameManager.inst.SoundM.ClickPlayer, GameManager.inst.SoundM.AngelAudio);
            }
            else
            {
                GameManager.inst.ScoreM.countPluse();  
                GameManager.inst.SoundM.Play(GameManager.inst.SoundM.ClickPlayer, GameManager.inst.SoundM.AttackAudio);
            }
             //애니메이션
            StartCoroutine("ThisDead");
            //shootBul = true;
        }
        
    }

    IEnumerator ThisDead()
    {
        anim.GetComponent<Animator>().SetBool("IsDead", true);
        while (!anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("mole_dead"))
            yield return null;
        while (anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
        GameManager.inst.SoundM.Relay(GameManager.inst.SoundM.ClickPlayer, GameManager.inst.SoundM.ClickAudio);

        Destroy(gameObject);
    }

    public IEnumerator BulletShoot()
    {
        yield return monPart.startTime;


        for (int i = 0; i < shootNum; i++)
        {
            //ani
            if (!anim.GetComponent<Animator>().GetBool("Bool"))
                if(shootBul)
                    ShootControl();
            //
            yield return new WaitForSeconds(shootDelay);

        }
        yield return monPart.endTime;
        //애니메이션
        anim.GetComponent<Animator>().SetBool("Bool", true);

        while (!anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("mole_end"))
            yield return null;
        while (anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
        Destroy(gameObject);
    }

    void ShootControl()
    {
        GameObject bullet = Instantiate(monClass.bul_obj, transform.position, monClass.bul_obj.transform.rotation);
        bullet.transform.SetParent(GameManager.inst.Bullet_obj.transform);
        Bullet bulletScp = bullet.GetComponent<Bullet>();
        bulletScp.monClass = monClass;
    }
}