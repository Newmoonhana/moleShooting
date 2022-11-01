using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    Animator title_ani;
    public AudioClip clear_bgs;
    public AudioSource gameover_as;
    public Sprite ClearImg;
    
    void Start()
    {
        if (globalVar.inst.selectClear)
        {
            gameover_as.clip = clear_bgs;
            gameObject.GetComponent<Image>().sprite = ClearImg;

        }
        title_ani = this.gameObject.GetComponent<Animator>();
        if (globalVar.inst.selectClear)
        {//
            //title_ani.SetBool("Clear", true);
        }
    }
}
