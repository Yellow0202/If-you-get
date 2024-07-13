using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cargold;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using Cargold.FrameWork;

public enum HealthType
{
    BackMovement = 0,
    ChestExercises,
    LowerBodyExercises,
    MAX
}

public class UI_Schedule_Script : SerializedMonoBehaviour
{
    public static UI_Schedule_Script Instance;

    [SerializeField, LabelText("온오프 오브젝트")] private GameObject _onoffGameObject;
    [SerializeField, LabelText("인게임 온 오프 오브젝트")] private GameObject _inGameOnOffGameObject;

    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("뒷 배경")] private Image _bgroundImg;
    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("클릭 이미지")] private Image _clickImg;

    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("타입별 애니메이션 온/오프")] private Dictionary<ScheduleType, GameObject> _typeToAnimObjDataDic;

    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("요일 텍스트")] private TextMeshProUGUI _weekDayText;
    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("스케쥴 타입 텍스트")] private TextMeshProUGUI _scheduleTypeText;

    [SerializeField, FoldoutGroup("결과창"), LabelText("결과창 오브젝트")] private GameObject _resultObj;
    [SerializeField, FoldoutGroup("결과창"), LabelText("결과 내용 텍스트")] private TextMeshProUGUI _resultCommentText;
    [SerializeField, FoldoutGroup("결과창"), LabelText("다음날 이동 버튼 텍스트")] private Button _resultNextDayBtn;
    [SerializeField, FoldoutGroup("결과창"), LabelText("다음날 이동 버튼")] private TextMeshProUGUI _resultNextDayBtnText;

    [SerializeField, FoldoutGroup("정산"), LabelText("온오프")] private GameObject _totalObj;
    [SerializeField, FoldoutGroup("정산"), LabelText("이전 근력 텍스트")] private TextMeshProUGUI _oldStrengthText;
    [SerializeField, FoldoutGroup("정산"), LabelText("변화 근력 텍스트")] private TextMeshProUGUI _newStrengthText;
    [SerializeField, FoldoutGroup("정산"), LabelText("이전 피로도 텍스트")] private TextMeshProUGUI _oldStressText;
    [SerializeField, FoldoutGroup("정산"), LabelText("변화 피로도 텍스트")] private TextMeshProUGUI _newStressText;
    [SerializeField, FoldoutGroup("정산"), LabelText("이전 피로도 이미지")] private Image _oldStressImg;
    [SerializeField, FoldoutGroup("정산"), LabelText("변화 피로도 이미지")] private Image _newStressImg;
    [SerializeField, FoldoutGroup("정산"), LabelText("이전 정신력 리스트")] private List<GameObject> _oldMentalObjList;
    [SerializeField, FoldoutGroup("정산"), LabelText("변화 정신력 리스트")] private List<GameObject> _newMentalObjList;
    [SerializeField, FoldoutGroup("정산"), LabelText("연출_근력 애니메이션")] private Animation _strengthAnim;
    [SerializeField, FoldoutGroup("정산"), LabelText("연출_피로도 애니메이션")] private Animation _stressAnim;
    [SerializeField, FoldoutGroup("정산"), LabelText("연출_정신력 애니메이션")] private Animation _mentalAnim;
    [SerializeField, FoldoutGroup("정산"), LabelText("정산 소모 코스트 나열 배열")] private TotalCostObj_Script[] _totalCostObjArr;
    [SerializeField, FoldoutGroup("정산"), LabelText("메인 이동 버튼")] private Button _goToMainBtn;
    [SerializeField, FoldoutGroup("정산"), LabelText("메인 이동 버튼 이미지")] private Image _goToMainBtnImg;
    [SerializeField, FoldoutGroup("정산"), LabelText("몇 월 정산 텍스트")] private TextMeshProUGUI _monthText;
    [SerializeField, FoldoutGroup("정산"), LabelText("남은 금액 텍스트")] private TextMeshProUGUI _resultCost;

    [SerializeField, FoldoutGroup("스케쥴"), LabelText("현재 운동 타입"), ReadOnly] private HealthType _schedule_HealthType; public HealthType schedule_HealthType => this._schedule_HealthType;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("카운트다운 텍스트")] private TextMeshProUGUI _schedule_CountDownText; public TextMeshProUGUI schedule_CountDownText => this._schedule_CountDownText;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("벤치프레스 갯수 텍스트")] private TextMeshProUGUI _schedule_CountText; public TextMeshProUGUI schedule_CountText => this._schedule_CountText;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("타이머 텍스트")] private TextMeshProUGUI _schedule_TimerText; public TextMeshProUGUI schedule_TimerText => this._schedule_TimerText;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("게이지 이미지")] private Image _schedule_GageBar; public Image schedule_GageBar => this._schedule_GageBar;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("게이지 스크롤 바")] private Scrollbar _schedule_Scrollbar; public Scrollbar schedule_Scrollbar => this._schedule_Scrollbar;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("배경 이미지")] private Image _schedule_BgImg; public Image schedule_BgImg => this._schedule_BgImg;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("키 이미지")] private Image _schedule_KeyImg; public Image schedule_KeyImg => this._schedule_KeyImg;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("애니메이션 리스트")] private List<Animation> _schedule_AnimationList; public List<Animation> schedule_AnimationList => this._schedule_AnimationList;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("눌리기 전 스프라이트"), ReadOnly] private Sprite _schedule_Out_Sprite;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("눌리기 후 스프라이트"), ReadOnly] private Sprite _schedule_In_Sprite;

    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("타이머"), ReadOnly] private float _schedule_Time; public float schedule_Time => this._schedule_Time;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("감소 타이머"), ReadOnly] private float _schedule_DeleteTime;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("최대 체력"), ReadOnly] private float _MaxHPGage;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("현재 체력"), ReadOnly] private float _curHPGage;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("현재 부하량"), ReadOnly] private float _curDeleyValue;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("현재 운동"), ReadOnly] private string _curHealthTypeStr;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("현재 갯수"), ReadOnly] private int _curCount; public int curCount => this._curCount;
    [LabelText("현재 운동 체력 정보")] private List<int> _curHealthHpData;


    [LabelText("정산 코루틴 변수")] private CoroutineData _totalResultCorData;

    private void Awake()
    {
        Instance = this;
        this._onoffGameObject.SetActive(false);
        this._inGameOnOffGameObject.SetActive(false);
        this._resultNextDayBtn.onClick.AddListener(NextBtnClick_Func);

        this._goToMainBtn.onClick.AddListener(() =>
        {
            if(GameSystem_Manager.Instance.curweekDay % 3 == 0)
            {
                SceneManager.LoadScene("2.MeasurementScene");
            }
            else
            {
                SceneManager.LoadScene("0.MainScene_2");
            }

            GameSystem_Manager.Instance.Set_CurWeekDayCountUp_Func();
        });

        this._totalObj.SetActive(false);

        for (int i = 0; i < this._totalCostObjArr.Length; i++)
        {
            this._totalCostObjArr[i].gameObject.SetActive(false);
        }

        this._curHealthHpData = new List<int>();
        this._curCount = 0;
    }

    private void Start()
    {
        //나중에 연출 맞춰서 시작하도록 수정
        this.Start_Func();
    }

    public void Start_Func()
    {
        //this.Set_HpBar_Func();
        ScheduleSystem_Manager.Instance.Start_Schedule_Func();
    }

    public void Set_BgImgChange_Func(ScheduleType a_BgType)
    {
        switch (a_BgType)
        {
            case ScheduleType.BackMovement:
                Sound_Script.Instance.Play_BGM(BGMListType.공용운동BGM);
                break;

            case ScheduleType.ChestExercises:
                Sound_Script.Instance.Play_BGM(BGMListType.공용운동BGM);
                break;

            case ScheduleType.LowerBodyExercises:
                Sound_Script.Instance.Play_BGM(BGMListType.공용운동BGM);
                break;

            case ScheduleType.Lowbreak:
                Sound_Script.Instance.Play_BGM(BGMListType.휴식BGM);
                break;

            case ScheduleType.Hardbreak:
                Sound_Script.Instance.Play_BGM(BGMListType.휴식BGM);
                break;

            case ScheduleType.Business:
                Sound_Script.Instance.Play_BGM(BGMListType.업무BGM);
                break;

            case ScheduleType.Cheating:
                Sound_Script.Instance.Play_BGM(BGMListType.치팅데이BGM);
                break;
        }
    }

    public void Set_ArrowSpriteChange_Func(HealthType a_ArrowType)
    {
        this._schedule_HealthType = a_ArrowType;

        switch (a_ArrowType)
        {
            case HealthType.BackMovement:
                //위키
                this._schedule_KeyImg.sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                this._schedule_In_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowIn;
                this._schedule_Out_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                break;

            case HealthType.ChestExercises:
                //위키
                this._schedule_KeyImg.sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                this._schedule_In_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowIn;
                this._schedule_Out_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                break;

            case HealthType.LowerBodyExercises:
                //아래키
                this._schedule_KeyImg.sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_DownArrowOut;
                this._schedule_In_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_DownArrowIn;
                this._schedule_Out_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_DownArrowOut;
                break;
        }
    }

    public void Arrow_InputSpriteChange_Func(bool is_Down)
    {
        if (is_Down == true)
            this._schedule_KeyImg.sprite = this._schedule_In_Sprite;
        else
            this._schedule_KeyImg.sprite = this._schedule_Out_Sprite;
    }

    public void Set_HpBar_Func()
    {
        if(this._curCount <= 0)
        {
            switch (this._schedule_HealthType)
            {
                case HealthType.ChestExercises:
                    this._curHealthHpData =
                        DataBase_Manager.Instance.GetBackSchedule.Get_TypeToBackScheduleDataDic_Func(ScheduleSystem_Manager.Instance.curScheduleData._curHealthValunceArr[ScheduleSystem_Manager.s_curWeekDay.ToInt()]);

                    this._curHealthTypeStr = CONSTSTRIONG.STR_BENCHPRESS;
                    break;

                case HealthType.BackMovement:
                    this._curHealthHpData =
                        DataBase_Manager.Instance.GetChestSchedule.Get_TypeToChestScheduleDDataDic_Func(ScheduleSystem_Manager.Instance.curScheduleData._curHealthValunceArr[ScheduleSystem_Manager.s_curWeekDay.ToInt()]);

                    this._curHealthTypeStr = CONSTSTRIONG.STR_DEADLIFT;
                    break;

                case HealthType.LowerBodyExercises:
                    this._curHealthHpData =
                        DataBase_Manager.Instance.GetLowerSchedule.Get_TypeToLowerScheduleDataDic_Func(ScheduleSystem_Manager.Instance.curScheduleData._curHealthValunceArr[ScheduleSystem_Manager.s_curWeekDay.ToInt()]);

                    this._curHealthTypeStr = CONSTSTRIONG.STR_SQUAT;
                    break;
            }

            this.Reset_Func();
            this._inGameOnOffGameObject.SetActive(true);
        }
        else if(CONSTSTRIONG.INT_COUNTMAX <= this._curCount)
        {
            //종료 상태임을 알림.
            return;
        }

        this._MaxHPGage = this._curHealthHpData[this._curCount];
        this._schedule_CountText.text = this._curHealthTypeStr + "[" + this._curCount + "/" + CONSTSTRIONG.INT_COUNTMAX + "]";
        this._curDeleyValue = DataBase_Manager.Instance.GetTable_Define.level_Back_DeleyValue / this._MaxHPGage;
        this._curHPGage = 0.0f;
        this.Set_HP_Func(0.0f);
    }

    public void Set_AttackDmg_Func(float a_AttackDmg)
    {
        this.Set_HP_Func(a_AttackDmg);
    }

    public void Update_Health_Func()
    {
        this._schedule_Time += Time.deltaTime;
        this._schedule_DeleteTime += Time.deltaTime;

        switch(this._schedule_HealthType)
        {
            case HealthType.BackMovement:
                if (DataBase_Manager.Instance.GetTable_Define.level_Back_DeleteTime <= this._schedule_DeleteTime)
                {
                    this._schedule_DeleteTime = 0.0f;
                    this.Set_HP_Func(-(this._curDeleyValue));
                }
                break;

            case HealthType.ChestExercises:
                if (DataBase_Manager.Instance.GetTable_Define.level_Chest_DeleteTime <= this._schedule_DeleteTime)
                {
                    this._schedule_DeleteTime = 0.0f;
                    this.Set_HP_Func(-(this._curDeleyValue));
                }
                break;

            case HealthType.LowerBodyExercises:
                if (DataBase_Manager.Instance.GetTable_Define.level_LowerBody_DeleteTime <= this._schedule_DeleteTime)
                {
                    this._schedule_DeleteTime = 0.0f;
                    this.Set_HP_Func(-(this._curDeleyValue));
                }
                break;
        }

        if(1.0f <= this._curHPGage)
        {
            this._curCount++;
            this._schedule_Time = 0.0f;
            Sound_Script.Instance.Play_SFX(SFXListType.갯수증가SFX);
            this.Set_HpBar_Func();
        }
        this._schedule_TimerText.text = this._schedule_Time.ToString("F2").ToString() + " 초";
    }

    private void Set_HP_Func(float a_Value)
    {
        this._curHPGage += a_Value / this._MaxHPGage;

        if (this._curHPGage < 0.0f)
            this._curHPGage = 0.0f;

        if (1.0f < this._curHPGage)
            this._curHPGage = 1.0f;

        this._schedule_GageBar.fillAmount = this._curHPGage;
        this._schedule_Scrollbar.value = this._curHPGage;
    }

    public void Setting_OnOffObject_Func(ScheduleType a_CurType)
    {
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
                return CONSTSTRIONG.STR_THURSDAY;

            case CurWeekDayType.Friday:
                return CONSTSTRIONG.STR_FRIDAY;

            case CurWeekDayType.Saturday:
                return CONSTSTRIONG.STR_SATURDAY;

            case CurWeekDayType.Sunday:
                return CONSTSTRIONG.STR_SUNDAY;
        }

        return "";
    }

    public void WeekDayClear_Func(ScheduleBase a_ClearData, int a_PlusValue)
    {
        this._inGameOnOffGameObject.SetActive(false);

        switch(a_ClearData.myschedulType)
        {
            case ScheduleType.BackMovement:
                this._resultCommentText.text = this._curHealthTypeStr + "[" + this._curCount + "/" + CONSTSTRIONG.INT_COUNTMAX + "]\n\n" +
                                               "<color=green>등 근육 +" + a_PlusValue + "</color>";
                break;

            case ScheduleType.ChestExercises:
                this._resultCommentText.text = this._curHealthTypeStr + "[" + this._curCount + "/" + CONSTSTRIONG.INT_COUNTMAX + "]\n\n" +
                               "<color=green>가슴 근육 +" + a_PlusValue + "</color>";
                break;

            case ScheduleType.LowerBodyExercises:
                this._resultCommentText.text = this._curHealthTypeStr + "[" + this._curCount + "/" + CONSTSTRIONG.INT_COUNTMAX + "]\n\n" +
                               "<color=green>하체 근육 +" + a_PlusValue + "</color>";
                break;

            case ScheduleType.Lowbreak:
                this._resultCommentText.text = "느긋한 휴식을 즐겼다!\n\n" + "<color=green>피로도 -" + a_PlusValue + "</color>";
                break;

            case ScheduleType.Hardbreak:
                this._resultCommentText.text = "화려한 휴식을 즐겼다!!\n\n" + "<color=green>피로도 -" + a_PlusValue + "</color><color=red>" + "  돈 -100,000" + "</color>";
                break;

            case ScheduleType.Business:
                this._resultCommentText.text = "업무를 진행했다\n\n" + "<color=green>돈 +" + a_PlusValue + "</color><color=end>피로도 +20</color>";
                break;

            case ScheduleType.Cheating:
                this._resultCommentText.text = "치킨 맛있다\n\n" + "<color=green>정신력 +" + a_PlusValue + "</color>  <color=red>돈 -50,000" + "\n랜덤근력 -5</color>";
                break;
        }

        this.Reset_Func();

        int a_CurWeekDay = ScheduleSystem_Manager.s_curWeekDay.ToInt();
        a_CurWeekDay++;
        string a_NextDaySTR = this.Setting_WeekDayUpdate_Func((CurWeekDayType)a_CurWeekDay);

        if((CurWeekDayType)a_CurWeekDay != CurWeekDayType.MAX)
        {
            this._resultNextDayBtnText.text = a_NextDaySTR + " 스케쥴로";
        }
        else
        {
            this._resultNextDayBtnText.text = "정산하기";
        }

        this._resultObj.SetActive(true);
    }

    private void NextBtnClick_Func()
    {
        this._resultObj.SetActive(false);


        //일정 확률로 이벤트가 발생했다면 이벤트 처리를 우선
        if (EventSettingSystem_Manager.Instance.Is_EventCall_Func() == true &&
            EventSettingSystem_Manager.Instance.eventCurCount < DataBase_Manager.Instance.GetTable_Define.event_CountMax)
        {
            //이벤트 발생
            EventSettingSystem_Manager.Instance.EventCall_Func();
        }
        else
        {
            if(EventSettingSystem_Manager.Instance.eventCurCount <= 0)
            {
                //이벤트 발생
                EventSettingSystem_Manager.Instance.EventCall_Func();
            }
            else
            {
                ScheduleSystem_Manager.Instance.Set_NestWeekDay_Func();
            }
        }
    }

    public void EventObjOpen_Func(Event_InfoData a_CurEventData)
    {
        //이벤트 오브젝트를 오픈해주고 해당 이벤트를 진행하기.
        EventUI_Script.Instance.EventStart_Func(a_CurEventData);
    }

    public void TotalResult_Func()
    {
        //정산 애니메이션 후 호출됨.
        this._weekDayText.transform.parent.gameObject.SetActive(false);
        this._scheduleTypeText.transform.parent.gameObject.SetActive(false);

        this._totalObj.SetActive(true);
        this._totalResultCorData.StartCoroutine_Func(TotalResult_Cor(), CoroutineStartType.StartWhenStop);
    }

    private IEnumerator TotalResult_Cor()
    {
        this._monthText.text = "<" + GameSystem_Manager.Instance.curweekDay + "월 정산 금액" + ">";
        this._goToMainBtn.transform.parent.gameObject.SetActive(false);
        this._resultCost.gameObject.SetActive(false);

        {
            UserStatusData a_OldUserStatus = UserSystem_Manager.Instance.status.Get_UserStatus_Func();

            string _oldStr = "";
            _oldStr += "등 " + DataBase_Manager.Instance.GetStrength_Info.Get_BackMovementStrengthInfoDataList_Func(a_OldUserStatus.backMovementSTR) + " / ";
            _oldStr += "가슴 " + DataBase_Manager.Instance.GetStrength_Info.Get_ChestExercisesStrengthInfoDataList_Func(a_OldUserStatus.chestExercisesSTR) + " / ";
            _oldStr += "하체 " + DataBase_Manager.Instance.GetStrength_Info.Get_LowerBodyExercises_CostStrengthInfoDataList_Func(a_OldUserStatus.lowerBodyExercisesSTR);
            this._oldStrengthText.text = _oldStr;

            _oldStr = a_OldUserStatus.stress.ToString() + "/ 100";

            this._oldStressText.text = _oldStr;
            this._oldStressImg.fillAmount = a_OldUserStatus.stress / 100;

            for (int i = 0; i < this._oldMentalObjList.Count; i++)
            {
                this._oldMentalObjList[i].SetActive(false);
            }

            for (int i = 0; i < a_OldUserStatus.mentality; i++)
            {
                this._oldMentalObjList[i].SetActive(true);
            }

            ////////////////////////////

            UserSystem_Manager.Instance.status.Set_BackMovementSTR_Func(ScheduleSystem_Manager.Instance.plusStatus.backStr);
            UserSystem_Manager.Instance.status.Set_ChestExercisesSTR_Func(ScheduleSystem_Manager.Instance.plusStatus.chestStr);
            UserSystem_Manager.Instance.status.Set_LowerBodyExercisesSTR_Func(ScheduleSystem_Manager.Instance.plusStatus.lowerbodyStr);
            UserSystem_Manager.Instance.status.Set_Stress_Func(ScheduleSystem_Manager.Instance.plusStatus.stress);
            UserSystem_Manager.Instance.status.Set_MentalStatus_Func(ScheduleSystem_Manager.Instance.plusStatus.mentalCount);

            UserStatusData a_NewUserStatus = UserSystem_Manager.Instance.status.Get_UserStatus_Func();

            string _newStr = "";
            _newStr += "등 " + DataBase_Manager.Instance.GetStrength_Info.Get_BackMovementStrengthInfoDataList_Func(a_NewUserStatus.backMovementSTR) + " / ";
            _newStr += "가슴 " + DataBase_Manager.Instance.GetStrength_Info.Get_ChestExercisesStrengthInfoDataList_Func(a_NewUserStatus.chestExercisesSTR) + " / ";
            _newStr += "하체 " + DataBase_Manager.Instance.GetStrength_Info.Get_LowerBodyExercises_CostStrengthInfoDataList_Func(a_NewUserStatus.lowerBodyExercisesSTR);
            this._newStrengthText.text = _newStr;

            _newStr = a_NewUserStatus.stress.ToString() + "/ 100";

            this._newStressText.text = _newStr;
            this._newStressImg.fillAmount = a_NewUserStatus.stress / 100.0f;

            for (int i = 0; i < this._newMentalObjList.Count; i++)
            {
                this._newMentalObjList[i].SetActive(false);
            }

            for (int i = 0; i < a_NewUserStatus.mentality; i++)
            {
                this._newMentalObjList[i].SetActive(true);
            }

            yield return null;

        }   //스테이터스

        {
            this._strengthAnim.Play("Schedule_Str_Anim");
            Sound_Script.Instance.Play_SFX(SFXListType.토탈애니메이션SFX);
            yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            this._stressAnim.Play("Schedule_Stress_Anim");
            Sound_Script.Instance.Play_SFX(SFXListType.토탈애니메이션SFX);
            yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            this._mentalAnim.Play("Schedule_Mental_Anim");
            Sound_Script.Instance.Play_SFX(SFXListType.토탈애니메이션SFX);
        }   //스테이터스 연출 시작

        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);

        {
            //헬스비, 식단비, 휴식비용, 이벤트비용
            this._totalCostObjArr[0].Call_Func(DataBase_Manager.Instance.GetTable_Define.totalCost_HealthCost, TotalCostType._Health);
            yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            this._totalCostObjArr[1].Call_Func(DataBase_Manager.Instance.GetTable_Define.totalCost_MealsCost, TotalCostType._Meals);
            yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);

            if (ScheduleSystem_Manager.Instance.plusStatus.breakCost != 0)
            {
                this._totalCostObjArr[2].Call_Func(ScheduleSystem_Manager.Instance.plusStatus.breakCost, TotalCostType._Break);
                yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            }

            if (ScheduleSystem_Manager.Instance.plusStatus.eventCost != 0)
            {
                this._totalCostObjArr[3].Call_Func(ScheduleSystem_Manager.Instance.plusStatus.eventCost, TotalCostType._Event);
                yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            }

            if (ScheduleSystem_Manager.Instance.plusStatus.businessGold != 0)
            {
                this._totalCostObjArr[4].Call_Func(ScheduleSystem_Manager.Instance.plusStatus.businessGold, TotalCostType._Business);
                yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            }
        }   //정산표 연출 및 기입

        yield return Coroutine_C.GetWaitForSeconds_Cor(0.65f);
        this._resultCost.text = "남은 금액 : " + UserSystem_Manager.Instance.wealth.GetQuantity_Func(WealthType.Money).ToStringLong();
        Sound_Script.Instance.Play_SFX(SFXListType.토탈총합등장SFX);
        this._resultCost.gameObject.SetActive(true);
        yield return Coroutine_C.GetWaitForSeconds_Cor(0.75f);

        if(GameSystem_Manager.Instance.curweekDay % 3 == 0)
        {
            this._goToMainBtnImg.sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_Record;
        }

        this._goToMainBtn.transform.parent.gameObject.SetActive(true);

        this._totalResultCorData.StopCorountine_Func();
    }

    private void Reset_Func()
    {
        this._schedule_TimerText.text = "0.00 초";
        this._schedule_Time = 0.0f;
        this._curCount = 0;
    }
}
