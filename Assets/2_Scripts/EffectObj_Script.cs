using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;

public class EffectObj_Script : MonoBehaviour
{
    public static EffectObj_Script Instance;
    public Animator anim;
    public GameObject onoff;

    private bool is_Play;

    private void Awake()
    {
        Instance = this;
    }

    public void Call_Effect_Func()
    {
        if (this.is_Play == true)
            return;

        this.is_Play = true;
        this.onoff.SetActive(true);
        this.anim.Play("Effect_Anim_2");

        Coroutine_C.Invoke_Func(() =>
        {
            this.onoff.SetActive(false);
            this.is_Play = false;
        }, 0.25f);
    }
}
