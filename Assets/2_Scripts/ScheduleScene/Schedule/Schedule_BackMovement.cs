using Cargold;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule_BackMovement : ScheduleBase
{
    [LabelText("코루틴 변수")] private CoroutineData _corData;

    public override void SchedulStart_Func()
    {
        base.SchedulStart_Func();

        this._corData.StartCoroutine_Func(Schedule_Cor(), CoroutineStartType.StartWhenStop);
    }

    private IEnumerator Schedule_Cor()
    {
        //배경 스프라이트 변경
        UI_Schedule_Script.Instance.Set_BgImgChange_Func(this.myschedulType);

        //스프라이트 고정
        UI_Schedule_Script.Instance.Set_ArrowSpriteChange_Func(HealthType.BackMovement);
        UI_Schedule_Script.Instance.Set_HpBar_Func();
        UI_Schedule_Script.Instance.schedule_CountDownText.gameObject.SetActive(true);

        //카운트
        UI_Schedule_Script.Instance.schedule_CountDownText.text = "3";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        UI_Schedule_Script.Instance.schedule_CountDownText.text = "2";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        UI_Schedule_Script.Instance.schedule_CountDownText.text = "1";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        UI_Schedule_Script.Instance.schedule_CountDownText.text = "0";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        UI_Schedule_Script.Instance.schedule_CountDownText.text = "";

        UI_Schedule_Script.Instance.schedule_CountDownText.gameObject.SetActive(false);

        //게임 시작
        float a_CurDamage = UserSystem_Manager.Instance.status.Get_UserStatus_Func().backMovementSTR / DataBase_Manager.Instance.GetTable_Define.level_PlusAttackDmg;

        while (true)
        {
            //위
            if(Input.GetKeyDown(KeyCode.UpArrow) == true)
            {
                UI_Schedule_Script.Instance.Arrow_InputSpriteChange_Func(true);
                UI_Schedule_Script.Instance.Set_AttackDmg_Func(a_CurDamage);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) == true)
            {
                UI_Schedule_Script.Instance.Arrow_InputSpriteChange_Func(false);
            }

            UI_Schedule_Script.Instance.Update_Health_Func();

            if(CONSTSTRIONG.INT_COUNTMAX <= UI_Schedule_Script.Instance.curCount)
            {
                break;
            }

            if(CONSTSTRIONG.INT_TIMEMAX <= UI_Schedule_Script.Instance.schedule_Time)
            {
                break;
            }

            yield return null;
        }

        //스테이터스 내용 추가

        HealthValunceType a_CurHealthValunce = ScheduleSystem_Manager.Instance.curScheduleData._curHealthValunceArr[ScheduleSystem_Manager.s_curWeekDay.ToInt()];

        float a_TotalStr = 0;

        switch(a_CurHealthValunce)
        {
            case HealthValunceType.Easy:
                StatusSystem_Manager.Instance.Set_StressPlus_Func(DataBase_Manager.Instance.GetTable_Define.level_LowStress);

                a_TotalStr = DataBase_Manager.Instance.GetTable_Define.plus_Low_backMovement / 10;
                a_TotalStr *= UI_Schedule_Script.Instance.curCount;

                StatusSystem_Manager.Instance.Set_BackStrPlus_Func((int)a_TotalStr);
                break;

            case HealthValunceType.Nomal:
                StatusSystem_Manager.Instance.Set_StressPlus_Func(DataBase_Manager.Instance.GetTable_Define.level_MidStress);

                a_TotalStr = DataBase_Manager.Instance.GetTable_Define.plus_Mid_backMovement / 10;
                a_TotalStr *= UI_Schedule_Script.Instance.curCount;

                StatusSystem_Manager.Instance.Set_BackStrPlus_Func((int)a_TotalStr);
                break;

            case HealthValunceType.Hard:
                StatusSystem_Manager.Instance.Set_StressPlus_Func(DataBase_Manager.Instance.GetTable_Define.level_HigtStress);

                a_TotalStr = DataBase_Manager.Instance.GetTable_Define.plus_Higt_backMovement / 10;
                a_TotalStr *= UI_Schedule_Script.Instance.curCount;

                StatusSystem_Manager.Instance.Set_BackStrPlus_Func((int)a_TotalStr);
                break;
        }

        yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);

        UI_Schedule_Script.Instance.WeekDayClear_Func(this, (int)a_TotalStr);

        yield return null;

        this._corData.StopCorountine_Func();
    }
}
