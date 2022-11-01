using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready_Manager : MonoBehaviour
{
    public Animator UI;
     

    void Awake()
    {
        UI =   GetComponent<Animator>();
    }

    public void GameRun()
    {
       
        StartCoroutine("UpdateCor");
    }
   

    IEnumerator UpdateCor()
    {


        if ( UI.GetCurrentAnimatorStateInfo(0).IsName("Ready_fade"))
        {
           
            Time.timeScale = 0;
           
        }
        else
        {
            Time.timeScale = 1;
         
        }
      
        yield return null;
        StartCoroutine("UpdateCor");
    }

   
  
  


}