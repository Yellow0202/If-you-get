using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

[System.Serializable]
public partial class Measure_infoData : Data_C
{
     public string Key;
     [LabelText("3대측정 무게")] public string Weight_KG;
     [LabelText("등 무게별 체력")] public int BackMovement_HP;

    

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(string[] _cellDataArr)
    {
        Key = _cellDataArr[0];
        Weight_KG = _cellDataArr[1];
        BackMovement_HP = _cellDataArr[2].ToInt();
    }
#endif
}