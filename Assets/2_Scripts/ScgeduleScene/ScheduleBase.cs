using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ScheduleBase : MonoBehaviour
{
    [SerializeField, LabelText("�� ������ Ÿ��")] private ScheduleType _myschedulType; public ScheduleType myschedulType => this._myschedulType;

    public virtual void SchedulStart_Func()
    {

    }
}
