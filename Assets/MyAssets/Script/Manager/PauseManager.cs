using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public GameObject pauseUI;
    public bool paused = false;

    void Start()
    {
        pauseUI.SetActive(false);
    }

    public void GameRun()
    {
        StartCoroutine("UpdateCor");
    }
    public void Pause_button() { paused = !paused; }

    public IEnumerator UpdateCor()
    {
        if (Input.GetButtonDown("Pause"))
        {
            GameManager.inst.SoundM.Play(GameManager.inst.SoundM.ClickPlayer, GameManager.inst.SoundM.ClickAudio);
            paused = !paused;
        }
        if (paused)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            Resume();
        }
        yield return null;
        StartCoroutine("UpdateCor");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        paused = false;
        pauseUI.SetActive(false);
    }
}
