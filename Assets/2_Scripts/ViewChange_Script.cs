using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;

public class ViewChange_Script : MonoBehaviour
{
    public static ViewChange_Script Instance;

    [SerializeField] private Animation anim;
    private bool is_On = false;

    private void Awake()
    {
        Instance = this;
    }

    public void BoxOn_Func()
    {
        this.anim.Play("BoxMove_On_Anim");
        this.is_On = true;
    }

    public void BoxOff_Func()
    {
        Coroutine_C.Invoke_Func(() =>
        {
            this.anim.Play("BoxMove_Off_Anim");
        }, 0.75f);
    }
}
