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

    public UserData()
    {
        this.userWealthDataList = new List<UserWealthData>();

        this.userStatusData = new UserStatusData();
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
#region
[System.Serializable]
public class UserStatusData
{
    public int mentality;
    public int backMovementSTR;
    public int chestExercisesSTR;
    public int lowerBodyExercisesSTR;
    public float stress;

    public UserStatusData(int a_Mentality = 3, int a_backMovementSTR = 69, int a_chestExercisesSTR = 51, int a_lowerBodyExercisesSTR = 69, float fatiguelevel = 0.0f)
    {
        this.mentality = a_Mentality;
        this.backMovementSTR = a_backMovementSTR;
        this.chestExercisesSTR = a_chestExercisesSTR;
        this.lowerBodyExercisesSTR = a_lowerBodyExercisesSTR;
        this.stress = fatiguelevel;
    }
}
#endregion
