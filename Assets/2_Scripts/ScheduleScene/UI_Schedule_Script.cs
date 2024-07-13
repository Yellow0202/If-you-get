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

    [SerializeField, LabelText("�¿��� ������Ʈ")] private GameObject _onoffGameObject;
    [SerializeField, LabelText("�ΰ��� �� ���� ������Ʈ")] private GameObject _inGameOnOffGameObject;

    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("�� ���")] private Image _bgroundImg;
    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("Ŭ�� �̹���")] private Image _clickImg;

    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("Ÿ�Ժ� �ִϸ��̼� ��/����")] private Dictionary<ScheduleType, GameObject> _typeToAnimObjDataDic;

    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("���� �ؽ�Ʈ")] private TextMeshProUGUI _weekDayText;
    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("������ Ÿ�� �ؽ�Ʈ")] private TextMeshProUGUI _scheduleTypeText;

    [SerializeField, FoldoutGroup("���â"), LabelText("���â ������Ʈ")] private GameObject _resultObj;
    [SerializeField, FoldoutGroup("���â"), LabelText("��� ���� �ؽ�Ʈ")] private TextMeshProUGUI _resultCommentText;
    [SerializeField, FoldoutGroup("���â"), LabelText("������ �̵� ��ư �ؽ�Ʈ")] private Button _resultNextDayBtn;
    [SerializeField, FoldoutGroup("���â"), LabelText("������ �̵� ��ư")] private TextMeshProUGUI _resultNextDayBtnText;

    [SerializeField, FoldoutGroup("����"), LabelText("�¿���")] private GameObject _totalObj;
    [SerializeField, FoldoutGroup("����"), LabelText("���� �ٷ� �ؽ�Ʈ")] private TextMeshProUGUI _oldStrengthText;
    [SerializeField, FoldoutGroup("����"), LabelText("��ȭ �ٷ� �ؽ�Ʈ")] private TextMeshProUGUI _newStrengthText;
    [SerializeField, FoldoutGroup("����"), LabelText("���� �Ƿε� �ؽ�Ʈ")] private TextMeshProUGUI _oldStressText;
    [SerializeField, FoldoutGroup("����"), LabelText("��ȭ �Ƿε� �ؽ�Ʈ")] private TextMeshProUGUI _newStressText;
    [SerializeField, FoldoutGroup("����"), LabelText("���� �Ƿε� �̹���")] private Image _oldStressImg;
    [SerializeField, FoldoutGroup("����"), LabelText("��ȭ �Ƿε� �̹���")] private Image _newStressImg;
    [SerializeField, FoldoutGroup("����"), LabelText("���� ���ŷ� ����Ʈ")] private List<GameObject> _oldMentalObjList;
    [SerializeField, FoldoutGroup("����"), LabelText("��ȭ ���ŷ� ����Ʈ")] private List<GameObject> _newMentalObjList;
    [SerializeField, FoldoutGroup("����"), LabelText("����_�ٷ� �ִϸ��̼�")] private Animation _strengthAnim;
    [SerializeField, FoldoutGroup("����"), LabelText("����_�Ƿε� �ִϸ��̼�")] private Animation _stressAnim;
    [SerializeField, FoldoutGroup("����"), LabelText("����_���ŷ� �ִϸ��̼�")] private Animation _mentalAnim;
    [SerializeField, FoldoutGroup("����"), LabelText("���� �Ҹ� �ڽ�Ʈ ���� �迭")] private TotalCostObj_Script[] _totalCostObjArr;
    [SerializeField, FoldoutGroup("����"), LabelText("���� �̵� ��ư")] private Button _goToMainBtn;
    [SerializeField, FoldoutGroup("����"), LabelText("���� �̵� ��ư �̹���")] private Image _goToMainBtnImg;
    [SerializeField, FoldoutGroup("����"), LabelText("�� �� ���� �ؽ�Ʈ")] private TextMeshProUGUI _monthText;
    [SerializeField, FoldoutGroup("����"), LabelText("���� �ݾ� �ؽ�Ʈ")] private TextMeshProUGUI _resultCost;

    [SerializeField, FoldoutGroup("������"), LabelText("���� � Ÿ��"), ReadOnly] private HealthType _schedule_HealthType; public HealthType schedule_HealthType => this._schedule_HealthType;
    [SerializeField, FoldoutGroup("������"), LabelText("ī��Ʈ�ٿ� �ؽ�Ʈ")] private TextMeshProUGUI _schedule_CountDownText; public TextMeshProUGUI schedule_CountDownText => this._schedule_CountDownText;
    [SerializeField, FoldoutGroup("������"), LabelText("��ġ������ ���� �ؽ�Ʈ")] private TextMeshProUGUI _schedule_CountText; public TextMeshProUGUI schedule_CountText => this._schedule_CountText;
    [SerializeField, FoldoutGroup("������"), LabelText("Ÿ�̸� �ؽ�Ʈ")] private TextMeshProUGUI _schedule_TimerText; public TextMeshProUGUI schedule_TimerText => this._schedule_TimerText;
    [SerializeField, FoldoutGroup("������"), LabelText("������ �̹���")] private Image _schedule_GageBar; public Image schedule_GageBar => this._schedule_GageBar;
    [SerializeField, FoldoutGroup("������"), LabelText("������ ��ũ�� ��")] private Scrollbar _schedule_Scrollbar; public Scrollbar schedule_Scrollbar => this._schedule_Scrollbar;
    [SerializeField, FoldoutGroup("������"), LabelText("��� �̹���")] private Image _schedule_BgImg; public Image schedule_BgImg => this._schedule_BgImg;
    [SerializeField, FoldoutGroup("������"), LabelText("Ű �̹���")] private Image _schedule_KeyImg; public Image schedule_KeyImg => this._schedule_KeyImg;
    [SerializeField, FoldoutGroup("������"), LabelText("�ִϸ��̼� ����Ʈ")] private List<Animation> _schedule_AnimationList; public List<Animation> schedule_AnimationList => this._schedule_AnimationList;
    [SerializeField, FoldoutGroup("������"), LabelText("������ �� ��������Ʈ"), ReadOnly] private Sprite _schedule_Out_Sprite;
    [SerializeField, FoldoutGroup("������"), LabelText("������ �� ��������Ʈ"), ReadOnly] private Sprite _schedule_In_Sprite;

    [SerializeField, FoldoutGroup("� ������ ����"), LabelText("Ÿ�̸�"), ReadOnly] private float _schedule_Time; public float schedule_Time => this._schedule_Time;
    [SerializeField, FoldoutGroup("� ������ ����"), LabelText("���� Ÿ�̸�"), ReadOnly] private float _schedule_DeleteTime;
    [SerializeField, FoldoutGroup("� ������ ����"), LabelText("�ִ� ü��"), ReadOnly] private float _MaxHPGage;
    [SerializeField, FoldoutGroup("� ������ ����"), LabelText("���� ü��"), ReadOnly] private float _curHPGage;
    [SerializeField, FoldoutGroup("� ������ ����"), LabelText("���� ���Ϸ�"), ReadOnly] private float _curDeleyValue;
    [SerializeField, FoldoutGroup("� ������ ����"), LabelText("���� �"), ReadOnly] private string _curHealthTypeStr;
    [SerializeField, FoldoutGroup("� ������ ����"), LabelText("���� ����"), ReadOnly] private int _curCount; public int curCount => this._curCount;
    [LabelText("���� � ü�� ����")] private List<int> _curHealthHpData;


    [LabelText("���� �ڷ�ƾ ����")] private CoroutineData _totalResultCorData;

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
        //���߿� ���� ���缭 �����ϵ��� ����
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
                Sound_Script.Instance.Play_BGM(BGMListType.����BGM);
                break;

            case ScheduleType.ChestExercises:
                Sound_Script.Instance.Play_BGM(BGMListType.����BGM);
                break;

            case ScheduleType.LowerBodyExercises:
                Sound_Script.Instance.Play_BGM(BGMListType.����BGM);
                break;

            case ScheduleType.Lowbreak:
                Sound_Script.Instance.Play_BGM(BGMListType.�޽�BGM);
                break;

            case ScheduleType.Hardbreak:
                Sound_Script.Instance.Play_BGM(BGMListType.�޽�BGM);
                break;

            case ScheduleType.Business:
                Sound_Script.Instance.Play_BGM(BGMListType.����BGM);
                break;

            case ScheduleType.Cheating:
                Sound_Script.Instance.Play_BGM(BGMListType.ġ�õ���BGM);
                break;
        }
    }

    public void Set_ArrowSpriteChange_Func(HealthType a_ArrowType)
    {
        this._schedule_HealthType = a_ArrowType;

        switch (a_ArrowType)
        {
            case HealthType.BackMovement:
                //��Ű
                this._schedule_KeyImg.sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                this._schedule_In_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowIn;
                this._schedule_Out_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                break;

            case HealthType.ChestExercises:
                //��Ű
                this._schedule_KeyImg.sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                this._schedule_In_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowIn;
                this._schedule_Out_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                break;

            case HealthType.LowerBodyExercises:
                //�Ʒ�Ű
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
            //���� �������� �˸�.
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
            Sound_Script.Instance.Play_SFX(SFXListType.��������SFX);
            this.Set_HpBar_Func();
        }
        this._schedule_TimerText.text = this._schedule_Time.ToString("F2").ToString() + " ��";
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
                                               "<color=green>�� ���� +" + a_PlusValue + "</color>";
                break;

            case ScheduleType.ChestExercises:
                this._resultCommentText.text = this._curHealthTypeStr + "[" + this._curCount + "/" + CONSTSTRIONG.INT_COUNTMAX + "]\n\n" +
                               "<color=green>���� ���� +" + a_PlusValue + "</color>";
                break;

            case ScheduleType.LowerBodyExercises:
                this._resultCommentText.text = this._curHealthTypeStr + "[" + this._curCount + "/" + CONSTSTRIONG.INT_COUNTMAX + "]\n\n" +
                               "<color=green>��ü ���� +" + a_PlusValue + "</color>";
                break;

            case ScheduleType.Lowbreak:
                this._resultCommentText.text = "������ �޽��� ����!\n\n" + "<color=green>�Ƿε� -" + a_PlusValue + "</color>";
                break;

            case ScheduleType.Hardbreak:
                this._resultCommentText.text = "ȭ���� �޽��� ����!!\n\n" + "<color=green>�Ƿε� -" + a_PlusValue + "</color><color=red>" + "  �� -100,000" + "</color>";
                break;

            case ScheduleType.Business:
                this._resultCommentText.text = "������ �����ߴ�\n\n" + "<color=green>�� +" + a_PlusValue + "</color><color=end>�Ƿε� +20</color>";
                break;

            case ScheduleType.Cheating:
                this._resultCommentText.text = "ġŲ ���ִ�\n\n" + "<color=green>���ŷ� +" + a_PlusValue + "</color>  <color=red>�� -50,000" + "\n�����ٷ� -5</color>";
                break;
        }

        this.Reset_Func();

        int a_CurWeekDay = ScheduleSystem_Manager.s_curWeekDay.ToInt();
        a_CurWeekDay++;
        string a_NextDaySTR = this.Setting_WeekDayUpdate_Func((CurWeekDayType)a_CurWeekDay);

        if((CurWeekDayType)a_CurWeekDay != CurWeekDayType.MAX)
        {
            this._resultNextDayBtnText.text = a_NextDaySTR + " �������";
        }
        else
        {
            this._resultNextDayBtnText.text = "�����ϱ�";
        }

        this._resultObj.SetActive(true);
    }

    private void NextBtnClick_Func()
    {
        this._resultObj.SetActive(false);


        //���� Ȯ���� �̺�Ʈ�� �߻��ߴٸ� �̺�Ʈ ó���� �켱
        if (EventSettingSystem_Manager.Instance.Is_EventCall_Func() == true &&
            EventSettingSystem_Manager.Instance.eventCurCount < DataBase_Manager.Instance.GetTable_Define.event_CountMax)
        {
            //�̺�Ʈ �߻�
            EventSettingSystem_Manager.Instance.EventCall_Func();
        }
        else
        {
            if(EventSettingSystem_Manager.Instance.eventCurCount <= 0)
            {
                //�̺�Ʈ �߻�
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
        //�̺�Ʈ ������Ʈ�� �������ְ� �ش� �̺�Ʈ�� �����ϱ�.
        EventUI_Script.Instance.EventStart_Func(a_CurEventData);
    }

    public void TotalResult_Func()
    {
        //���� �ִϸ��̼� �� ȣ���.
        this._weekDayText.transform.parent.gameObject.SetActive(false);
        this._scheduleTypeText.transform.parent.gameObject.SetActive(false);

        this._totalObj.SetActive(true);
        this._totalResultCorData.StartCoroutine_Func(TotalResult_Cor(), CoroutineStartType.StartWhenStop);
    }

    private IEnumerator TotalResult_Cor()
    {
        this._monthText.text = "<" + GameSystem_Manager.Instance.curweekDay + "�� ���� �ݾ�" + ">";
        this._goToMainBtn.transform.parent.gameObject.SetActive(false);
        this._resultCost.gameObject.SetActive(false);

        {
            UserStatusData a_OldUserStatus = UserSystem_Manager.Instance.status.Get_UserStatus_Func();

            string _oldStr = "";
            _oldStr += "�� " + DataBase_Manager.Instance.GetStrength_Info.Get_BackMovementStrengthInfoDataList_Func(a_OldUserStatus.backMovementSTR) + " / ";
            _oldStr += "���� " + DataBase_Manager.Instance.GetStrength_Info.Get_ChestExercisesStrengthInfoDataList_Func(a_OldUserStatus.chestExercisesSTR) + " / ";
            _oldStr += "��ü " + DataBase_Manager.Instance.GetStrength_Info.Get_LowerBodyExercises_CostStrengthInfoDataList_Func(a_OldUserStatus.lowerBodyExercisesSTR);
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
            _newStr += "�� " + DataBase_Manager.Instance.GetStrength_Info.Get_BackMovementStrengthInfoDataList_Func(a_NewUserStatus.backMovementSTR) + " / ";
            _newStr += "���� " + DataBase_Manager.Instance.GetStrength_Info.Get_ChestExercisesStrengthInfoDataList_Func(a_NewUserStatus.chestExercisesSTR) + " / ";
            _newStr += "��ü " + DataBase_Manager.Instance.GetStrength_Info.Get_LowerBodyExercises_CostStrengthInfoDataList_Func(a_NewUserStatus.lowerBodyExercisesSTR);
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

        }   //�������ͽ�

        {
            this._strengthAnim.Play("Schedule_Str_Anim");
            Sound_Script.Instance.Play_SFX(SFXListType.��Ż�ִϸ��̼�SFX);
            yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            this._stressAnim.Play("Schedule_Stress_Anim");
            Sound_Script.Instance.Play_SFX(SFXListType.��Ż�ִϸ��̼�SFX);
            yield return Coroutine_C.GetWaitForSeconds_Cor(0.5f);
            this._mentalAnim.Play("Schedule_Mental_Anim");
            Sound_Script.Instance.Play_SFX(SFXListType.��Ż�ִϸ��̼�SFX);
        }   //�������ͽ� ���� ����

        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);

        {
            //�ｺ��, �Ĵܺ�, �޽ĺ��, �̺�Ʈ���
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
        }   //����ǥ ���� �� ����

        yield return Coroutine_C.GetWaitForSeconds_Cor(0.65f);
        this._resultCost.text = "���� �ݾ� : " + UserSystem_Manager.Instance.wealth.GetQuantity_Func(WealthType.Money).ToStringLong();
        Sound_Script.Instance.Play_SFX(SFXListType.��Ż���յ���SFX);
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
        this._schedule_TimerText.text = "0.00 ��";
        this._schedule_Time = 0.0f;
        this._curCount = 0;
    }
}
