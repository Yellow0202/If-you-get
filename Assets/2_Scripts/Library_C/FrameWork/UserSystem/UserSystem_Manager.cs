using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using Cargold.Remocon;
using Cargold.FrameWork;
using Cargold.Infinite;

public class UserSystem_Manager : Cargold.FrameWork.UserSystem_Manager
{
    public static new UserSystem_Manager Instance;

    [SerializeField] private UserData userData = null;

    public Common common;
    public Log log;
    public Wealth wealth;
    public Status status;
    public Record record;

    public override Common_C GetCommon => this.common;
    public override Log_C GetLog => this.log;
    public override UserData_C GetUserData => this.userData;

    public override void Init_Func(int _layer)
    {
        base.Init_Func(_layer);

        if(_layer == 0)
        {
            Instance = this;
        }

        this.wealth.Init_Func(_layer);
        this.status.Init_Func(_layer);
        this.record.Init_Func(_layer);
    }

    protected override void OnLoadUserDataStr_Func(string _userDataStr)
    {
        UserData _userData = null;
#if UNITY_2020_1_OR_NEWER
        _userData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserData>(_userDataStr);
#else
        _userData = JsonUtility.FromJson<UserData>(_userDataStr);
#endif

        _userData.version = ProjectRemocon.Instance.buildSystem.GetVersion_Func();

        base.SetUserData_Func(_userData);

        this.userData = _userData;
    }

    protected override void OnLoadDefaultUserData_Func(UserData_C _userDataC)
    {
        UserData _userData = _userDataC as UserData;

        base.SetUserData_Func(_userData);

        this.userData = _userData;
    }

    #region Common
    [System.Serializable]
    public class Common : Common_C
    {

    } 
    #endregion
    #region Log
    [System.Serializable]
    public class Log : Log_C
    {

    }
        #endregion
    #region Wealth
    [System.Serializable]
    public class Wealth : Wealth_C<WealthType, Infinite>
    {
        public override void Init_Func(int _layer)
        {
            base.Init_Func(_layer);

            if(_layer == 1)
            {
                UserData _userData = UserSystem_Manager.Instance.GetUserData as UserData;
                base.SetData_Func(_userData.userWealthDataList);
            }
        }

        protected override IWealthData GenerateUserWealthData_Func(WealthType _itemType, Infinite _quantity)
        {
            UserWealthData _userWealthData = new UserWealthData(_itemType, _quantity);

            Instance.userData.userWealthDataList.Add(_userWealthData);

            if (Cargold.SaveSystem.SaveSystem_Manager.Instance is null == false)
                Cargold.SaveSystem.SaveSystem_Manager.Instance.Save_Func();

            return _userWealthData;
        }
    }
    #endregion
    #region Status
    [System.Serializable]
    public class Status
    {
        private UserStatusData GetData => Instance.userData.userStatusData;

        public void Init_Func(int _layer)
        {
            if (_layer == 0)
            {

            }
        }

        public UserStatusData Get_UserStatus_Func()
        {
            UserStatusData a_Data = this.GetData;
            return a_Data;
        }

        public void Set_MentalStatus_Func(int a_MentalCount)
        {
            this.GetData.mentality += a_MentalCount;

            if (3 < this.GetData.mentality)
                this.GetData.mentality = 3;
            else if (this.GetData.mentality < 0)
                this.GetData.mentality = 0;

        }

        public void Set_BackMovementSTR_Func(int a_BackMovementSTR)
        {
            this.GetData.backMovementSTR += a_BackMovementSTR;
        }

        public void Set_ChestExercisesSTR_Func(int a_ChestExercisesSTR)
        {
            this.GetData.chestExercisesSTR += a_ChestExercisesSTR;
        }

        public void Set_LowerBodyExercisesSTR_Func(int a_LowerBodyExercisesSTR)
        {
            this.GetData.lowerBodyExercisesSTR += a_LowerBodyExercisesSTR;
        }

        public void Set_Stress_Func(float a_Stress)
        {
            this.GetData.stress += a_Stress;

            if (200 < this.GetData.stress)
                this.GetData.stress = 200;
            else if (this.GetData.stress < 0)
                this.GetData.stress = 0;
        }
    }
    #endregion

    #region Record
    [System.Serializable]
    public class Record
    {
        private UserRecordData GetData => Instance.userData.userRecordData;

        public void Init_Func(int _layer)
        {
            if (_layer == 0)
            {

            }
        }

        public void Set_ClearBackMovement_Func()
        {
            this.GetData.backMovement += 20;
        }

        public void Set_ClearChestExercises_Func()
        {
            this.GetData.chestExercises += 20;
        }

        public void Set_ClearLowerBodyExercises_Func()
        {
            this.GetData.lowerBodyExercises += 20;
        }

        public int Get_BackMovement_Func()
        {
            return this.GetData.backMovement;
        }

        public int Get_ChestExercises_Func()
        {
            return this.GetData.chestExercises;
        }

        public int Get_LowerBodyExercises_Func()
        {
            return this.GetData.lowerBodyExercises;
        }
    }
    #endregion
}
