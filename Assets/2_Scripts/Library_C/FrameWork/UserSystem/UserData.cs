using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using Cargold.Infinite;

[System.Serializable]
public class UserData : Cargold.FrameWork.UserData_C
{
    public List<UserWealthData> userWealthDataList;

    public UserStatusData userStatusData;
    public UserRecordData userRecordData;

    public UserData()
    {
        this.userWealthDataList = new List<UserWealthData>();

        this.userStatusData = new UserStatusData();
        this.userRecordData = new UserRecordData();
    }
}

#region Wealth
[System.Serializable]
public class UserWealthData : Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, Infinite>.IWealthData
{
    public WealthType wealthType;
    public Infinite quantity;

    public UserWealthData(WealthType _wealthType, Infinite _quantity)
    {
        this.wealthType = _wealthType;
        this.quantity = _quantity;
    }

    WealthType Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, Infinite>.IWealthData.GetWealthType => this.wealthType;
    Infinite Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, Infinite>.IWealthData.GetQuantity => this.quantity;

    void Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, Infinite>.IWealthData.AddQuantity_Func(Infinite _quantity)
    {
        this.quantity += _quantity;
    }

    void Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, Infinite>.IWealthData.SetQuantity_Func(Infinite _quantity)
    {
        this.quantity = _quantity;
    }

    bool Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, Infinite>.IWealthData.TryGetSubtract_Func(Infinite _quantity, bool _isJustCheck)
    {
        if (_quantity <= this.quantity)
        {
            if (_isJustCheck == false)
                this.quantity -= _quantity;

            return true;
        }
        else
        {
            return false;
        }
    }
}
#endregion
#region UserStatusData
[System.Serializable]
public class UserStatusData
{
    public int mentality;
    public int backMovementSTR;
    public int chestExercisesSTR;
    public int lowerBodyExercisesSTR;
    public float stress;

    public UserStatusData(int a_Mentality = 3, int a_backMovementSTR = 60, int a_chestExercisesSTR = 45, int a_lowerBodyExercisesSTR = 50, float fatiguelevel = 0.0f)
    {
        this.mentality = a_Mentality;
        this.backMovementSTR = a_backMovementSTR;
        this.chestExercisesSTR = a_chestExercisesSTR;
        this.lowerBodyExercisesSTR = a_lowerBodyExercisesSTR;
        this.stress = fatiguelevel;
    }
}
#endregion

#region UserStatusData
[System.Serializable]
public class UserRecordData
{
    public int backMovement;
    public int chestExercises;
    public int lowerBodyExercises;

    public UserRecordData(int a_back = 20, int a_chest = 20, int a_lower = 20)
    {
        this.backMovement = a_back;
        this.chestExercises = a_chest;
        this.lowerBodyExercises = a_lower;
    }
}
#endregion
