using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Re : MonoBehaviour
{
    public void Res()
    {
        SceneManager.LoadScene("Main_Screen");
    }
    public void Ending()
    {
        SceneManager.LoadScene("Ending_Screen");
    }
    // Update is called once per frame
     
}
