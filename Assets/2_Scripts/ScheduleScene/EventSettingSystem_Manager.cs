using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using UnityEngine.SceneManagement;
using Cargold.Infinite;
using static ScheduleSystem_Manager;

public class EventSettingSystem_Manager : SerializedMonoBehaviour, GameSystem_Manager.IInitializer
{
    public static EventSettingSystem_Manager Instance;

    [SerializeField, LabelText("이벤트 발생 확률"), ReadOnly] float _eventCallPersent => DataBase_Manager.Instance.GetTable_Define.event_CallPersent;
    [SerializeField, LabelText("조건부 이벤트 발생 확률"), ReadOnly] float _eventStatusCallPersent => DataBase_Manager.Instance.GetTable_Define.event_StatusPersent;

    [SerializeField, LabelText("현재까지 오픈된 이벤트 수"), ReadOnly] private int _eventCurCount; public int eventCurCount => this._eventCurCount;

    public void Init_Func(int _layer)
    {
        if (_layer == 0)
        {
            Instance = this;
            this._eventCurCount = 0;
        }
        else if (_layer == 1)
        {

        }
        else if (_layer == 2)
        {

        }
    }

    public bool Is_EventCall_Func()
    {
        float a_Random = Random.Range(0.0f, 1.0f);

        if(a_Random <= this.Get_EventCallPersent_Func())
            return true;
        else
            return false;
    }

    public void EventCall_Func()
    {
        //이벤트 호출 됨.

        this._eventCurCount++;

        //조건부 이벤트 확률적 호출
        float a_RandomFloat = Random.Range(0.0f, 1.0f);
        Event_InfoData a_Event = new Event_InfoData();

        if (a_RandomFloat <= this.Get_EventStatusCallPersent_Func())
        {
            //각 스테이터스 별로 호출 가능 이벤트가 있는지 확인.
            //리스트에 넣고 해당 리스트에서 랜덤으로 하나 지정.

            List<Event_InfoData> a_StatusEventList = new List<Event_InfoData>();

            UserStatusData a_UserStatusData = UserSystem_Manager.Instance.status.Get_UserStatus_Func();

            for (int i = 0; i < StatusType.Done.ToInt(); i++)
            {
                float a_Value = 0.0f;

                switch((StatusType)i)
                {
                    case StatusType.BackMovementStr:
                        a_Value = a_UserStatusData.backMovementSTR;
                        break;

                    case StatusType.ChestExercisesStr:
                        a_Value = a_UserStatusData.chestExercisesSTR;
                        break;

                    case StatusType.LowerBodyExercisesStr:
                        a_Value = a_UserStatusData.lowerBodyExercisesSTR;
                        break;

                    case StatusType.Mentality:
                        a_Value = a_UserStatusData.mentality;
                        break;

                    case StatusType.Stress:
                        a_Value = a_UserStatusData.stress;
                        break;

                    case StatusType.Money:
                        a_Value = (float)UserSystem_Manager.Instance.wealth.GetQuantity_Func(WealthType.Money);
                        break;
                }

                Event_InfoData a_EventData = DataBase_Manager.Instance.GetEvent_Info.Get_StatusToboolToEventInfoDicDataDic_Func((StatusType)i, a_Value);

                if(a_EventData != null)
                    a_StatusEventList.Add(a_EventData);
            }

            if(0 < a_StatusEventList.Count)
            {
                a_Event = a_StatusEventList.GetRandItem_Func();
            }
            else
            {
                a_Event = DataBase_Manager.Instance.GetEvent_Info.Get_BoolToEventInfoDataDic_Func();
            }
        }
        else
        {
            a_Event = DataBase_Manager.Instance.GetEvent_Info.Get_BoolToEventInfoDataDic_Func();
        }

        //해당 이벤트 정보를 이벤트 창에게 보내주기.
        UI_Schedule_Script.Instance.EventObjOpen_Func(a_Event);
    }

    public float Get_EventCallPersent_Func()
    {
        float a_Persent = this._eventCallPersent / 100.0f;
        return a_Persent;
    }

    public float Get_EventStatusCallPersent_Func()
    {
        float a_Persent = this._eventStatusCallPersent / 100.0f;
        return a_Persent;
    }
}
