using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject hitBefore_obj = null;    //전에 클릭한 오브젝트.
    public GameObject hit_obj = null;   //클릭한 오브젝트.
    public int ispush = -1;  //처리 된 마우스 입력 여부(-1: null, 0: 다운, 1:클릭, 2: 업).
    public int douClick = 1;   //더블클릭까지의 클릭 여부(1: 클릭 X(한번 클릭), n: n번째 클릭).
    public bool dontClick = false;  //클릭 금지 상태(false:꺼짐, true: 켜짐).
    public bool ClickSound;

    public void GameRun()
    {
        StartCoroutine("UpdateCor");
    }

    public IEnumerator UpdateCor()
    {
        hit_obj = null;
        if (dontClick == false)
        {
            if (ispush != 1)
                ispush = -1;
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                TouchDown();
                Touch();
                TouchUp();
            }
#else
            MouseDown();
            Mouse();
            MouseUp();
#endif
        }
        yield return null;
        StartCoroutine("UpdateCor");
    }

#if UNITY_ANDROID
    void TouchDown()
    {
        for (int i = 0; i < Input.touchCount; i++)
        if (Input.GetTouch(i).phase == TouchPhase.Began)
        {
            if (ispush != 0)
            {
                if (!ClickSound)
                    GameManager.inst.SoundM.ClickPlayer.Play();
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    hitBefore_obj = hit_obj;
                    hit_obj = hit.transform.gameObject;
                    if (hitBefore_obj == hit_obj && hit_obj != null)
                        douClick += 1;
                    else
                        douClick = 1;
                }
                ispush = 0;
            }
        }
    }

    void Touch()
    {
        for (int i = 0; i < Input.touchCount; i++)
            if (Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary)
        {
            if (ispush == -1)
            {
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    hit_obj = hit.transform.gameObject;
                }
                ispush = 1;
            }
        }
    }

    private void TouchUp()
    {
        for (int i = 0; i < Input.touchCount; i++)
            if (Input.GetTouch(i).phase == TouchPhase.Ended)
        {
            if (ispush != 2)
            {
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    hit_obj = hit.transform.gameObject;
                }
                ispush = 2;
            }
        }
    }
#else

    void MouseDown()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (ispush != 0)
            {
                if (!ClickSound)
                    GameManager.inst.SoundM.ClickPlayer.Play();
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    hitBefore_obj = hit_obj;
                    hit_obj = hit.transform.gameObject;
                    if (hitBefore_obj == hit_obj && hit_obj != null)
                        douClick += 1;
                    else
                        douClick = 1;
                }
                ispush = 0;
            }
        }
    }

    void Mouse()
    {
        if (Input.GetMouseButton(0) == true)
        {
            if (ispush == -1)
            {
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    hit_obj = hit.transform.gameObject;
                }
                ispush = 1;
            }
        }
    }

    private void MouseUp()
    {
        if (Input.GetMouseButtonUp(0) == true)
        {
            if (ispush != 2)
            {
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    hit_obj = hit.transform.gameObject;
                }
                ispush = 2;
            }
        }
    }
#endif
}
