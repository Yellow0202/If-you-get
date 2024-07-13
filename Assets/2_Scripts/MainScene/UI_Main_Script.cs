using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.XR;
using static ScheduleSystem_Manager;

public enum ScheduleType
{
    BackMovement = 0,
    ChestExercises,
    LowerBodyExercises,
    Lowbreak,
    Hardbreak,
    Business,
    Cheating,
    Done,
    MAX
}

public enum HealthValunceType
{ 
    Easy = 0,
    Nomal,
    Hard,
    Done,
    MAX
}

public class UI_Main_Script : MonoBehaviour
{
    public static UI_Main_Script Instance;

    [SerializeField, FoldoutGroup("������"), LabelText("������ �̹��� ����Ʈ")] private List<Image> _scheduleIconDataList;
    [SerializeField, FoldoutGroup("������"), LabelText("������ �����")] private Button _scheduleBackBtn;
    [SerializeField, FoldoutGroup("������"), LabelText("���� ������ �迭 ����")] private ScheduleType[] _curScheduleArr;
    [SerializeField, FoldoutGroup("������"), LabelText("���� ������ ���̵� ����")] private HealthValunceType[] _curHealthValunceArr;
    [SerializeField, FoldoutGroup("������"), LabelText("���� ���õ� ���̵�")] private HealthValunceType _selHealthValunce;
    [FoldoutGroup("������"), LabelText("������� ��ϵ� ������")] private int _curScheduleNum;

    [SerializeField, FoldoutGroup("����"), LabelText("� ��ư")] private List<Button> _healthBtnDataList;
    [SerializeField, FoldoutGroup("����"), LabelText("�޽� ��ư")] private List<Button> _breakBtnDataList;
    [SerializeField, FoldoutGroup("����"), LabelText("���� ��ư")] private Button _businessBtn;
    [SerializeField, FoldoutGroup("����"), LabelText("ġ�� ��ư")] private Button _cheatBtn;
    [SerializeField, FoldoutGroup("����"), LabelText("� �뷱�� ���� ��ư")] private List<Button> _valunceBtnDataList;

    [SerializeField, LabelText("���� ��ư")] private Button _scheduleStartBtn;

    private void Awake()
    {
        Instance = this;
        this._curScheduleArr = new ScheduleType[CONSTSTRIONG.INT_BASEDAY];
        this._curHealthValunceArr = new HealthValunceType[CONSTSTRIONG.INT_BASEDAY];
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Start_MainUI_Func();
    }

    private void Start_MainUI_Func()
    {
        this.Set_BtnValue_Func();

        this._curScheduleNum = -1;
    }

    #region ������ ���� ��ư
    private void Set_BtnValue_Func()
    {
        this.Set_ScheduleBackBtn_Func();
        this.Set_ValunceBtnDataList_Func();
        this.Set_HealthBtnDataList_Func();
        this.Set_BreakBtnDataList_Func();
        this.Set_BusinessBtn_Func();
        this.Set_CheatBtn_Func();

        this._scheduleStartBtn.onClick.AddListener(Click_SchedulePlayStart_Func);
    }

    private void Set_ScheduleBackBtn_Func()
    {
        this._scheduleBackBtn.onClick.AddListener(() =>
        {
            this.Set_ScheduleMius_Func();
        });
    }

    private void Set_ValunceBtnDataList_Func()
    {
        for (int i = 0; i < this._valunceBtnDataList.Count; i++)
        {
            int a_HealthValType = i;

            this._valunceBtnDataList[i].onClick.AddListener(() =>
            {
                this._selHealthValunce = (HealthValunceType)a_HealthValType;
            });
        }
    }

    private void Set_HealthBtnDataList_Func()
    {
        for (int i = 0; i < this._healthBtnDataList.Count; i++)
        {
            int a_SelNum = i;
            this._healthBtnDataList[i].onClick.AddListener(() =>
            {
                this.Set_SchedulePlus_Func((ScheduleType)a_SelNum);
            });
        }
    }

    private void Set_BreakBtnDataList_Func()
    {
        for (int i = 0; i < this._breakBtnDataList.Count; i++)
        {
            int a_SelNum = i + 3;
            this._breakBtnDataList[i].onClick.AddListener(() =>
            {
                this.Set_SchedulePlus_Func((ScheduleType)a_SelNum);
            });
        }
    }

    private void Set_BusinessBtn_Func()
    {
        this._businessBtn.onClick.AddListener(() =>
        {
            this.Set_SchedulePlus_Func(ScheduleType.Business);
        });
    }

    private void Set_CheatBtn_Func()
    {
        this._cheatBtn.onClick.AddListener(() =>
        {
            this.Set_SchedulePlus_Func(ScheduleType.Cheating);
        });
    }

    private void Set_SchedulePlus_Func(ScheduleType a_scheduleType)
    {
        if (this._curScheduleArr.Length - 1 <= this._curScheduleNum)
            return;

        //if (this._curScheduleNum < 0)
        //    this._curScheduleNum = 0;

        this._curScheduleNum++;

        this._curScheduleArr[this._curScheduleNum] = a_scheduleType;

        if(a_scheduleType == ScheduleType.BackMovement ||
           a_scheduleType == ScheduleType.ChestExercises ||
           a_scheduleType == ScheduleType.LowerBodyExercises)
        {
            this._curHealthValunceArr[this._curScheduleNum] = this._selHealthValunce;
        }
        else
        {
            this._curHealthValunceArr[this._curScheduleNum] = HealthValunceType.Done;
        }

        this.Set_ScheduleSpriteChange_Func(a_scheduleType);
    }

    private void Set_ScheduleMius_Func()
    {
        if (this._curScheduleNum < 0)
            return;

        //if (this._curScheduleArr.Length - 1 <= this._curScheduleNum)
        //    this._curScheduleNum = this._curScheduleArr.Length - 1;

        this._curScheduleArr[this._curScheduleNum] = ScheduleType.Done;
        this._curHealthValunceArr[this._curScheduleNum] = HealthValunceType.Done;
        this.Set_ScheduleSpriteChange_Func(ScheduleType.Done);

        this._curScheduleNum--;
    }

    private void Set_ScheduleSpriteChange_Func(ScheduleType a_scheduleType)
    {
        switch(a_scheduleType)
        {
            case ScheduleType.Done:
                this._scheduleIconDataList[this._curScheduleNum].sprite = DataBase_Manager.Instance.GetTable_Define.uI_Icon_done;
                break;

            case ScheduleType.BackMovement:
                this._scheduleIconDataList[this._curScheduleNum].sprite = DataBase_Manager.Instance.GetTable_Define.uI_Icon_backMovement;
                break;

            case ScheduleType.ChestExercises:
                this._scheduleIconDataList[this._curScheduleNum].sprite = DataBase_Manager.Instance.GetTable_Define.uI_Icon_chestExercises;
                break;

            case ScheduleType.LowerBodyExercises:
                this._scheduleIconDataList[this._curScheduleNum].sprite = DataBase_Manager.Instance.GetTable_Define.uI_Icon_lowerBodyExercises;
                break;

            case ScheduleType.Lowbreak:
                this._scheduleIconDataList[this._curScheduleNum].sprite = DataBase_Manager.Instance.GetTable_Define.uI_Icon_lowbreak;
                break;

            case ScheduleType.Hardbreak:
                this._scheduleIconDataList[this._curScheduleNum].sprite = DataBase_Manager.Instance.GetTable_Define.uI_Icon_hardbreak;
                break;

            case ScheduleType.Business:
                this._scheduleIconDataList[this._curScheduleNum].sprite = DataBase_Manager.Instance.GetTable_Define.uI_Icon_business;
                break;

            case ScheduleType.Cheating:
                this._scheduleIconDataList[this._curScheduleNum].sprite = DataBase_Manager.Instance.GetTable_Define.uI_Icon_cheating;
                break;

            default:
                break;
        }
    }

    #endregion

    private void Click_SchedulePlayStart_Func()
    {
        if (this._curScheduleNum < this._curScheduleArr.Length - 1)
            return;

        ScheduleSystem_Manager.ScheduleClass a_CurSelSchedulData = new ScheduleClass(this._curScheduleArr, this._curHealthValunceArr);
        ScheduleSystem_Manager.Instance.Set_ScheduleData_Func(a_CurSelSchedulData);
    }

}
