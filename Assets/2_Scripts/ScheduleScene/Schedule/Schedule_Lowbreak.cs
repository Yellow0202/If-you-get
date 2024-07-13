using Cargold;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule_Lowbreak : ScheduleBase
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

        while (true)
        {
            break;
        }

        yield return new WaitForSeconds(1.5f);

        StatusSystem_Manager.Instance.Set_StressPlus_Func(-40);

        UI_Schedule_Script.Instance.WeekDayClear_Func(this, 40);

        yield return null;

        this._corData.StopCorountine_Func();
    }
}
