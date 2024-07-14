using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class AnimationCharacAnim_Script : MonoBehaviour
{
    public static AnimationCharacAnim_Script Instance;

    [SerializeField, LabelText("등운동 애니메이션")] private Animator _backAnim;
    [SerializeField, LabelText("가슴운동 애니메이션")] private Animator _chestAnim;
    [SerializeField, LabelText("하체운동 애니메이션")] private Animator _lowerAnim;

    private void Awake()
    {
        Instance = this;
    }

    public void Close_GameObject_Func(ScheduleType a_BgType)
    { 
        switch(a_BgType)
        {
            case ScheduleType.BackMovement:
                this._backAnim.gameObject.SetActive(true);
                this._chestAnim.gameObject.SetActive(false);
                this._lowerAnim.gameObject.SetActive(false);
                break;

            case ScheduleType.ChestExercises:
                this._backAnim.gameObject.SetActive(false);
                this._chestAnim.gameObject.SetActive(true);
                this._lowerAnim.gameObject.SetActive(false);
                break;

            case ScheduleType.LowerBodyExercises:
                this._backAnim.gameObject.SetActive(false);
                this._chestAnim.gameObject.SetActive(false);
                this._lowerAnim.gameObject.SetActive(true);
                break;
        }
    }

    public void Set_BackAnim_Func(float a_Value)
    {
        this._backAnim.SetFloat("Gage", a_Value);
    }

    public void Set_ChestAnim_Func(float a_Value)
    {
        this._chestAnim.SetFloat("Gage", a_Value);
    }

    public void Set_LowerAnim_Func(float a_Value)
    {
        this._lowerAnim.SetFloat("Gage", a_Value);
    }
}
