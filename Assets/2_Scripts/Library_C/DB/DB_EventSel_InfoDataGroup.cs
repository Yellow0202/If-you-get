using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_EventSel_InfoDataGroup
{
    [LabelText("이름 별 데이터 딕")] private Dictionary<string, EventSel_InfoData> _nameToEventSelDataDic;


    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.Set_NameToEventSelDataDic_Func();
    }

    private void Set_NameToEventSelDataDic_Func()
    {
        this._nameToEventSelDataDic = new Dictionary<string, EventSel_InfoData>();

        foreach (EventSel_InfoData item in dataArr)
        {
            this._nameToEventSelDataDic.Add(item.Btn, item);
        }
    }

    public EventSel_InfoData Get_NameToEventSelDataDic_Func(string a_BtnName)
    {
        if (this._nameToEventSelDataDic.TryGetValue(a_BtnName, out EventSel_InfoData a_Value) == true)
            return a_Value;
        else
            return null;
    }

#if UNITY_EDITOR
    public override void CallEdit_OnDataImportDone_Func()
    {
        base.CallEdit_OnDataImportDone_Func();

        /* 테이블 임포트가 모두 마무리된 뒤 마지막으로 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */
    }
#endif

}