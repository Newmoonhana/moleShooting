using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    float span = 1.0f;
    float delta = 0;
    public float time = 60;
    Slider SlTime;
    void Awake()
    {
        SlTime = GetComponent<Slider>();
        SlTime.value = 0;
        SlTime.maxValue = time;
        delta = -3.5f;
    }

    bool timerEnd = false;
    void Update()
    {
        this.delta += Time.deltaTime;
        if (delta > span)
        {
            if (SlTime.value < time)
                SlTime.value += span;
            else
            {
                GameManager.inst.mouseM.ClickSound = false;
                globalVar.inst.selectClear = true;
                StartCoroutine(GameManager.inst.StageM.GameOver());
                return;
            }

            if (SlTime.value > time - 5 && timerEnd == false)
            {
                GetComponent<AudioSource>().Play();
                if (SlTime.value == time)
                    timerEnd = true;
            }

            delta = 0;
        }
    }

}