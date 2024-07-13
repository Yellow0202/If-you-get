using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Cargold.Infinite;

public enum TotalCostType
{
    _Health,
    _Meals,
    _Business,
    _Break,
    _Event,
    MAX
}

public class TotalCostObj_Script : MonoBehaviour
{
    [SerializeField, LabelText("���� �� �ؽ�Ʈ")] private TextMeshProUGUI _costText;
    [SerializeField, LabelText("ȣ�� �ִϸ��̼�")] private Animation _anim;

    [LabelText("���� ����")] UserSystem_Manager.WealthControl _wealthControl;

    public void Call_Func(Infinite a_Cost, TotalCostType a_Type)
    {
        switch(a_Type)
        {
            case TotalCostType._Health:
                this._costText.text = "-  " + a_Cost.ToStringLong() + CONSTSTRIONG.STR_TOTALRESULT_HEALTH;
                this._wealthControl = Cargold.FrameWork.UserSystem_Manager.WealthControl.Spend;
                break;

            case TotalCostType._Meals:
                this._costText.text = "-  " + a_Cost.ToStringLong() + CONSTSTRIONG.STR_TOTALRESULT_MEALS;
                this._wealthControl = Cargold.FrameWork.UserSystem_Manager.WealthControl.Spend;
                break;

            case TotalCostType._Break:
                this._costText.text = "-  " + a_Cost.ToStringLong() + CONSTSTRIONG.STR_TOTALRESULT_BREAK;
                this._wealthControl = Cargold.FrameWork.UserSystem_Manager.WealthControl.Spend;
                break;

            case TotalCostType._Event:
                this._costText.text = "-  " + a_Cost.ToStringLong() + CONSTSTRIONG.STR_TOTALRESULT_EVENT;
                this._wealthControl = Cargold.FrameWork.UserSystem_Manager.WealthControl.Spend;
                break;

            case TotalCostType._Business:
                this._costText.text = "+  " + a_Cost.ToStringLong() + CONSTSTRIONG.STR_TOTALRESULT_BREAK;
                this._wealthControl = Cargold.FrameWork.UserSystem_Manager.WealthControl.Earn;
                break;
        }

        UserSystem_Manager.Instance.wealth.TryGetWealthControl_Func(this._wealthControl, WealthType.Money, a_Cost);

        this.gameObject.SetActive(true);
        this._anim.Play("TotalCost_Text_Anim");
    }
}