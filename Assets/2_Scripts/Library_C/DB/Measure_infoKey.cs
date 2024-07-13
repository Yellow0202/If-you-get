using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable, InlineProperty, HideLabel]
public partial class Measure_infoKey : Cargold.FrameWork.TableDataKeyDropdown
{
    [LabelWidth(DropdownDefine.LabelWidth)]
    [SerializeField, LabelText("Measure_info Key"), ValueDropdown("CallEdit_KeyDropdown_Func")] private string key = null;

    public string GetKey => this.key;

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Preview")]
    public Measure_infoData GetData
    {
        get
        {
            DataBase_Manager.Instance.GetMeasure_info.TryGetData_Func(this.key, out Measure_infoData _measure_infoData);

            return _measure_infoData;
        }
    }

    public Measure_infoKey(string _keyStr = null)
    {
        this.key = _keyStr;
    }

#if UNITY_EDITOR
    private IEnumerable<string> CallEdit_KeyDropdown_Func()
    {
        return DataBase_Manager.Instance.GetMeasure_info.GetKeyArr;
    }
    public bool CallEdit_IsUnitTestDone_Func()
    {
        if(this.key.IsNullOrWhiteSpace_Func() == false)
            return DataBase_Manager.Instance.GetMeasure_info.IsContain_Func(this.key);

        return false;
    }
#endif

    public static implicit operator string(Measure_infoKey _key)
    {
        return _key.key;
    }
}
