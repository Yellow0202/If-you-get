using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using UnityEngine.SceneManagement;
using Cargold.Infinite;

public class StatusSystem_Manager : SerializedMonoBehaviour, GameSystem_Manager.IInitializer
{
    public static StatusSystem_Manager Instance;

    [LabelText("다음 주 포함될 곱")] private float _mextWeekDayMulti = 1.0f;
    [LabelText("이번 주 포함될 곱")] private float _curWeekDayMulti = 1.0f;
    [LabelText("피로도에 의한 스테이터스 감소")] private float _statusDeley = 0.5f;
    [LabelText("피로도 증가 여부")] private bool is_Stressss;

    public void Init_Func(int _layer)
    {
        if(_layer == 0)
        {
            Instance = this;
        }
        else if(_layer == 1)
        {

        }
        else if(_layer == 2)
        {

        }
    }

    public void Set_NextWeekDayMultiValue_Func(float a_Multi)
    {
        this._mextWeekDayMulti += a_Multi;
    }

    public void Start_Schedule_Func()
    {
        this.Get_CurWeekDayMultiValue_Func();
        this.Get_CurStress_Func();
    }

    #region 값 입력

    private void Get_CurWeekDayMultiValue_Func()
    {
        this._curWeekDayMulti = this._mextWeekDayMulti;
        this._mextWeekDayMulti = 1.0f;
    }

    private void Get_CurStress_Func()
    {
        if (100 <= UserSystem_Manager.Instance.status.Get_UserStatus_Func().stress)
            this.is_Stressss = true;
        else
            this.is_Stressss = false;
    }

    public void Set_BackStrPlus_Func(int a_Value)
    {
        float a_Total = 0.0f;

        if (0 < a_Value)
        {
            a_Total = a_Value * this._curWeekDayMulti;

            if (this.is_Stressss == true)
                a_Total *= this._statusDeley;
        }
        else if(a_Value < 0)
        {
            a_Total = a_Value;
        }

        ScheduleSystem_Manager.Instance.plusStatus.backStr += (int)a_Total;
    }

    public void Set_ChestStrPlus_Func(int a_Value)
    {
        float a_Total = 0.0f;

        if (0 < a_Value)
        {
            a_Total = a_Value * this._curWeekDayMulti;

            if (this.is_Stressss == true)
                a_Total *= this._statusDeley;
        }
        else if (a_Value < 0)
        {
            a_Total = a_Value;
        }
        ScheduleSystem_Manager.Instance.plusStatus.chestStr += (int)a_Total;
    }

    public void Set_LowerbodyStrPlus_Func(int a_Value)
    {
        float a_Total = 0.0f;

        if (0 < a_Value)
        {
            a_Total = a_Value * this._curWeekDayMulti;

            if (this.is_Stressss == true)
                a_Total *= this._statusDeley;
        }
        else if (a_Value < 0)
        {
            a_Total = a_Value;
        }

        ScheduleSystem_Manager.Instance.plusStatus.lowerbodyStr += (int)a_Total;
    }

    public void Set_MentalCountPlus_Func(int a_Value)
    {
        ScheduleSystem_Manager.Instance.plusStatus.mentalCount += a_Value;
    }

    public void Set_BusinessGoldPlus_Func(int a_Value)
    {
        float a_Total = a_Value;

        if (this.is_Stressss == true)
            a_Total *= this._statusDeley;


        ScheduleSystem_Manager.Instance.plusStatus.businessGold += (int)a_Total;
    }

    public void Set_EventGoldPlus_Func(int a_Value)
    {
        float a_Total = a_Value;

        if (this.is_Stressss == true)
            a_Total *= this._statusDeley;


        ScheduleSystem_Manager.Instance.plusStatus.eventCost += (int)a_Total;
    }

    public void Set_BreakGoldPlus_Func(int a_Value)
    {
        float a_Total = a_Value;

        if (this.is_Stressss == true)
            a_Total *= this._statusDeley;


        ScheduleSystem_Manager.Instance.plusStatus.breakCost += (int)a_Total;
    }

    public void Set_StressPlus_Func(int a_Value)
    {
        ScheduleSystem_Manager.Instance.plusStatus.stress += a_Value;
    }

    #endregion

    public void StatusPlus_Func(EventSel_InfoData a_EventSelPInfoData)
    {
        if (a_EventSelPInfoData.Is_Dual == false)
        {
            this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_1, a_EventSelPInfoData.Status_ValueType_1, a_EventSelPInfoData.Status_Change_Value_1);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if(i == 0)
                {
                    if(a_EventSelPInfoData.StatusType_1 != StatusType.Done)
                    {
                        this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_2, a_EventSelPInfoData.Status_ValueType_2, a_EventSelPInfoData.Status_Change_Value_2);
                    }
                }
                else if(i == 1)
                {
                    if (a_EventSelPInfoData.StatusType_2 != StatusType.Done)
                    {
                        this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_1, a_EventSelPInfoData.Status_ValueType_1, a_EventSelPInfoData.Status_Change_Value_1);
                    }
                }
                else if (i == 2)
                {
                    if (a_EventSelPInfoData.StatusType_3 != StatusType.Done)
                    {
                        this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_3, a_EventSelPInfoData.Status_ValueType_3, a_EventSelPInfoData.Status_Change_Value_3);
                    }
                }
                else if (i == 3)
                {
                    if (a_EventSelPInfoData.StatusType_4 != StatusType.Done)
                    {
                        this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_4, a_EventSelPInfoData.Status_ValueType_4, a_EventSelPInfoData.Status_Change_Value_4);
                    }
                }
            }
        }
    }

    public void StatusPlus_Func(EventSelP_InfoData a_EventSelPInfoData)
    {
        if (a_EventSelPInfoData.Is_Dual == false)
        {
            this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_1, a_EventSelPInfoData.Status_ValueType_1, a_EventSelPInfoData.Status_Change_Value_1);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    if (a_EventSelPInfoData.StatusType_1 != StatusType.Done)
                    {
                        this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_2, a_EventSelPInfoData.Status_ValueType_2, a_EventSelPInfoData.Status_Change_Value_2);
                    }
                }
                else if (i == 1)
                {
                    if (a_EventSelPInfoData.StatusType_2 != StatusType.Done)
                    {
                        this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_1, a_EventSelPInfoData.Status_ValueType_1, a_EventSelPInfoData.Status_Change_Value_1);
                    }
                }
                else if (i == 2)
                {
                    if (a_EventSelPInfoData.StatusType_3 != StatusType.Done)
                    {
                        this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_3, a_EventSelPInfoData.Status_ValueType_3, a_EventSelPInfoData.Status_Change_Value_3);
                    }
                }
                else if (i == 3)
                {
                    if (a_EventSelPInfoData.StatusType_4 != StatusType.Done)
                    {
                        this.StatusTypeToSetValue_Func(a_EventSelPInfoData.StatusType_4, a_EventSelPInfoData.Status_ValueType_4, a_EventSelPInfoData.Status_Change_Value_4);
                    }
                }
            }
        }
    }

    private void StatusTypeToSetValue_Func(StatusType a_Type, StatusValueType a_ValueType, float a_Value)
    {
        switch (a_Type)
        {
            case StatusType.BackMovementStr:
                if(a_ValueType == StatusValueType.Single)
                {
                    this.Set_BackStrPlus_Func((int)a_Value);
                }
                else if(a_ValueType == StatusValueType.Multi)
                {
                    this.Set_NextWeekDayMultiValue_Func((int)a_Value);
                }
                break;

            case StatusType.ChestExercisesStr:
                if (a_ValueType == StatusValueType.Single)
                {
                    this.Set_ChestStrPlus_Func((int)a_Value);
                }
                else if (a_ValueType == StatusValueType.Multi)
                {
                    this.Set_NextWeekDayMultiValue_Func((int)a_Value);
                }
                break;

            case StatusType.LowerBodyExercisesStr:
                if (a_ValueType == StatusValueType.Single)
                {
                    this.Set_LowerbodyStrPlus_Func((int)a_Value);
                }
                else if (a_ValueType == StatusValueType.Multi)
                {
                    this.Set_NextWeekDayMultiValue_Func((int)a_Value);
                }
                break;

            case StatusType.Mentality:
                if (a_ValueType == StatusValueType.Single)
                {
                    this.Set_MentalCountPlus_Func((int)a_Value);
                }
                break;

            case StatusType.Stress:
                if (a_ValueType == StatusValueType.Single)
                {
                    this.Set_StressPlus_Func((int)a_Value);
                }
                break;

            case StatusType.Money:
                if (a_ValueType == StatusValueType.Single)
                {
                    this.Set_EventGoldPlus_Func((int)a_Value * -1);
                }
                break;
        }
    }
}
