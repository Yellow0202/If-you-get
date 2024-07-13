using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable, InlineProperty, HideLabel]
public partial class LowerScheduleKey : Cargold.FrameWork.TableDataKeyDropdown
{
    [LabelWidth(DropdownDefine.LabelWidth)]
    [SerializeField, LabelText("LowerSchedule Key"), ValueDropdown("CallEdit_KeyDropdown_Func")] private string key = null;

    public string GetKey => this.key;

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Preview")]
    public LowerScheduleData GetData
    {
        get
        {
            DataBase_Manager.Instance.GetLowerSchedule.TryGetData_Func(this.key, out LowerScheduleData _lowerScheduleData);

            return _lowerScheduleData;
        }
    }

    public LowerScheduleKey(string _keyStr = null)
    {
        this.key = _keyStr;
    }

#if UNITY_EDITOR
    private IEnumerable<string> CallEdit_KeyDropdown_Func()
    {
        return DataBase_Manager.Instance.GetLowerSchedule.GetKeyArr;
    }
    public bool CallEdit_IsUnitTestDone_Func()
    {
        if(this.key.IsNullOrWhiteSpace_Func() == false)
            return DataBase_Manager.Instance.GetLowerSchedule.IsContain_Func(this.key);

        return false;
    }
#endif

    public static implicit operator string(LowerScheduleKey _key)
    {
        return _key.key;
    }
}