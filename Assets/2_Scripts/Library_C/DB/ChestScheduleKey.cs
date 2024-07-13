using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable, InlineProperty, HideLabel]
public partial class ChestScheduleKey : Cargold.FrameWork.TableDataKeyDropdown
{
    [LabelWidth(DropdownDefine.LabelWidth)]
    [SerializeField, LabelText("ChestSchedule Key"), ValueDropdown("CallEdit_KeyDropdown_Func")] private string key = null;

    public string GetKey => this.key;

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Preview")]
    public ChestScheduleData GetData
    {
        get
        {
            DataBase_Manager.Instance.GetChestSchedule.TryGetData_Func(this.key, out ChestScheduleData _chestScheduleData);

            return _chestScheduleData;
        }
    }

    public ChestScheduleKey(string _keyStr = null)
    {
        this.key = _keyStr;
    }

#if UNITY_EDITOR
    private IEnumerable<string> CallEdit_KeyDropdown_Func()
    {
        return DataBase_Manager.Instance.GetChestSchedule.GetKeyArr;
    }
    public bool CallEdit_IsUnitTestDone_Func()
    {
        if(this.key.IsNullOrWhiteSpace_Func() == false)
            return DataBase_Manager.Instance.GetChestSchedule.IsContain_Func(this.key);

        return false;
    }
#endif

    public static implicit operator string(ChestScheduleKey _key)
    {
        return _key.key;
    }
}