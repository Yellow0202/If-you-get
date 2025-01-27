using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

[System.Serializable]
public partial class Strength_InfoData : Data_C
{
     public string Key;
     [LabelText("등급")] public string Upgrade;
     [LabelText("등 등급컷")] public int BackMovement_Cost;
     [LabelText("가슴 등급컷")] public int ChestExercises_Cost;
     [LabelText("하체 등급컷")] public int LowerBodyExercises_Cost;

    

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(string[] _cellDataArr)
    {
        Key = _cellDataArr[0];
        Upgrade = _cellDataArr[1];
        BackMovement_Cost = _cellDataArr[2].ToInt();
        ChestExercises_Cost = _cellDataArr[3].ToInt();
        LowerBodyExercises_Cost = _cellDataArr[4].ToInt();
    }
#endif
}