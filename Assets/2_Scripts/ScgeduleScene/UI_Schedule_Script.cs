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

    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("������ �ִϸ��̼� ��/����")] private Dictionary<ScheduleType, GameObject> _typeToAnimObjDataDic;

    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("���� �ؽ�Ʈ")] private TextMeshProUGUI _weekDayText;
    [SerializeField, FoldoutGroup("������ �� ����� ��"), LabelText("���� �ؽ�Ʈ")] private TextMeshProUGUI _scheduleTypeText;

    private void Awake()
    {
        Instance = this;
        this._onoffGameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setting_OnOffObject_Func(ScheduleType a_CurType)
    {
        switch(a_CurType)
        {
            case ScheduleType.BackMovement:

                break;

            case ScheduleType.ChestExercises:

                break;

            case ScheduleType.LowerBodyExercises:

                break;

            case ScheduleType.Lowbreak:

                break;

            case ScheduleType.Hardbreak:

                break;

            case ScheduleType.Business:

                break;

            case ScheduleType.Cheating:

                break;

            default:
                break;
        }

        this._onoffGameObject.SetActive(true);
    }
}
