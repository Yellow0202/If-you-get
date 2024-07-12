using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

[System.Serializable]
public partial class EventSelSel_InfoData : Data_C
{
     public string Key;
     [LabelText("선택지")] public string Btn;
     [LabelText("성공여부")] public bool is_True;
     [LabelText("성공확률")] public float Percent;
     [LabelText("설명")] public string Comment;
     [LabelText("스테이터스 변화 문구")] public string Staus_change_Str;
     [LabelText("스테이터스 변화 문구")] public SatusChangeType Status_Change;
     [LabelText("변화하는 스테이터스 종류")] public SatusType StatusType_1;
     [LabelText("변화값 종류")] public SatusValueType Staus_ValueType_1;
     [LabelText("변화값")] public float Staus_Change_Value_1;
     [LabelText("변화하는 스테이터스 종류")] public SatusType StatusType_2;
     [LabelText("변화값 종류")] public SatusValueType Staus_ValueType_2;
     [LabelText("변화값")] public float Staus_Change_Value_2;
     [LabelText("변화하는 스테이터스 종류")] public SatusType StatusType_3;
     [LabelText("변화값 종류")] public SatusValueType Staus_ValueType_3;
     [LabelText("변화값")] public float Staus_Change_Value_3;

    

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(string[] _cellDataArr)
    {
        Key = _cellDataArr[0];
        Btn = _cellDataArr[1];
        is_True = _cellDataArr[2].ToBool();
        Percent = _cellDataArr[3].ToFloat();
        Comment = _cellDataArr[4];
        Staus_change_Str = _cellDataArr[5];
        Status_Change = _cellDataArr[6].ToEnum<SatusChangeType>();
        StatusType_1 = _cellDataArr[7].ToEnum<SatusType>();
        Staus_ValueType_1 = _cellDataArr[8].ToEnum<SatusValueType>();
        Staus_Change_Value_1 = _cellDataArr[9].ToFloat();
        StatusType_2 = _cellDataArr[10].ToEnum<SatusType>();
        Staus_ValueType_2 = _cellDataArr[11].ToEnum<SatusValueType>();
        Staus_Change_Value_2 = _cellDataArr[12].ToFloat();
        StatusType_3 = _cellDataArr[13].ToEnum<SatusType>();
        Staus_ValueType_3 = _cellDataArr[14].ToEnum<SatusValueType>();
        Staus_Change_Value_3 = _cellDataArr[15].ToFloat();
    }
#endif
}