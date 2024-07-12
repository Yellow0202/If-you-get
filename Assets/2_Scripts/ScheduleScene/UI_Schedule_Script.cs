using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cargold;
using TMPro;

public class UI_Schedule_Script : SerializedMonoBehaviour
{
    public static UI_Schedule_Script Instance;

    [SerializeField, LabelText("�¿��� ������Ʈ")] private GameObject _onoffGameObject;

    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("�� ���")] private Image _bgroundImg;
    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("Ŭ�� �̹���")] private Image _clickImg;

    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("Ÿ�Ժ� �ִϸ��̼� ��/����")] private Dictionary<ScheduleType, GameObject> _typeToAnimObjDataDic;

    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("���� �ؽ�Ʈ")] private TextMeshProUGUI _weekDayText;
    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("������ Ÿ�� �ؽ�Ʈ")] private TextMeshProUGUI _scheduleTypeText;

    [SerializeField, FoldoutGroup("���â"), LabelText("���â ������Ʈ")] private GameObject _resultObj;
    [SerializeField, FoldoutGroup("���â"), LabelText("��� ���� �ؽ�Ʈ")] private TextMeshProUGUI _resultCommentText;
    [SerializeField, FoldoutGroup("���â"), LabelText("������ �̵� ��ư �ؽ�Ʈ")] private Button _resultNextDayBtn;
    [SerializeField, FoldoutGroup("���â"), LabelText("������ �̵� ��ư")] private TextMeshProUGUI _resultNextDayBtnText;

    private void Awake()
    {
        Instance = this;
        this._onoffGameObject.SetActive(false);
        this._resultNextDayBtn.onClick.AddListener(NextBtnClick_Func);
    }

    private void Start()
    {
        ScheduleSystem_Manager.Instance.Start_Schedule_Func();
    }

    public void Setting_OnOffObject_Func(ScheduleType a_CurType)
    {
        Debug.Log(a_CurType);

        switch(a_CurType)
        {
            case ScheduleType.BackMovement:
                this._scheduleTypeText.text = CONSTSTRIONG.STR_BACKMOVEMENT;

                break;

            case ScheduleType.ChestExercises:
                this._scheduleTypeText.text = CONSTSTRIONG.STR_CHESTEXERCISES;

                break;

            case ScheduleType.LowerBodyExercises:
                this._scheduleTypeText.text = CONSTSTRIONG.STR_LOWERBODYEXERCIESE;

                break;

            case ScheduleType.Lowbreak:
                this._scheduleTypeText.text = CONSTSTRIONG.STR_LOWBREAK;

                break;

            case ScheduleType.Hardbreak:
                this._scheduleTypeText.text = CONSTSTRIONG.STR_HARDBREAK;

                break;

            case ScheduleType.Business:
                this._scheduleTypeText.text = CONSTSTRIONG.STR_BUSINESS;

                break;

            case ScheduleType.Cheating:
                this._scheduleTypeText.text = CONSTSTRIONG.STR_CHEATING;
                break;

            default:
                break;
        }
        this._weekDayText.text = this.Setting_WeekDayUpdate_Func(ScheduleSystem_Manager.s_curWeekDay);

        this._onoffGameObject.SetActive(true);
    }

    private string Setting_WeekDayUpdate_Func(CurWeekDayType a_WeekDay)
    {
        switch(a_WeekDay)
        {
            case CurWeekDayType.Monday:
                return CONSTSTRIONG.STR_MONDAY;

            case CurWeekDayType.Tuesday:
                return CONSTSTRIONG.STR_THUESDAY;

            case CurWeekDayType.Wednesday:
                return CONSTSTRIONG.STR_WEDNESDAY;

            case CurWeekDayType.Thursday:
                this._weekDayText.text = CONSTSTRIONG.STR_THURSDAY;
                break;

            case CurWeekDayType.Friday:
                return CONSTSTRIONG.STR_FRIDAY;

            case CurWeekDayType.Saturday:
                return CONSTSTRIONG.STR_SATURDAY;

            case CurWeekDayType.Sunday:
                return CONSTSTRIONG.STR_SUNDAY;
        }

        return "";
    }

    public void WeekDayClear_Func(ScheduleBase a_ClearData)
    {
        this._resultCommentText.text = "�׽�Ʈ ���� \n" + a_ClearData.myschedulType;
        int a_CurWeekDay = ScheduleSystem_Manager.s_curWeekDay.ToInt();
        a_CurWeekDay++;
        string a_NextDaySTR = this.Setting_WeekDayUpdate_Func((CurWeekDayType)a_CurWeekDay);
        this._resultNextDayBtnText.text = a_NextDaySTR + " �������";

        this._resultObj.SetActive(true);
    }

    private void NextBtnClick_Func()
    {
        this._resultObj.SetActive(false);
        ScheduleSystem_Manager.Instance.Set_NestWeekDay_Func();
    }
}
