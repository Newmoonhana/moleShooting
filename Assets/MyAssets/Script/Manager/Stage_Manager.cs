using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage_Manager : MonoBehaviour
{
    //전역 변수.
    public Animator fade_ani;
    public Slider Slider;
    public Text progressText;
    bool fadeOn = false;

    public void Ending()
    {
        SceneManager.LoadScene("Ending_Screen");
    }
    public void StageReset()
    {
        globalVar.inst.ResetPP();
        Restart();
    }

    public IEnumerator fadeToLevel()
    {
        int fadeName = fade_ani.GetCurrentAnimatorStateInfo(0).fullPathHash;
        fade_ani.SetTrigger("fadeTriger");
        if ((fade_ani.GetCurrentAnimatorStateInfo(0).fullPathHash == fadeName))
            yield return null;
        while (fade_ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
    }
    public IEnumerator LoadLevel(string LoadName)
    {
        if (!fadeOn)
        {
            fadeOn = true;
            GameManager.inst.mouseM.gameObject.SetActive(false);
            if (GameManager.inst.pauseM != null)
                GameManager.inst.pauseM.Resume();
            yield return StartCoroutine("fadeToLevel");
            GameManager.inst.loadingScreen.SetActive(true);//해당 오브젝트를 활성화시킨다. 활성화 여부 확인하고해
            yield return StartCoroutine(LoadAsynchronously(LoadName));
            fadeOn = false;
        }
    }
    IEnumerator LoadAsynchronously(string LoadName)
    {
        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);//seneIndex번의 방으로 이동.
        //GameManager.inst.SoundM.Pause(GameManager.inst.SoundM.BackgroundPlayer);
        GameManager.inst.SoundM.ClickPlayer.clip = GameManager.inst.SoundM.ClickAudio;
        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadName);//LoadScene
        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            // Debug.Log(operation.progress);//progress는 진행정도를 0부터 1까지로 나타낸다.
            Slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }

    public void Restart()
    {
        //게임 재시작.
        StartCoroutine("LoadLevel", SceneManager.GetActiveScene().name);
    }

    public void Stage(int selectStage)
    {
        if (selectStage - 1 <= int.Parse(globalVar.inst.stageOpen.text))
        {
            globalVar.inst.selectClear = false;
            globalVar.inst.selectStage = selectStage;
            StartCoroutine("LoadLevel", "DebugRoom");
            //GameManager.inst.mouseM.ClickSound = false;
        }
    }

    public void Stage_Quit()
    {
        StartCoroutine("LoadLevel", "Main_Screen");
        
    }
    public IEnumerator GameOver()
    {
        yield return GameManager.inst.StartCoroutine(GameManager.inst.GameOverRun());

        if (globalVar.inst.selectClear)
            if (globalVar.inst.selectStage - 1 == int.Parse(globalVar.inst.stageOpen.text))
            {
                int temp = int.Parse(globalVar.inst.stageOpen.text) + 1;
                globalVar.inst.stageOpen.text = temp.ToString();
                globalVar.inst.Save();
            }
                
        StartCoroutine("LoadLevel", "GameOver_Scene");
    }
    //위는 스테이지 메뉴
    //아래는 메인메뉴
    public void MainGame_Start()
    {
        GameManager.inst.SoundM.Play(GameManager.inst.SoundM.BackgroundPlayer, GameManager.inst.SoundM.MainStaAudio);
        StartCoroutine("LoadLevel", "Stage_Screen_2");
    }

    public void Game_Quit()
    {
        Application.Quit();
    }   
    //컷신
    public GameObject canvas;
    public void CutScene()
    {
        canvas = GameObject.Find("MainCanvas");
        canvas.SetActive(false);
        GameObject.Find("CutSceneManager").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("CutSceneManager").transform.GetChild(0).GetComponent<CutSceneManager>().StartThis();
    }
    public void HowToPlay()
    {
        canvas = GameObject.Find("MainCanvas");
        canvas.SetActive(false);
        GameObject.Find("HowToPlay").transform.GetChild(0).gameObject.SetActive(true);
    }
}
