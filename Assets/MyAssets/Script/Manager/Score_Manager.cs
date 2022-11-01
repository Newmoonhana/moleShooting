using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    //전역 변수. 
    public int count = 0;

    public Text countText;
    

    public void countPluse()
    {
        count += 100;
    }

    void Start()
    {

        countText.text =    count+"점!";
    }



    void Update()
    {
        countText.text = count + "점!";

    }
    /*
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Missile"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            countText.text = "Count: " + count;
        }
    }*/
}

