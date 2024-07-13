using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable, InlineProperty, HideLabel]
public partial class BackScheduleKey : Cargold.FrameWork.TableDataKeyDropdown
{
    [LabelWidth(DropdownDefine.LabelWidth)]
    [SerializeField, LabelText("BackSchedule Key"), ValueDropdown("CallEdit_KeyDropdown_Func")] private string key = null;

    public string GetKey => this.key;

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Preview")]
    public BackScheduleData GetData
    {
        get
        {
            DataBase_Manager.Instance.GetBackSchedule.TryGetData_Func(this.key, out BackScheduleData _backScheduleData);

            return _backScheduleData;
        }
    }

    public BackScheduleKey(string _keyStr = null)
    {
        this.key = _keyStr;
    }

#if UNITY_EDITOR
    private IEnumerable<string> CallEdit_KeyDropdown_Func()
    {
        return DataBase_Manager.Instance.GetBackSchedule.GetKeyArr;
    }
    public bool CallEdit_IsUnitTestDone_Func()
    {
        if(this.key.IsNullOrWhiteSpace_Func() == false)
            return DataBase_Manager.Instance.GetBackSchedule.IsContain_Func(this.key);

        return false;
    }
#endif

    public static implicit operator string(BackScheduleKey _key)
    {
        return _key.key;
    }
}
