using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cargold;

public class UI_Schedule_Script : MonoBehaviour
{
    public static UI_Schedule_Script Instance;

    [SerializeField, LabelText("�¿��� ������Ʈ")] private Dictionary<ScheduleType, GameObject> _scheduleTypeToGameObject;

    [FoldoutGroup("�ｺ_��")]
    [FoldoutGroup("�ｺ_����")]
    [FoldoutGroup("�ｺ_��ü")]
    [FoldoutGroup("�޽�_������")]
    [FoldoutGroup("�޽�_ȭ����")]
    [FoldoutGroup("����")]
    [FoldoutGroup("ġ�õ���")]

    private void Awake()
    {
        Instance = this;

        foreach (KeyValuePair<ScheduleType, GameObject> item in this._scheduleTypeToGameObject)
        {
            item.Value.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setting_OnOffObject_Func(ScheduleType a_CurType)
    {
        GameObject a_CurTypeObj = this._scheduleTypeToGameObject.GetValue_Func(a_CurType);
        a_CurTypeObj.SetActive(true);
    }
}
