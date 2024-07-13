using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class GameSystem_Manager : Cargold.FrameWork.GameSystem_Manager
{
    public static GameSystem_Manager Instance;

    [SerializeField, LabelText("현재 월 수"), ReadOnly] private int _curweekDay; public int curweekDay => this._curweekDay;

    protected override void Init_Func()
    {
        base.Init_Func();

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        // 프로젝트가 시작되면 가장 먼저 호출되는 곳입니다.
    }

    public void Set_CurWeekDayCountUp_Func()
    {
        this._curweekDay++;
    }
}
