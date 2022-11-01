using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainGame_Manager : MonoBehaviour
    
{
    /*
    private static MainGame_Manager _instance;

    public static MainGame_Manager Instance
    {
        get
        {
            if (_instance = null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
;            }
            return _instance;
        }
    }
    public int score;
    public bool isDead;
    */

    // public Button button;
    //ㅈㅇㅅ  추가한 것
    public AudioSource ClickPalyer;
    public AudioClip ClickAudio;
    public AudioSource BackgroundPalyer;
    public AudioClip BackgroundAudio;
    public static MainGame_Manager instance;
    public int level;

    public GameObject cutScene;
    //

    public GameObject loadingScreen;
    public Slider Slider;
    public Text progressText;


    //fadeout
    public GameObject fadeanim;

    //fadeout
    //private void Start()
    //{
    //    loadingScreen.SetActive(false);//해당 오브젝트를 비활성시킨다. 활성화 여부 확인하고해

    //}
     
           void fadeToLevel()
    {
        fadeanim.GetComponent<Animator>().SetTrigger("fadeTriger");
    }

    void Awake()
    {
        fadeanim.GetComponent<Animator>().SetTrigger("fadeTriger");
        ClickPalyer.clip = ClickAudio;//ㅈㅇㅅ
        cutScene = GameObject.Find("CutSceneManager");
        Debug.Log(cutScene);
        cutScene.SetActive(false);

        //  _instance = this;
        // DontDestroyOnLoad(gameObject);
        loadingScreen.SetActive(false);//해당 오브젝트를 비활성시킨다. 활성화 여부 확인하고해

    }

    // Start is called before the first frame update
    
    void LoadLevel(string LoadName)
    {

        fadeToLevel();//fade
        StartCoroutine(LoadAsynchronously(LoadName));

        loadingScreen.SetActive(true);//해당 오브젝트를 활성화시킨다. 활성화 여부 확인하고해
    }

    IEnumerator LoadAsynchronously(string LoadName)
    {
        //   AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);//seneIndex번의 방으로 이동.
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
    //  public static MainGame_Manager instance;
    //  public int level;
    // Start is called before the first frame update
   
    //
    public void Stage_1()
    {
        LoadLevel("DebugRoom");
    }
    public void Stage_Quit()
    {
        LoadLevel("Main_Screen");
       
    }
    //위는 스테이지 메뉴
    //아래는 메인메뉴
    public void MainGame_Start()
    {
        LoadLevel("Stage_Screen_2");
      
    }
    public void MainGame_Quit()
    {
        Application.Quit();
    }

}
