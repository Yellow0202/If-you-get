using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_Table_DefineDataGroup
{
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("등 운동")] private Sprite _uI_Icon_backMovement; public Sprite uI_Icon_backMovement => this._uI_Icon_backMovement;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("가슴 운동")] private Sprite _uI_Icon_chestExercises; public Sprite uI_Icon_chestExercises => this._uI_Icon_chestExercises;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("하체 운동")] private Sprite _uI_Icon_lowerBodyExercises; public Sprite uI_Icon_lowerBodyExercises => this._uI_Icon_lowerBodyExercises;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("가벼운 휴식")] private Sprite _uI_Icon_lowbreak; public Sprite uI_Icon_lowbreak => this._uI_Icon_lowbreak;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("호화로운 휴식")] private Sprite _uI_Icon_hardbreak; public Sprite uI_Icon_hardbreak => this._uI_Icon_hardbreak;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("업무")] private Sprite _uI_Icon_business; public Sprite uI_Icon_business => this._uI_Icon_business;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("치팅")] private Sprite _uI_Icon_cheating; public Sprite uI_Icon_cheating => this._uI_Icon_cheating;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("빈 공간")] private Sprite _uI_Icon_done; public Sprite uI_Icon_done => this._uI_Icon_done;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("위 키 안눌림")] private Sprite _ui_Icon_UpArrowOut; public Sprite ui_Icon_UpArrowOut => this._ui_Icon_UpArrowOut;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("위 키 눌림")] private Sprite _ui_Icon_UpArrowIn; public Sprite ui_Icon_UpArrowIn => this._ui_Icon_UpArrowIn;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("아래 키 안눌림")] private Sprite _ui_Icon_DownArrowOut; public Sprite ui_Icon_DownArrowOut => this._ui_Icon_DownArrowOut;
    [SerializeField, FoldoutGroup("스케쥴 아이콘"), LabelText("아래 키 눌림")] private Sprite _ui_Icon_DownArrowIn; public Sprite ui_Icon_DownArrowIn => this._ui_Icon_DownArrowIn;


    [SerializeField, FoldoutGroup("레벨 디자인"), LabelText("감소시간(등)")] private float _level_Back_DeleteTime; public float level_Back_DeleteTime => this._level_Back_DeleteTime; 
    [SerializeField, FoldoutGroup("레벨 디자인"), LabelText("감소시간(가슴)")] private float _level_Chest_DeleteTime; public float level_Chest_DeleteTime => this._level_Chest_DeleteTime; 
    [SerializeField, FoldoutGroup("레벨 디자인"), LabelText("감소시간(하체)")] private float _level_LowerBody_DeleteTime; public float level_LowerBody_DeleteTime => this._level_LowerBody_DeleteTime; 
    [SerializeField, FoldoutGroup("레벨 디자인"), LabelText("공격력 증가 조건 값")] private float _level_PlusAttackDmg; public float level_PlusAttackDmg => this._level_PlusAttackDmg;
    [SerializeField, FoldoutGroup("레벨 디자인"), LabelText("부하 값(등)")] private float _level_Back_DeleyValue; public float level_Back_DeleyValue => this._level_Back_DeleyValue;
    [SerializeField, FoldoutGroup("레벨 디자인"), LabelText("부하 값(가슴)")] private float _level_Chest_DeleyValue; public float level_Chest_DeleyValue => this._level_Chest_DeleyValue;
    [SerializeField, FoldoutGroup("레벨 디자인"), LabelText("부하 값(하체)")] private float _level_LowerBody_DeleyValue; public float level_LowerBody_DeleyValue => this._level_LowerBody_DeleyValue;

    [SerializeField, FoldoutGroup("정산"), LabelText("헬스장 비용(고정)")] private int _totalCost_HealthCost; public int totalCost_HealthCost => this._totalCost_HealthCost;
    [SerializeField, FoldoutGroup("정산"), LabelText("식단 비용(고정)")] private int _totalCost_MealsCost; public int totalCost_MealsCost => this._totalCost_MealsCost;

    [SerializeField, FoldoutGroup("이벤트"), LabelText("이벤트 등장 최대 카운트")] private int _event_CountMax; public int event_CountMax => this._event_CountMax;
    [SerializeField, FoldoutGroup("이벤트"), LabelText("이벤트 등장 확률")] private float _event_CallPersent; public float event_CallPersent => this._event_CallPersent;
    [SerializeField, FoldoutGroup("이벤트"), LabelText("조건부 이벤트 등장 확률")] private float _event_StatusPersent; public float event_StatusPersent => this._event_StatusPersent;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */
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