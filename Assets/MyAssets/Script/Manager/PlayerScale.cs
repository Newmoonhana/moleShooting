using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScale : MonoBehaviour
{
    GameObject Player_spr;
    Vector3 playerScale;
    bool p_SC = false;

    void Start()
    {
        Player_spr = GameObject.Find("Player_spr");
    }
    
    void Update()
    {
        if (p_SC)
        {
            Player_spr.transform.localScale += new Vector3(0.02f, 0.02f, 0.0f);
            playerScale = Player_spr.transform.localScale;
            if (playerScale.x >= 26)
                p_SC = false;
        }
        else
        {
            Player_spr.transform.localScale -= new Vector3(0.02f, 0.02f, 0.0f);
            playerScale = Player_spr.transform.localScale;
            if (playerScale.x <= 25)
                p_SC = true;
        }
    }
}
