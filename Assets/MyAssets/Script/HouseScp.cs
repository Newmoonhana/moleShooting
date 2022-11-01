using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScp : MonoBehaviour
{
    public Animator house_ani;

    public void Awake()
    {
        house_ani = this.GetComponent<Animator>();
        StartCoroutine("UpdateCor");
    }

    IEnumerator UpdateCor()
    {
        if (house_ani.GetBool("Smash"))
        {
            while (!house_ani.GetCurrentAnimatorStateInfo(0).IsName("House_Smash"))
                yield return null;
            while (house_ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                yield return null;
            house_ani.SetBool("Smash", false);
        }
        yield return null;
        StartCoroutine("UpdateCor");
    }
}
