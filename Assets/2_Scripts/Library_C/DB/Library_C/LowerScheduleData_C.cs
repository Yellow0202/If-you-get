using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

[System.Serializable]
public partial class LowerScheduleData : Data_C
{
     public string Key;
     [LabelText("저중량")] public int Low_Weight;
     [LabelText("보통 중량")] public int Medium_Weight;
     [LabelText("고중량")] public int High_Weight;

    

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(string[] _cellDataArr)
    {
        Key = _cellDataArr[0];
        Low_Weight = _cellDataArr[1].ToInt();
        Medium_Weight = _cellDataArr[2].ToInt();
        High_Weight = _cellDataArr[3].ToInt();
    }
#endif
}