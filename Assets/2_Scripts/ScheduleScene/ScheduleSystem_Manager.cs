using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using UnityEngine.SceneManagement;

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
    [SerializeField, LabelText("스케쥴 데이터"), ReadOnly] private ScheduleClass _curScheduleData; public ScheduleClass curScheduleData => this.curScheduleData;
    [SerializeField, LabelText("스케쥴 별 스크립트")] private List<ScheduleBase> _scheduleScriptDataList;
    [SerializeField, LabelText("스케쥴타입 to 스크립트")] private Dictionary<ScheduleType, ScheduleBase> _scheduleTypeToScriptDataDic;

    [LabelText("스케쥴 관리 코루틴 변수")] private CoroutineData _scheduleCoroutineData;

    public void Init_Func(int _layer)
    {
        if(_layer == 0)
        {
            Instance = this;
            this._curScheduleData =
                new ScheduleClass(new ScheduleType[DataBase_Manager.Instance.GetTable_Define.playDayData], new HealthValunceType[DataBase_Manager.Instance.GetTable_Define.playDayData]);

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
        //스케쥴이 시작되었을 때.
        //현재 상태에 맞춰 상태를 전개해야 함.
        Debug.Log("시작됨");
        this.NextSchedule_Func();
    }

    public void Set_NestWeekDay_Func()
    {
        s_curWeekDay++;
        this.NextSchedule_Func();
    }

    private void NextSchedule_Func()
    {
        //현재 상태가 MAX라면 정산, 아니라면 다음 스케쥴 시작
        if (s_curWeekDay == CurWeekDayType.MAX)
        {
            //정산처리

            s_curWeekDay = CurWeekDayType.Monday;
        }
        else
        {
            //다음날 스케쥴 시행
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
}
