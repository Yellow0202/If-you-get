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

    public UserData()
    {
        this.userWealthDataList = new List<UserWealthData>();
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
