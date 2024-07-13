using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using UnityEngine.SceneManagement;
using Cargold.Infinite;

public enum CurWeekDayType
{
    Monday = 0,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday,
    MAX
}

public class ScheduleSystem_Manager : SerializedMonoBehaviour, GameSystem_Manager.IInitializer
{
    public static ScheduleSystem_Manager Instance;
    public static CurWeekDayType s_curWeekDay = CurWeekDayType.Monday;
    [SerializeField, LabelText("������ ������"), ReadOnly] private ScheduleClass _curScheduleData; public ScheduleClass curScheduleData => this._curScheduleData;
    [SerializeField, LabelText("�߰��� �ɷ�ġ"), ReadOnly] private PlusStatus _plusStatus; public PlusStatus plusStatus => this._plusStatus;
    [SerializeField, LabelText("������ �� ��ũ��Ʈ")] private List<ScheduleBase> _scheduleScriptDataList;
    [SerializeField, LabelText("������Ÿ�� to ��ũ��Ʈ")] private Dictionary<ScheduleType, ScheduleBase> _scheduleTypeToScriptDataDic;

    [LabelText("������ ���� �ڷ�ƾ ����")] private CoroutineData _scheduleCoroutineData;

    public void Init_Func(int _layer)
    {
        if(_layer == 0)
        {
            Instance = this;

            if(this._curScheduleData == null)
            {
                this._curScheduleData =
                        new ScheduleClass(new ScheduleType[CONSTSTRIONG.INT_BASEDAY], new HealthValunceType[CONSTSTRIONG.INT_BASEDAY]);
            }

            this._plusStatus = new PlusStatus();

            this._scheduleTypeToScriptDataDic = new Dictionary<ScheduleType, ScheduleBase>();

            foreach (ScheduleBase item in this._scheduleScriptDataList)
            {
                this._scheduleTypeToScriptDataDic.Add(item.myschedulType, item);
            }
        }
        else if(_layer == 1)
        {

        }
        else if(_layer == 2)
        {

        }
    }

    public void Start_Schedule_Func()
    {
        //�������� ���۵Ǿ��� ��.
        //���� ���¿� ���� ���¸� �����ؾ� ��.
        this._plusStatus = new PlusStatus();
        this.NextSchedule_Func();
    }

    public void Set_NestWeekDay_Func()
    {
        s_curWeekDay++;
        this.NextSchedule_Func();
    }

    private void NextSchedule_Func()
    {
        //���� ���°� MAX��� ����, �ƴ϶�� ���� ������ ����
        if (s_curWeekDay == CurWeekDayType.MAX)
        {
            //����ó�� �ִϸ��̼� ȣ��.
            //����ó�� �ִϸ��̼��� �ִϸ��̼� ���� �� �Ʒ� ��Ż �Լ��� ȣ�� �� ����.

            UI_Schedule_Script.Instance.TotalResult_Func();

            s_curWeekDay = CurWeekDayType.Monday;
        }
        else
        {
            //������ ������ ����
            ScheduleBase a_CurScheduleScript = this._scheduleTypeToScriptDataDic.GetValue_Func(this._curScheduleData._curScheduleArr[s_curWeekDay.ToInt()]);
            a_CurScheduleScript.SchedulStart_Func();
        }
    }

    public void Set_ScheduleData_Func(ScheduleClass a_CurScheduleData)
    {
        this._curScheduleData = a_CurScheduleData;
        SceneManager.LoadScene("ScheduleScene");
    }

    [System.Serializable]
    public class ScheduleClass
    {
        public ScheduleType[] _curScheduleArr;
        public HealthValunceType[] _curHealthValunceArr;

        public ScheduleClass(ScheduleType[] a_CurScheduleArr, HealthValunceType[] _curHealthValunceArr)
        {
            this._curScheduleArr = a_CurScheduleArr;
            this._curHealthValunceArr = _curHealthValunceArr;
        }
    }

    [System.Serializable]
    public class PlusStatus
    {
        public int mentalCount;
        public int backStr;
        public int chestStr;
        public int lowerbodyStr;
        public float stress;
        public int breakCost;
        public int eventCost;
        public int businessGold;

        public PlusStatus()
        {
            this.mentalCount = 0;
            this.backStr = 0;
            this.chestStr = 0;
            this.lowerbodyStr = 0;
            this.stress = 0;

            this.breakCost = 0;
            this.eventCost = 0;
            this.businessGold = 0;
        }
    }
}
