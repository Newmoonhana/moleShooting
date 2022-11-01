using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BackgroundPlayer;
    public AudioClip InGameAudio;
    public AudioClip MainStaAudio;
    public AudioClip CutAudio;
    public AudioClip GameStart_me;
    public AudioClip GameOver_me;
    public AudioClip GameClear_me;

    public AudioSource ClickPlayer;
    public AudioClip ClickAudio;
    public AudioClip AttackAudio;
    public AudioClip AngelAudio;
    public AudioClip NotAttackAudio;


    public AudioSource BasketPalyer;
    public AudioClip BasketAudio;
    public AudioSource HousePlayer;
    public AudioClip HouseAudio;
    public AudioClip HouseHealAudio;

    //public GameObject MAManager;

    private void Awake()
    {
        Transform gloSound = GameObject.Find("globalVar").transform.GetChild(1);
        BackgroundPlayer = gloSound.GetChild(0).GetComponent<AudioSource>();
        ClickPlayer = gloSound.GetChild(1).GetComponent<AudioSource>();
        BasketPalyer = gloSound.GetChild(2).GetComponent<AudioSource>();
        HousePlayer = gloSound.GetChild(3).GetComponent<AudioSource>();
    }

    void Start()
    {

        //MAManager = GameObject.Find("MainGame_Manager");
        //MAManager.SetActive(false);
    }
    public void Play(AudioSource soure, AudioClip clip)
    {
        if(soure == GameManager.inst.SoundM.BackgroundPlayer)
        {
            if (soure.clip != clip)
            {
                soure.clip = clip;
                soure.Play();
            }
        }
        else
        {
            soure.clip = clip;
            soure.Play();
        }
    }
    public void Pause(AudioSource soure)
    {
        soure.Pause();
    }
    public void Relay(AudioSource soure, AudioClip clip)
    {
        soure.clip = clip;
    }
}
