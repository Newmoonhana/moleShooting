using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    int CutTouch;
    Transform csChild;

    public void StartThis()
    {
        CutTouch = 0;
        csChild = gameObject.transform.GetChild(1);
        csChild.gameObject.SetActive(true);
        StartCoroutine("PlayBG");
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Direction(CutTouch + 1));
    }

    IEnumerator PlayBG()
    {
        yield return null;
        GameManager.inst.SoundM.BackgroundPlayer.Stop();
        yield return null;
        GameManager.inst.SoundM.Play(GameManager.inst.SoundM.BackgroundPlayer, GameManager.inst.SoundM.CutAudio);
        GameManager.inst.SoundM.BackgroundPlayer.loop = true;
    }

    void Update()
    {
        if (GameManager.inst.mouseM.ispush == 0 && dirOn == false)
        {
            if (CutTouch < 7)
                StartCoroutine(Direction(CutTouch + 2));
            CutTouch++;
            SetCut(CutTouch);
        }
    }
    void SetCut(int touch)
    {
        if (touch >= 7)
        {
            GameManager.inst.SoundM.Play(GameManager.inst.SoundM.BackgroundPlayer, GameManager.inst.SoundM.MainStaAudio);

            for (int i = 0; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(false);

            gameObject.SetActive(false);
            GameManager.inst.StageM.canvas.SetActive(true);
            CutTouch = 0;
            csChild = gameObject.transform.GetChild(0);
            csChild.gameObject.SetActive(true);
        }
        else
        {
            csChild = gameObject.transform.GetChild(touch + 1);

            csChild.gameObject.SetActive(true);
        }
        
        if (touch > 1)
            gameObject.transform.GetChild(touch).gameObject.SetActive(false);
    }

    bool dirOn = false;
    //컷 당 연출.
    public AudioClip snakeSE;
    public AudioClip GlassSE;
    public AudioClip RunSE;
    public AudioClip SpeedSE;
    IEnumerator Direction(int cut)
    {
        dirOn = true;

        GameObject mainCamera_obj = GameObject.Find("Main Camera");
        Camera mainCamera = mainCamera_obj.GetComponent<Camera>();
        StopCoroutine("ZoomInOut");
        StopCoroutine("MoveCamera");
        switch (cut)
        {
            case 1:
                yield return StartCoroutine(ZoomInOut(mainCamera, true, 1));
                break;
            case 2:
                mainCamera.orthographicSize = 1;
                mainCamera_obj.transform.position = new Vector3(519, 608, mainCamera_obj.transform.position.z);
                yield return null;
                GameManager.inst.SoundM.HousePlayer.Stop();
                AudioClip temp = GameManager.inst.SoundM.HouseAudio;
                GameManager.inst.SoundM.Play(GameManager.inst.SoundM.HousePlayer, GlassSE);
                StartCoroutine(MoveCamera(mainCamera_obj, 519, 608, 3));
                yield return StartCoroutine(ZoomInOut(mainCamera, true, 5));
                yield return GameManager.inst.waits01_00;
                GameManager.inst.SoundM.HousePlayer.clip = temp;
                break;
            case 3:
                mainCamera_obj.transform.position = new Vector3(1280 + 640, 360, mainCamera_obj.transform.position.z);
                yield return null;
                GameManager.inst.SoundM.HousePlayer.Stop();
                temp = GameManager.inst.SoundM.HouseAudio;
                GameManager.inst.SoundM.Play(GameManager.inst.SoundM.HousePlayer, SpeedSE);
                yield return StartCoroutine(MoveCamera(mainCamera_obj, 1280 + 640, 360, 10));
                yield return GameManager.inst.waits01_00;
                GameManager.inst.SoundM.HousePlayer.clip = temp;
                break;
            case 4:
                mainCamera_obj.transform.position = new Vector3(-640, 360, mainCamera_obj.transform.position.z);
                yield return null;
                GameManager.inst.SoundM.HousePlayer.Stop();
                temp = GameManager.inst.SoundM.HouseAudio;
                GameManager.inst.SoundM.Play(GameManager.inst.SoundM.HousePlayer, RunSE);
                yield return StartCoroutine(MoveCamera(mainCamera_obj, -640, 360, 5));
                yield return GameManager.inst.waits01_00;
                GameManager.inst.SoundM.HousePlayer.clip = temp;
                break;
            case 5:
                mainCamera.orthographicSize = 1;
                yield return null;
                GameManager.inst.SoundM.HousePlayer.Stop();
                temp = GameManager.inst.SoundM.HouseAudio;
                GameManager.inst.SoundM.Play(GameManager.inst.SoundM.HousePlayer, snakeSE);
                yield return StartCoroutine(ZoomInOut(mainCamera, true, 10));
                yield return GameManager.inst.waits01_00;
                GameManager.inst.SoundM.HousePlayer.clip = temp;
                break;
            case 6:
                mainCamera.orthographicSize = 1;
                mainCamera_obj.transform.position = new Vector3(405, 557, mainCamera_obj.transform.position.z);
                yield return null;
                GameManager.inst.SoundM.HousePlayer.Stop();
                temp = GameManager.inst.SoundM.HouseAudio;
                GameManager.inst.SoundM.Play(GameManager.inst.SoundM.HousePlayer, GlassSE);
                StartCoroutine(MoveCamera(mainCamera_obj, 405, 557, 1));
                yield return StartCoroutine(ZoomInOut(mainCamera, true, 1));
                yield return GameManager.inst.waits01_00;
                GameManager.inst.SoundM.HousePlayer.clip = temp;
                break;
        }

        yield return null;
        dirOn = false;
    }
    IEnumerator ZoomInOut(Camera camera, bool InOn, float speed)
    {
        for (float i = 1; i <= 360; i += speed * Time.deltaTime * 100)
        {
            camera.orthographicSize = i;
            yield return null;
        }
        camera.orthographicSize = 360;
    }
    IEnumerator MoveCamera(GameObject camera, int x, int y, float speed)
    {
        Vector3 Pos = Vector3.zero;  //타켓의 좌표.
        Pos.Set(x, y, camera.transform.position.z);
        camera.transform.position = Pos;
        Pos.Set(640, 360, camera.transform.position.z);
        while (camera.transform.position != Pos)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, Pos, speed * Time.deltaTime * 100);
            yield return null;
        }
    }
}
