using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable, InlineProperty, HideLabel]
public partial class EventSel_InfoKey : Cargold.FrameWork.TableDataKeyDropdown
{
    [LabelWidth(DropdownDefine.LabelWidth)]
    [SerializeField, LabelText("EventSel_Info Key"), ValueDropdown("CallEdit_KeyDropdown_Func")] private string key = null;

    public string GetKey => this.key;

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Preview")]
    public EventSel_InfoData GetData
    {
        get
        {
            DataBase_Manager.Instance.GetEventSel_Info.TryGetData_Func(this.key, out EventSel_InfoData _eventSel_InfoData);

            return _eventSel_InfoData;
        }
    }

    public EventSel_InfoKey(string _keyStr = null)
    {
        this.key = _keyStr;
    }

#if UNITY_EDITOR
    private IEnumerable<string> CallEdit_KeyDropdown_Func()
    {
        return DataBase_Manager.Instance.GetEventSel_Info.GetKeyArr;
    }
    public bool CallEdit_IsUnitTestDone_Func()
    {
        if(this.key.IsNullOrWhiteSpace_Func() == false)
            return DataBase_Manager.Instance.GetEventSel_Info.IsContain_Func(this.key);

        return false;
    }
#endif

    public static implicit operator string(EventSel_InfoKey _key)
    {
        return _key.key;
    }
}
