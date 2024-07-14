using Cargold;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule_LowerBodyExercises : ScheduleBase
{
    [LabelText("�ڷ�ƾ ����")] private CoroutineData _corData;

    public override void SchedulStart_Func()
    {
        base.SchedulStart_Func();

        this._corData.StartCoroutine_Func(Schedule_Cor(), CoroutineStartType.StartWhenStop);
    }

    private IEnumerator Schedule_Cor()
    {
        yield return Coroutine_C.GetWaitForSeconds_Cor(0.75f);

        //��� ��������Ʈ ����
        UI_Schedule_Script.Instance.Set_BgImgChange_Func(this.myschedulType);

        //��������Ʈ ����
        UI_Schedule_Script.Instance.Set_ArrowSpriteChange_Func(HealthType.LowerBodyExercises);
        UI_Schedule_Script.Instance.Set_HpBar_Func();
        UI_Schedule_Script.Instance.schedule_CountDownText.gameObject.SetActive(true);

        //ī��Ʈ
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

        //���� ����
        float a_CurDamage = UserSystem_Manager.Instance.status.Get_UserStatus_Func().lowerBodyExercisesSTR / DataBase_Manager.Instance.GetTable_Define.level_PlusAttackDmg;

        while (true)
        {
            //�Ʒ�
            if (Input.GetKeyDown(KeyCode.DownArrow) == true)
            {
                UI_Schedule_Script.Instance.Arrow_InputSpriteChange_Func(true);
                UI_Schedule_Script.Instance.Set_AttackDmg_Func(a_CurDamage);
            }

            if (Input.GetKeyUp(KeyCode.DownArrow) == true)
            {
                UI_Schedule_Script.Instance.Arrow_InputSpriteChange_Func(false);
            }

            UI_Schedule_Script.Instance.Update_Health_Func();

            if (CONSTSTRIONG.INT_COUNTMAX <= UI_Schedule_Script.Instance.curCount)
            {
                break;
            }

            if (CONSTSTRIONG.INT_TIMEMAX <= UI_Schedule_Script.Instance.schedule_Time)
            {
                break;
            }

            yield return null;
        }

        //�������ͽ� ���� �߰�
        HealthValunceType a_CurHealthValunce = ScheduleSystem_Manager.Instance.curScheduleData._curHealthValunceArr[ScheduleSystem_Manager.s_curWeekDay.ToInt()];

        float a_TotalStr = 0;

        switch (a_CurHealthValunce)
        {
            case HealthValunceType.Easy:
                StatusSystem_Manager.Instance.Set_StressPlus_Func(DataBase_Manager.Instance.GetTable_Define.level_LowStress);

                a_TotalStr = DataBase_Manager.Instance.GetTable_Define.plus_Low_lowerBodyExercises / 10.0f;
                a_TotalStr = a_TotalStr * UI_Schedule_Script.Instance.curCount;

                StatusSystem_Manager.Instance.Set_LowerbodyStrPlus_Func((int)a_TotalStr);
                break;

            case HealthValunceType.Nomal:
                StatusSystem_Manager.Instance.Set_StressPlus_Func(DataBase_Manager.Instance.GetTable_Define.level_MidStress);

                a_TotalStr = DataBase_Manager.Instance.GetTable_Define.plus_Mid_lowerBodyExercises / 10;
                a_TotalStr *= UI_Schedule_Script.Instance.curCount;

                StatusSystem_Manager.Instance.Set_LowerbodyStrPlus_Func((int)a_TotalStr);
                break;

            case HealthValunceType.Hard:
                StatusSystem_Manager.Instance.Set_StressPlus_Func(DataBase_Manager.Instance.GetTable_Define.level_HigtStress);

                a_TotalStr = DataBase_Manager.Instance.GetTable_Define.plus_Higt_lowerBodyExercises / 10;
                a_TotalStr *= UI_Schedule_Script.Instance.curCount;

                StatusSystem_Manager.Instance.Set_LowerbodyStrPlus_Func((int)a_TotalStr);
                break;
        }

        UI_Schedule_Script.Instance.WeekDayClear_Func(this, (int)a_TotalStr);

        yield return null;

        this._corData.StopCorountine_Func();
    }
}
