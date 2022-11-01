using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hp_player : MonoBehaviour
{
    public GameObject HP;
    //public GameObject HP_Timer;
    public float Hp_value = 100;
    public GameObject house;

    public Sprite []house_spr = new Sprite[4];

    
      void Start()
    {
        GameManager.inst.mouseM.ClickSound = true;
        HP.GetComponent<Slider>().maxValue = HP.GetComponent<Slider>().value = Hp_value;
    }



    /*  void Update()
      {
          if (HP_Timer.GetComponent<Slider>().value > 0)
              HP_Timer.GetComponent<Slider>().value -= Time.deltaTime;
          else
              Die();
      }*/

    public void InDecreaseHP(int power)
    {
        /* //HP_Timer;
         if (HP_Timer.GetComponent<Slider>().value <= 100)
             HP_Timer.GetComponent<Slider>().value += power;
         if (HP_Timer.GetComponent<Slider>().value <= 0)
         {
             Die();
         }*/
        //
        //HP
        if (power < 0)
            house.GetComponent<Animator>().SetBool("Smash", true);
        if (HP.GetComponent<Slider>().value <= 100)
            HP.GetComponent<Slider>().value += power;
        if (HP.GetComponent<Slider>().value <= 0)
        {
            Die();
        }

        if (HP.GetComponent<Slider>().value <= 25)
            house.GetComponent<SpriteRenderer>().sprite = house_spr[0];
        else if (HP.GetComponent<Slider>().value <= 50)
            house.GetComponent<SpriteRenderer>().sprite = house_spr[1];
        else if (HP.GetComponent<Slider>().value <= 75)
            house.GetComponent<SpriteRenderer>().sprite = house_spr[2];
        else if (HP.GetComponent<Slider>().value <= 100)
            house.GetComponent<SpriteRenderer>().sprite = house_spr[3];
        //
    }

    void Die()
    {
        //GameManager.inst.SoundM.MAManager.SetActive(true);
        //GameManager.inst.SoundM.MAManager.GetComponent<MainGame_Manager>().BackgroundPalyer.Stop();
        StartCoroutine(GameManager.inst.StageM.GameOver());
        GameManager.inst.mouseM.ClickSound = false;
        //Application.LoadLevel(Application.loadedLevel);
    }
    //public float maxHealth;
    //public float curHealth;
    //public Slider hpSlider;
    //public Hp_player(float max =100 )
    //{
    //    this.maxHealth = max;
    //    this.curHealth = max; 

    //}
    //public void player_Health()
    //{
    //    if (curHealth > maxHealth)
    //    {
    //        curHealth = maxHealth;
    //    }
    //    if (curHealth <= 0)
    //    {
    //        Die();
    //    }
    //    hpSlider.maxValue = maxHealth;
    //    hpSlider.value = curHealth;
    //}




}
