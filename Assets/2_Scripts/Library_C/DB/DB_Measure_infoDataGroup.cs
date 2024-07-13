using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_Measure_infoDataGroup
{
    [LabelText("무게 별 각각 체력")] private Dictionary<int, List<int>> _kgToMaxHpListDataDic;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.Set_KgToMaxHpListDataDic_Func();
    }

    private void Set_KgToMaxHpListDataDic_Func()
    {
        this._kgToMaxHpListDataDic = new Dictionary<int, List<int>>();

        foreach (Measure_infoData item in dataArr)
        {
            List<int> a_List = new List<int>();
            a_List.Add(item.BackMovement_HP);
            a_List.Add(item.ChestExercises_HP);
            a_List.Add(item.LowerBodyExercises_HP);

            this._kgToMaxHpListDataDic.Add(item.Weight_KG, a_List);
        }
    }

    public int Get_BackMovement_HP_Func(int a_KG)
    {
        if (this._kgToMaxHpListDataDic.TryGetValue(a_KG, out List<int> a_Value) == true)
            return a_Value[0];
        else
            return -1;
    }

    public int Get_ChestExercises_HP_Func(int a_KG)
    {
        if (this._kgToMaxHpListDataDic.TryGetValue(a_KG, out List<int> a_Value) == true)
            return a_Value[1];
        else
            return -1;
    }

    public int Get_LowerBodyExercises_HP_Func(int a_KG)
    {
        if (this._kgToMaxHpListDataDic.TryGetValue(a_KG, out List<int> a_Value) == true)
            return a_Value[2];
        else
            return -1;
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