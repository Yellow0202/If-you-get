using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_Event_InfoDataGroup
{
    [LabelText("조건부 이벤트 딕셔너리")] private Dictionary<StatusType, Dictionary<bool, List<Event_InfoData>>> _statusToboolToEventInfoDicDataDic;
    [LabelText("기본 이벤트 딕셔너리")] private Dictionary<bool, List<Event_InfoData>> _boolToEventInfoDataDic;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.Set_ToEventInfoDataDic_Func();
    }

    private void Set_ToEventInfoDataDic_Func()
    {
        this._statusToboolToEventInfoDicDataDic = new Dictionary<StatusType, Dictionary<bool, List<Event_InfoData>>>();
        this._boolToEventInfoDataDic = new Dictionary<bool, List<Event_InfoData>>();

        foreach (Event_InfoData item in dataArr)
        {
            if (item.is_StatusEvent == true)
            {
                if (this._statusToboolToEventInfoDicDataDic.TryGetValue(item.Event_StatusType, out Dictionary<bool, List<Event_InfoData>> a_ValueDic) == true)
                {   //현재는 이벤트 호출 여부를 유저데이터에 저장하지 않기 때문에 무조건 false로 둠. 저장 시스템이 들어가면 유저데이터를 통해 값 할당 될 예정.
                    //a_ValueDic.Add(false, item);
                    if(a_ValueDic.TryGetValue(false, out List<Event_InfoData> a_FalseList) == true)
                    {
                        a_FalseList.Add(item);
                    }
                }
                else
                {
                    Dictionary<bool, List<Event_InfoData>> a_NewDic = new Dictionary<bool, List<Event_InfoData>>();

                    List<Event_InfoData> a_NewTreuList = new List<Event_InfoData>();
                    a_NewDic.Add(true, a_NewTreuList);

                    List<Event_InfoData> a_NewFalseList = new List<Event_InfoData>();
                    a_NewFalseList.Add(item);
                    a_NewDic.Add(false, a_NewFalseList);

                    this._statusToboolToEventInfoDicDataDic.Add(item.Event_StatusType, a_NewDic);
                }
            }
            else
            {
                if(this._boolToEventInfoDataDic.TryGetValue(false, out List<Event_InfoData> a_BoolFalseList) == true)
                {
                    a_BoolFalseList.Add(item);
                }
                else
                {
                    List<Event_InfoData> a_NewFalseList = new List<Event_InfoData>();
                    a_NewFalseList.Add(item);
                    this._boolToEventInfoDataDic.Add(false, a_NewFalseList);
                }
            }
        }

        if(this._boolToEventInfoDataDic.TryGetValue(true, out List<Event_InfoData> a_BoolTrueList) == false)
        {
            List<Event_InfoData> a_NewTrueList = new List<Event_InfoData>();
            this._boolToEventInfoDataDic.Add(true, a_NewTrueList);
        }
    }

    public Event_InfoData Get_StatusToboolToEventInfoDicDataDic_Func(StatusType a_StatusType, float a_CurStatusValue)
    {
        if (this._statusToboolToEventInfoDicDataDic.TryGetValue(a_StatusType, out Dictionary<bool, List<Event_InfoData>> a_ValueDic) == true)
        {
            if (a_ValueDic.TryGetValue(false, out List<Event_InfoData> a_EventFalseList) == true)
            {
                if (0 < a_EventFalseList.Count)
                {
                    List<Event_InfoData> a_EventTrueList = a_ValueDic.GetValue_Func(true);

                    Event_InfoData a_EventData = a_EventFalseList[0];
                    a_EventFalseList.Remove(a_EventData);
                    a_EventTrueList.Add(a_EventData);

                    return a_EventData;
                }
                else
                    return null;
            }
            else
                return null;
        }
        else
            return null;
    }

    public Event_InfoData Get_BoolToEventInfoDataDic_Func()
    {
        if (this._boolToEventInfoDataDic.TryGetValue(false, out List<Event_InfoData> a_FalseList) == true)
        {
            List<Event_InfoData> a_TrueList = this._boolToEventInfoDataDic.GetValue_Func(true);

            Event_InfoData a_RandomEvent = a_FalseList.GetRandItem_Func();
            a_FalseList.Remove(a_RandomEvent);
            a_TrueList.Add(a_RandomEvent);

            return a_RandomEvent;
        }
        else
            return null;
    }


#if UNITY_EDITOR
    public override void CallEdit_OnDataImportDone_Func()
    {
        base.CallEdit_OnDataImportDone_Func();

        /* 테이블 임포트가 모두 마무리된 뒤 마지막으로 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */
    }
#endif

}