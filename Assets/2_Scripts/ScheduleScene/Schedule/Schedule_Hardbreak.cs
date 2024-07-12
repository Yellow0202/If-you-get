using Cargold;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule_Hardbreak : ScheduleBase
{
    [LabelText("코루틴 변수")] private CoroutineData _corData;

    public override void SchedulStart_Func()
    {
        base.SchedulStart_Func();

        this._corData.StartCoroutine_Func(Schedule_Cor(), CoroutineStartType.StartWhenStop);
    }

    private IEnumerator Schedule_Cor()
    {
        while (true)
        {
            break;
        }

        yield return new WaitForSeconds(1.5f);

        UI_Schedule_Script.Instance.WeekDayClear_Func(this);

        yield return null;

        this._corData.StopCorountine_Func();
    }
}
