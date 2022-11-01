using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class globalVar : MonoBehaviour
{
    public static globalVar inst;
    public int selectStage = 0;
    public bool selectClear = false;
    public InputField stageOpen;
    public bool reset = false;

    void Awake()
    {
        if (inst != null)   //싱글톤.
        {
            Destroy(gameObject);
            return;
        }
        inst= this;
        DontDestroyOnLoad(this);    //씬 이동 후 오브젝트 보존.

        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Open", int.Parse(stageOpen.text));
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("Open"))
        {
            ResetPP();
            return;
        }
        stageOpen.text = PlayerPrefs.GetInt("Open").ToString();
    }

    //모든 값 리셋.
    public void ResetPP()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Open", 0);
        PlayerPrefs.SetInt("MyScore", 0);
        PlayerPrefs.SetFloat("MyLifePoint", 0);
        PlayerPrefs.SetInt("HighScore", 0);

        PlayerPrefs.SetInt("CutScene00", 0);

        stageOpen.text = PlayerPrefs.GetInt("Open").ToString();
        reset = false;
    }
}
