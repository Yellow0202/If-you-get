using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;

public class EventUI_Script : MonoBehaviour
{
    public static EventUI_Script Instance;

    [LabelText("현재 이벤트")] private Event_InfoData _curEventData;
    [SerializeField, LabelText("온 오프 오브젝트")] private GameObject onoffObj;
    [SerializeField, LabelText("애니메이션")] private Animation _anim;

    [SerializeField, LabelText("이벤트 등장 오브젝트")] private GameObject _eventOccurrenceObj;

    [SerializeField, LabelText("이벤트 이름 텍스트")] private TextMeshProUGUI _eventNameText;
    [SerializeField, LabelText("이벤트 설명 텍스트")] private TextMeshProUGUI _eventCommentText;
    [SerializeField, LabelText("결과 표기 텍스트")] private TextMeshProUGUI _eventResultText;
    [SerializeField, LabelText("버튼 리스트(최대3)")] private List<EventBtn_Script> _eventBtnList;

    private void Awake()
    {
        Instance = this;
        this.onoffObj.SetActive(false);
    }

    public void EventStart_Func(Event_InfoData a_CurEvent)
    {
        this.onoffObj.SetActive(true);
        this._curEventData = a_CurEvent;

        this.BtnReSet_Func();

        this._anim.Play("Event_Occurrence_Anim");
    }

    public void EventActive_Func()
    {
        this._eventOccurrenceObj.SetActive(false);

        this._eventNameText.text = "[" + LocalizeSystem_Manager.Instance.GetLcz_Func(this._curEventData.Name) + "]";
        this._eventCommentText.text = LocalizeSystem_Manager.Instance.GetLcz_Func(this._curEventData.Comment);

        for (int i = 0; i < this._curEventData.Btn.Length; i++)
        {
            this._eventBtnList[this._eventBtnList.Count - (i + 1)].EventBtnSetting_Func(this._curEventData.Btn[i], this._curEventData.is_PersentBtn);
        }

        this._anim.Play("Event_BoxOn_Anim");
    }

    public void EventBtnActive_Func()
    {
        this._anim.Play("Event_BtnOn_Anim");
    }

    public void EventBtnCall_Func(EventSel_InfoData a_EventSelInfoData)
    {
        this._eventCommentText.text = LocalizeSystem_Manager.Instance.GetLcz_Func(a_EventSelInfoData.Comment);
        this._eventResultText.gameObject.SetActive(true);
        this._eventResultText.text = LocalizeSystem_Manager.Instance.GetLcz_Func(a_EventSelInfoData.Status_change_Str);
        this.EvnetClose_Func();
    }

    public void EventBtnCall_Func(EventSelP_InfoData a_EventSelPInfoData)
    {
        this._eventCommentText.text = LocalizeSystem_Manager.Instance.GetLcz_Func(a_EventSelPInfoData.Comment);
        this._eventResultText.gameObject.SetActive(true);
        this._eventResultText.text = LocalizeSystem_Manager.Instance.GetLcz_Func(a_EventSelPInfoData.Status_change_Str);

        this.EvnetClose_Func();
    }

    private void EvnetClose_Func()
    {
        this.BtnReSet_Func();
        this._eventBtnList[this._eventBtnList.Count - 1].EventEnd_Func(() =>
        {
            this.Reset_Func();
        });
    }

    public void LetMoveOn_Func()
    {
        this.BtnReSet_Func();
        this.Reset_Func();
    }

    private void Reset_Func()
    {
        this._eventResultText.gameObject.SetActive(false);
        this._anim.Play("Event_BoxOff_Anim");
        this.onoffObj.SetActive(false);
    }

    private void BtnReSet_Func()
    {
        for (int i = 0; i < this._eventBtnList.Count; i++)
        {
            this._eventBtnList[i].Reset_AllListeners_Func();
            this._eventBtnList[i].gameObject.SetActive(false);
        }
    }
}
