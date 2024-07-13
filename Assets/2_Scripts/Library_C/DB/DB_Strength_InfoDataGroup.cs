using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_Strength_InfoDataGroup
{
    [LabelText("등급컷 리스트")] private List<Strength_InfoData> _strengthInfoDataList;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.Set_StrengthInfoDataList_Func();
    }

    private void Set_StrengthInfoDataList_Func()
    {
        this._strengthInfoDataList = new List<Strength_InfoData>();

        foreach (Strength_InfoData item in dataArr)
        {
            this._strengthInfoDataList.Add(item);
        }
    }

    public string Get_BackMovementStrengthInfoDataList_Func(int a_CurStrength)
    {
        string a_CallBackStr = "F";

        for (int i = 0; i < this._strengthInfoDataList.Count; i++)
        {
            if (a_CurStrength < this._strengthInfoDataList[i].BackMovement_Cost)
            {
                break;
            }
            else
            {
                a_CallBackStr = this._strengthInfoDataList[i].Upgrade;
            }
        }

        return a_CallBackStr;
    }

    public string Get_ChestExercisesStrengthInfoDataList_Func(int a_CurStrength)
    {
        string a_CallBackStr = "F";

        for (int i = 0; i < this._strengthInfoDataList.Count; i++)
        {
            if (a_CurStrength < this._strengthInfoDataList[i].ChestExercises_Cost)
            {
                break;
            }
            else
            {
                a_CallBackStr = this._strengthInfoDataList[i].Upgrade;
            }
        }

        return a_CallBackStr;
    }

    public string Get_LowerBodyExercises_CostStrengthInfoDataList_Func(int a_CurStrength)
    {
        string a_CallBackStr = "F";

        for (int i = 0; i < this._strengthInfoDataList.Count; i++)
        {
            if (a_CurStrength < this._strengthInfoDataList[i].LowerBodyExercises_Cost)
            {
                break;
            }
            else
            {
                a_CallBackStr = this._strengthInfoDataList[i].Upgrade;
            }
        }

        return a_CallBackStr;
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