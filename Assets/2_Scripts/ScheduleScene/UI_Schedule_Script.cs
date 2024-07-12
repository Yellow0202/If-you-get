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

    [SerializeField, LabelText("온오프 오브젝트")] private GameObject _onoffGameObject;

    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("뒷 배경")] private Image _bgroundImg;
    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("클릭 이미지")] private Image _clickImg;

    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("타입별 애니메이션 온/오프")] private Dictionary<ScheduleType, GameObject> _typeToAnimObjDataDic;

    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("요일 텍스트")] private TextMeshProUGUI _weekDayText;
    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("스케쥴 타입 텍스트")] private TextMeshProUGUI _scheduleTypeText;

    [SerializeField, FoldoutGroup("결과창"), LabelText("결과창 오브젝트")] private GameObject _resultObj;
    [SerializeField, FoldoutGroup("결과창"), LabelText("결과 내용 텍스트")] private TextMeshProUGUI _resultCommentText;
    [SerializeField, FoldoutGroup("결과창"), LabelText("다음날 이동 버튼 텍스트")] private Button _resultNextDayBtn;
    [SerializeField, FoldoutGroup("결과창"), LabelText("다음날 이동 버튼")] private TextMeshProUGUI _resultNextDayBtnText;

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
        this._resultCommentText.text = "테스트 진행 \n" + a_ClearData.myschedulType;
        int a_CurWeekDay = ScheduleSystem_Manager.s_curWeekDay.ToInt();
        a_CurWeekDay++;
        string a_NextDaySTR = this.Setting_WeekDayUpdate_Func((CurWeekDayType)a_CurWeekDay);
        this._resultNextDayBtnText.text = a_NextDaySTR + " 스케쥴로";

        this._resultObj.SetActive(true);
    }

    private void NextBtnClick_Func()
    {
        this._resultObj.SetActive(false);
        ScheduleSystem_Manager.Instance.Set_NestWeekDay_Func();
    }
}
