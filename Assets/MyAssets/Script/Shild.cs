using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour
{
    public Transform shadow;    //소쿠리 그림자.
    public bool colRunning = false; //충돌 시 회전 코루틴 실행 중인지(false:X, true:O).
    int dir = 4;
    Vector3 vZ03 = new Vector3(0, 0, 2);

    //소쿠리 충돌 시 회전.
    public void SCB()
    {
        if (!colRunning)
            StartCoroutine(ShildColBullet());
    }
    IEnumerator ShildColBullet()
    {
        GameManager.inst.SoundM.Play(GameManager.inst.SoundM.BasketPalyer, GameManager.inst.SoundM.BasketAudio);
        colRunning = true;
        //회전1.
        for (float i = this.transform.rotation.z; i < dir; i++)
        {
            this.transform.Rotate(vZ03, Space.Self);
            shadow.Rotate(vZ03, Space.Self);
            if (this.transform.rotation.z > dir)
                break;
            yield return null;
        }
        //회전2(반대방향).
        for (float i = this.transform.rotation.z; i > -dir * 2; i--)
        {
            this.transform.Rotate(-vZ03, Space.Self);
            shadow.Rotate(-vZ03, Space.Self);
            if (this.transform.rotation.z < -dir)
                break;
            yield return null;
        }
        //회전3(제자리).
        for (float i = this.transform.rotation.z; i < dir; i++)
        {
            this.transform.Rotate(vZ03, Space.Self);
            shadow.Rotate(vZ03, Space.Self);
            if (this.transform.rotation.z > 0)
                break;
            yield return null;
        }
        //값 정확하게 설정.
        Quaternion tem = this.transform.rotation;
        tem.z = 0;
        this.transform.rotation = tem;
        shadow.transform.rotation = tem;

        colRunning = false;
    }
}
