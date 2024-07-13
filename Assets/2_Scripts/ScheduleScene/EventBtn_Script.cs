using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
using System;

public class EventBtn_Script : MonoBehaviour
{
    [SerializeField, LabelText("버튼 텍스트")] private TextMeshProUGUI _btnName;
    [SerializeField, LabelText("결과 텍스트")] private TextMeshProUGUI _btnComment;
    [SerializeField, LabelText("버튼")] private Button _btn;

    [LabelText("그냥 버튼 데이터")] private EventSel_InfoData _eventSelInfoData;
    [LabelText("퍼센트 버튼 데이터")] private List<EventSelP_InfoData> _eventSelPInfoData;

    [LabelText("퍼센트 버튼인지")] private bool is_Persent;

    public void EventBtnSetting_Func(string a_BtnName, bool is_Persent)
    {
        this.is_Persent = is_Persent;

        if (this.is_Persent == false)
        {
            this._eventSelInfoData = DataBase_Manager.Instance.GetEventSel_Info.Get_NameToEventSelDataDic_Func(a_BtnName);
            this._btnComment.text = "";
            this._btnName.text = LocalizeSystem_Manager.Instance.GetLcz_Func(this._eventSelInfoData.Btn);
        }
        else
        {
            this._eventSelPInfoData = DataBase_Manager.Instance.GetEventSelP_Info.Get_NameToEventSelPDataDic_Func(a_BtnName);

            this._btnName.text = LocalizeSystem_Manager.Instance.GetLcz_Func(this._eventSelPInfoData[0].Btn);
            this._btnComment.text = "성공 확률 : " + this._eventSelPInfoData[0].Percent * 100 + "%";
        }

        this._btn.onClick.AddListener(Click_Btn_Func);
        this.gameObject.SetActive(true);
    }

    private void Click_Btn_Func()
    {
        if (this.is_Persent == true)
        {
            //성공 실패 여부에 따라 데이터 넘기기.
            float a_Random = UnityEngine.Random.Range(0.0f, 1.0f);

            if(a_Random <= this._eventSelPInfoData[0].Percent)
            {   //true
                EventUI_Script.Instance.EventBtnCall_Func(this._eventSelPInfoData[0]);
            }
            else
            {   //false
                EventUI_Script.Instance.EventBtnCall_Func(this._eventSelPInfoData[1]);
            }
        }
        else
        {
            EventUI_Script.Instance.EventBtnCall_Func(this._eventSelInfoData);
        }
    }

    public void EventEnd_Func(Action a_Action)
    {
        this.gameObject.SetActive(true);

        this._btnName.text = "OK";
        this._btnComment.text = "";

        this._btn.onClick.AddListener(() =>
        {
            a_Action();
            this.Reset_AllListeners_Func();
            ScheduleSystem_Manager.Instance.Set_NestWeekDay_Func();
        });
    }

    public void Reset_AllListeners_Func()
    {
        this._btn.onClick.RemoveAllListeners();
    }
}
