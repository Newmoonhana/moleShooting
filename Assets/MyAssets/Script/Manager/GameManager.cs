using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst; //싱글톤 용 변수(GameManager.inst.(변수명)으로 다른 스크립트에서도 아래 변수 사용 가능).

    //스크립트.
    public MouseManager mouseM;
    public SoundManager SoundM;
    public Stage_Manager StageM;

    //스크립트(게임 플레이 씬).
    public CameraManager cameraM;
    public PauseManager pauseM;
    public MapManager mapM;
    public MonsterManager monM;
    public PlayerManager playerM;
    public Score_Manager ScoreM;

    //전역 변수.
    public GameObject fade_obj;
    public GameObject loadingScreen;

    //전역 변수(게임 플레이 씬).
    public GameObject GameStage_obj;    //게임이 돌아갈 스테이지 오브젝트.
    public GameObject Bullet_obj;   //탄환 오브젝트 모음.
    public Hp_player House_HPscp;
    public int defaultSprSize;  //스프라이트들의 기본 스케일값.
    public Animator gameoverTitle_ani;

    //WaitForSeconds
    public WaitForSeconds waits00_10 = new WaitForSeconds(00.10f);
    public WaitForSeconds waits00_50 = new WaitForSeconds(00.50f);
    public WaitForSeconds waits01_00 = new WaitForSeconds(01.00f);
    public WaitForSeconds waits01_50 = new WaitForSeconds(01.50f);
    public WaitForSeconds waits02_00 = new WaitForSeconds(02.00f);
    public WaitForSeconds waits02_50 = new WaitForSeconds(02.50f);
    public WaitForSeconds waits03_00 = new WaitForSeconds(03.00f);

    void Awake()
    {
        inst = this;
        //게임 시작.
        fade_obj.SetActive(true);

        if (SceneManager.GetActiveScene().name == "Main_Screen")
            MainRoom();
        else if (SceneManager.GetActiveScene().name == "Stage_Screen_2")
            StageRoom();
        else if (SceneManager.GetActiveScene().name == "DebugRoom")
            StartCoroutine("GameRun");
    }

    void MainRoom()
    {
        SoundM.ClickPlayer.clip = SoundM.ClickAudio;
        if (SoundM.BackgroundPlayer != null)
            SoundM.Play(SoundM.BackgroundPlayer, SoundM.MainStaAudio);
        mouseM.GameRun();
    }

    void StageRoom()
    {
        SoundM.ClickPlayer.clip = SoundM.ClickAudio;
        if (SoundM.BackgroundPlayer != null)
            SoundM.Play(SoundM.BackgroundPlayer, SoundM.MainStaAudio);
        mouseM.GameRun();

        if (PlayerPrefs.GetInt("CutScene00", 0) == 0)
        {
            PlayerPrefs.SetInt("CutScene00", 1);
            StageM.CutScene();
        }
    }

    public WaitForSeconds stageWait;
    public void SelectStageWait()
    {
        List<WaitForSeconds> sws = new List<WaitForSeconds>();
        sws.Insert(0, waits03_00);
        sws.Insert(1, waits02_00);
        sws.Insert(2, waits02_00);
        sws.Insert(3, waits01_50);
        sws.Insert(4, waits01_00);
        sws.Insert(5, waits01_50);
        sws.Insert(6, waits01_00);
        sws.Insert(7, waits01_00);
        sws.Insert(8, waits00_50);
        stageWait = sws[globalVar.inst.selectStage];
    }
    IEnumerator GameRun()
    {
        yield return null;
        SoundM.BackgroundPlayer.Stop();
        yield return waits00_50;

        SelectStageWait();

        SoundM.Play(GameManager.inst.SoundM.BackgroundPlayer, GameManager.inst.SoundM.GameStart_me);
        SoundM.BackgroundPlayer.loop = false;
        yield return waits01_00;

        if (!GameObject.Find("Ready").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Ready_fade"))
            yield return null;
        while (GameObject.Find("Ready").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
        yield return waits00_50;

        SoundM.Play(SoundM.BackgroundPlayer, SoundM.InGameAudio);
        SoundM.BackgroundPlayer.loop = true;
        pauseM.GameRun();
        mouseM.GameRun();
        playerM.GameRun();

        yield return waits03_00;

        mapM.GameRun();
    }
    public IEnumerator GameOverRun()
    {
        for (int i = 0; i < monM.Monster_obj.transform.childCount; i++)
            Destroy(monM.Monster_obj.transform.GetChild(i).gameObject);
        for (int i = 0; i < Bullet_obj.transform.childCount; i++)
            Destroy(Bullet_obj.transform.GetChild(i).gameObject);

        //StopCoroutine(pauseM.UpdateCor());
        //StopCoroutine(mouseM.UpdateCor());
        //StopCoroutine(playerM.UpdateCor());
        //StopCoroutine(mapM.UpdateCor());
        pauseM.gameObject.SetActive(false);
        playerM.gameObject.SetActive(false);
        mouseM.gameObject.SetActive(false);
        mapM.gameObject.SetActive(false);

        Animator ready_ani = GameObject.Find("Ready").GetComponent<Animator>();
        AudioClip temp;
        string name = "";
        if (!globalVar.inst.selectClear)
        {
            name = "GameOverTitle";
            ready_ani.SetInteger("GameOver", 1);
            temp = GameManager.inst.SoundM.GameOver_me;
        }
        else
        {
            name = "GameClearTitle";
            ready_ani.SetInteger("GameOver", 2);
            temp = GameManager.inst.SoundM.GameClear_me;
        }

        yield return null;
        //SoundM.BackgroundPlayer.Stop();
        //yield return waits00_50;
        SoundM.Play(GameManager.inst.SoundM.BackgroundPlayer, temp);
        SoundM.BackgroundPlayer.loop = false;

        if (!ready_ani.GetCurrentAnimatorStateInfo(0).IsName(name))
            yield return null;
        while (ready_ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
        yield return waits02_00;
        SoundM.BackgroundPlayer.Stop();
        yield return null;
        SoundM.BackgroundPlayer.loop = true;
    }
}
