using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    List<GameObject> startButton_List = new List<GameObject>();
    public List<Sprite> open;
    public Sprite close;

    void Awake()
    {
        startButton_List.Clear();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject temp = this.transform.GetChild(i).gameObject;
            startButton_List.Add(temp);
            if (i <= int.Parse(globalVar.inst.stageOpen.text))
                temp.GetComponent<Image>().sprite = open[i];
            else
                temp.GetComponent<Image>().sprite = close;
        }
    }
}
