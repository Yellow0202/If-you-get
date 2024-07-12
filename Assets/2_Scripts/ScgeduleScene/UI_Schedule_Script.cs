using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cargold;

public class UI_Schedule_Script : MonoBehaviour
{
    public static UI_Schedule_Script Instance;

    [SerializeField, LabelText("¿Â¿ÀÇÁ ¿ÀºêÁ§Æ®")] private Dictionary<ScheduleType, GameObject> _scheduleTypeToGameObject;

    [FoldoutGroup("Çï½º_µî")]
    [FoldoutGroup("Çï½º_°¡½¿")]
    [FoldoutGroup("Çï½º_ÇÏÃ¼")]
    [FoldoutGroup("ÈÞ½Ä_°¡º­¿î")]
    [FoldoutGroup("ÈÞ½Ä_È­·ÁÇÑ")]
    [FoldoutGroup("¾÷¹«")]
    [FoldoutGroup("Ä¡ÆÃµ¥ÀÌ")]

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
