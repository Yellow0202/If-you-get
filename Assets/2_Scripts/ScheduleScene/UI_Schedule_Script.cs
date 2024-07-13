using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cargold;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

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
    [SerializeField, FoldoutGroup("정산"), LabelText("연출_피로도 애니메이션")] private Animation _streesAnim;
    [SerializeField, FoldoutGroup("정산"), LabelText("연출_정신력 애니메이션")] private Animation _mentalAnim;
    [SerializeField, FoldoutGroup("정산"), LabelText("정산 소모 코스트 나열 배열")] private TotalCostObj_Script[] _totalCostObjArr;
    [SerializeField, FoldoutGroup("정산"), LabelText("메인 이동 버튼")] private Button _goToMainBtn;
    [SerializeField, FoldoutGroup("정산"), LabelText("몇 월 정산 텍스트")] private TextMeshProUGUI _monthText;
    [SerializeField, FoldoutGroup("정산"), LabelText("남은 금액 텍스트")] private TextMeshProUGUI _resultCost;
    [LabelText("정산 코루틴 변수")] private CoroutineData _totalResultCorData;

    private void Awake()
    {
        Instance = this;
        this._onoffGameObject.SetActive(false);
        this._resultNextDayBtn.onClick.AddListener(NextBtnClick_Func);

        this._goToMainBtn.onClick.AddListener(() => { SceneManager.LoadScene("MainScene"); });

        this._totalObj.SetActive(false);

        for (int i = 0; i < this._totalCostObjArr.Length; i++)
        {
            this._totalCostObjArr[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        ScheduleSystem_Manager.Instance.Start_Schedule_Func();
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

    public void WeekDayClear_Func(ScheduleBase a_ClearData)
    {
        this._resultCommentText.text = "테스트 진행 \n" + a_ClearData.myschedulType;
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

        ScheduleSystem_Manager.Instance.Set_NestWeekDay_Func();
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
        this._monthText.text = "<" + 3 + "월 정산 금액" + ">";
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
            yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            this._streesAnim.Play("Schedule_Stress_Anim");
            yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            this._mentalAnim.Play("Schedule_Mental_Anim");
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
                this._totalCostObjArr[4].Call_Func(ScheduleSystem_Manager.Instance.plusStatus.businessGold, TotalCostType._Break);
                yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            }
        }   //정산표 연출 및 기입

        yield return Coroutine_C.GetWaitForSeconds_Cor(0.65f);
        this._resultCost.text = "남은 금액 : " + UserSystem_Manager.Instance.wealth.GetQuantity_Func(WealthType.Money);
        this._resultCost.gameObject.SetActive(true);
        yield return Coroutine_C.GetWaitForSeconds_Cor(0.75f);
        this._goToMainBtn.transform.parent.gameObject.SetActive(true);

        this._totalResultCorData.StopCorountine_Func();
    }
}
