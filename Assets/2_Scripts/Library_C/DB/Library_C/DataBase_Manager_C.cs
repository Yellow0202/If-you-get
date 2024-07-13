using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DataBase_Manager : Cargold.FrameWork.DataBase_Manager
{
    private static DataBase_Manager instance;
    public static new DataBase_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<DataBase_Manager>("DataBase_Manager");
            }

            return instance;
        }
    }

    [SerializeField] private Debug_C.PrintLogType printLogType = Debug_C.PrintLogType.Common;
    #region Variable
    
    [InlineEditor, LabelText("Event_Info"), SerializeField] private DB_Event_InfoDataGroup event_Info;
    public DB_Event_InfoDataGroup GetEvent_Info
    {
        get
        {
            if (this.event_Info == null)
                this.event_Info = Resources.Load<DB_Event_InfoDataGroup>(base.dataGroupSobjPath + "DB_Event_InfoDataGroup");

            return this.event_Info;
        }
    }
    [InlineEditor, LabelText("EventSel_Info"), SerializeField] private DB_EventSel_InfoDataGroup eventSel_Info;
    public DB_EventSel_InfoDataGroup GetEventSel_Info
    {
        get
        {
            if (this.eventSel_Info == null)
                this.eventSel_Info = Resources.Load<DB_EventSel_InfoDataGroup>(base.dataGroupSobjPath + "DB_EventSel_InfoDataGroup");

            return this.eventSel_Info;
        }
    }
    [InlineEditor, LabelText("EventSelP_Info"), SerializeField] private DB_EventSelP_InfoDataGroup eventSelP_Info;
    public DB_EventSelP_InfoDataGroup GetEventSelP_Info
    {
        get
        {
            if (this.eventSelP_Info == null)
                this.eventSelP_Info = Resources.Load<DB_EventSelP_InfoDataGroup>(base.dataGroupSobjPath + "DB_EventSelP_InfoDataGroup");

            return this.eventSelP_Info;
        }
    }
    [InlineEditor, LabelText("Strength_Info"), SerializeField] private DB_Strength_InfoDataGroup strength_Info;
    public DB_Strength_InfoDataGroup GetStrength_Info
    {
        get
        {
            if (this.strength_Info == null)
                this.strength_Info = Resources.Load<DB_Strength_InfoDataGroup>(base.dataGroupSobjPath + "DB_Strength_InfoDataGroup");

            return this.strength_Info;
        }
    }
    [InlineEditor, LabelText("Measure_info"), SerializeField] private DB_Measure_infoDataGroup measure_info;
    public DB_Measure_infoDataGroup GetMeasure_info
    {
        get
        {
            if (this.measure_info == null)
                this.measure_info = Resources.Load<DB_Measure_infoDataGroup>(base.dataGroupSobjPath + "DB_Measure_infoDataGroup");

            return this.measure_info;
        }
    }
    [InlineEditor, LabelText("Localize"), SerializeField] private DB_LocalizeDataGroup localize;
    public DB_LocalizeDataGroup GetLocalize
    {
        get
        {
            if (this.localize == null)
                this.localize = Resources.Load<DB_LocalizeDataGroup>(base.dataGroupSobjPath + "DB_LocalizeDataGroup");

            return this.localize;
        }
    }
    [InlineEditor, LabelText("Table_Define"), SerializeField] private DB_Table_DefineDataGroup table_Define;
    public DB_Table_DefineDataGroup GetTable_Define
    {
        get
        {
            if (this.table_Define == null)
                this.table_Define = Resources.Load<DB_Table_DefineDataGroup>(base.dataGroupSobjPath + "DB_Table_DefineDataGroup");

            return this.table_Define;
        }
    }
    #endregion

    #region Library
    
            public override Cargold.FrameWork.IDB_Localize GetLocalize_C => this.localize;
            
    #endregion

    protected override Debug_C.PrintLogType GetPrintLogType => this.printLogType;

    public override void Init_Func(int _layer)
    {
        base.Init_Func(_layer);
        
        if(_layer == 0)
        {
            Debug_C.Init_Func(this);

            
            this.event_Info.Init_Func();
            this.eventSel_Info.Init_Func();
            this.eventSelP_Info.Init_Func();
            this.strength_Info.Init_Func();
            this.measure_info.Init_Func();
            this.localize.Init_Func();
            this.table_Define.Init_Func();
        }
    }

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(bool _isDataImport = true)
    {
        this.GetEvent_Info.CallEdit_OnDataImportDone_Func();
        this.GetEventSel_Info.CallEdit_OnDataImportDone_Func();
        this.GetEventSelP_Info.CallEdit_OnDataImportDone_Func();
        this.GetStrength_Info.CallEdit_OnDataImportDone_Func();
        this.GetMeasure_info.CallEdit_OnDataImportDone_Func();
        this.GetLocalize.CallEdit_OnDataImportDone_Func();
        this.GetTable_Define.CallEdit_OnDataImportDone_Func();
        
        base.CallEdit_OnDataImport_Func();
    }
#endif
}