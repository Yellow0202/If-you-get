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

    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("컨셉별 애니메이션 온/오프")] private Dictionary<ScheduleType, GameObject> _typeToAnimObjDataDic;

    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("요일 텍스트")] private TextMeshProUGUI _weekDayText;
    [SerializeField, FoldoutGroup("스케쥴 별 변경될 값"), LabelText("요일 텍스트")] private TextMeshProUGUI _scheduleTypeText;

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
