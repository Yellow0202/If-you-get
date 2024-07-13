using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_EventSelP_InfoDataGroup
{
    [LabelText("이름 별 데이터 딕")] private Dictionary<string, List<EventSelP_InfoData>> _nameToEventSelPDataDic;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.Set_NameToEventSelPDataDic_Func();
    }

    private void Set_NameToEventSelPDataDic_Func()
    {
        this._nameToEventSelPDataDic = new Dictionary<string, List<EventSelP_InfoData>>();

        foreach (EventSelP_InfoData item in dataArr)
        {
            if(this._nameToEventSelPDataDic.TryGetValue(item.Btn, out List<EventSelP_InfoData> a_ValueList) == true)
            {
                a_ValueList.Add(item);
            }
            else
            {
                List<EventSelP_InfoData> a_NewValueList = new List<EventSelP_InfoData>();
                a_NewValueList.Add(item);

                this._nameToEventSelPDataDic.Add(item.Btn, a_NewValueList);
            }
        }
    }

    public List<EventSelP_InfoData> Get_NameToEventSelPDataDic_Func(string a_BtnName)
    {
        if (this._nameToEventSelPDataDic.TryGetValue(a_BtnName, out List<EventSelP_InfoData> a_Value) == true)
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