using Cargold;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule_BackMovement : ScheduleBase
{
    [LabelText("�ڷ�ƾ ����")] private CoroutineData _corData;

    public override void SchedulStart_Func()
    {
        base.SchedulStart_Func();

        this._corData.StartCoroutine_Func(Schedule_Cor(), CoroutineStartType.StartWhenStop);
    }

    private IEnumerator Schedule_Cor()
    {
        //��������Ʈ ����
        UI_Schedule_Script.Instance.Set_ArrowSpriteChange_Func(HealthType.BackMovement);
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
        float a_CurDamage = UserSystem_Manager.Instance.status.Get_UserStatus_Func().backMovementSTR / DataBase_Manager.Instance.GetTable_Define.level_PlusAttackDmg;

        while (true)
        {
            //��
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

        //�������ͽ� ���� �߰�


        UI_Schedule_Script.Instance.WeekDayClear_Func(this);

        yield return null;

        this._corData.StopCorountine_Func();
    }
}
