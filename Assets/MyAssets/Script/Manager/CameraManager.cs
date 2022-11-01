using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject mainCamera_obj;   //메인카메라 오브젝트.
    Camera mainCamera_scp;  //메인카메라의 카메라.
    //Vector3 target_v;   //카메라의 위치.

    void Awake()
    {
        mainCamera_scp = mainCamera_obj.GetComponent<Camera>();
        //target_v = new Vector3(Screen.width / 2, Screen.height / 2, -500);
        //mainCamera_scp.orthographicSize = Screen.height / 2;
        //mainCamera_obj.transform.position = target_v;
    }
}
