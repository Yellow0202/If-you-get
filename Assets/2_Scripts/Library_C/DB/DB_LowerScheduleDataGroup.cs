using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_LowerScheduleDataGroup
{
    [LabelText("타입 to 체력 리스트")] private Dictionary<HealthValunceType, List<int>> _typeToLowerScheduleDataDic;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.Set_TypeToLowerScheduleDataDic_Func();
    }

    private void Set_TypeToLowerScheduleDataDic_Func()
    {
        this._typeToLowerScheduleDataDic = new Dictionary<HealthValunceType, List<int>>();

        List<int> a_Eesy = new List<int>();
        List<int> a_Nomal = new List<int>();
        List<int> a_Hard = new List<int>();

        foreach (LowerScheduleData item in dataArr)
        {
            a_Eesy.Add(item.Low_Weight);
            a_Nomal.Add(item.Medium_Weight);
            a_Hard.Add(item.High_Weight);
        }

        this._typeToLowerScheduleDataDic.Add(HealthValunceType.Easy, a_Eesy);
        this._typeToLowerScheduleDataDic.Add(HealthValunceType.Nomal, a_Nomal);
        this._typeToLowerScheduleDataDic.Add(HealthValunceType.Hard, a_Hard);
    }

    public List<int> Get_TypeToLowerScheduleDataDic_Func(HealthValunceType a_Type)
    {
        if (this._typeToLowerScheduleDataDic.TryGetValue(a_Type, out List<int> a_Value) == true)
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