using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayManager : MonoBehaviour
{
    static int CutTouch = 0;
    Transform buttonChild;
    Transform csChild;

    void Start()
    {

    }

    void Update()
    {

    }
    
    public void RightButton()
    {
        CutTouch++;

        if(CutTouch >=2)
        {
            buttonChild = GameObject.Find("Button").transform.GetChild(1);
            buttonChild.gameObject.SetActive(false);
            csChild = GameObject.Find("HowToPlayManager").transform.GetChild(1);
            csChild.gameObject.SetActive(false);
            GameObject.Find("HowToPlayManager").gameObject.SetActive(false);
            GameManager.inst.StageM.canvas.SetActive(true);
            CutTouch = 0;
        }
        else
        {
            buttonChild = GameObject.Find("Button").transform.GetChild(1);
            buttonChild.gameObject.SetActive(true);
            csChild = GameObject.Find("HowToPlayManager").transform.GetChild(1);
            csChild.gameObject.SetActive(true);

        }
        
    }
    public void LeftButton()
    {
        CutTouch--;
        Debug.Log(CutTouch);
        buttonChild = GameObject.Find("Button").transform.GetChild(1);
        buttonChild.gameObject.SetActive(false);
        csChild = GameObject.Find("HowToPlayManager").transform.GetChild(0);
        csChild.gameObject.SetActive(true);
        csChild = GameObject.Find("HowToPlayManager").transform.GetChild(1);
        csChild.gameObject.SetActive(false);
    }
}
