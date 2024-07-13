using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

[System.Serializable]
public partial class Event_InfoData : Data_C
{
     public string Key;
     [LabelText("이름")] public string Name;
     [LabelText("설명")] public string Comment;
     [LabelText("선택지")] public string[] Btn;
     [LabelText("퍼센트 버튼인지")] public bool is_PersentBtn;
     [LabelText("조건부 이벤트여부")] public bool is_StatusEvent;
     [LabelText("스테이터스 종류")] public StatusType Event_StatusType;
     [LabelText("스테이터스 값")] public float Event_StatusValue;

    

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(string[] _cellDataArr)
    {
        Key = _cellDataArr[0];
        Name = _cellDataArr[1];
        Comment = _cellDataArr[2];
        string[] _strArr3 = _cellDataArr[3].Split(',');
        Btn = new string[_strArr3.Length];
        Btn = _strArr3;
        is_PersentBtn = _cellDataArr[4].ToBool();
        is_StatusEvent = _cellDataArr[5].ToBool();
        Event_StatusType = _cellDataArr[6].ToEnum<StatusType>();
        Event_StatusValue = _cellDataArr[7].ToFloat();
    }
#endif
}