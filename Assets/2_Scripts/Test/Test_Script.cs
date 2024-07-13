using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class Test_Script : MonoBehaviour
{
    public Image hpBar;

    [SerializeField, FoldoutGroup("�׽�Ʈ �����"), LabelText("���� ī��Ʈ"), ReadOnly] private int curCount;
    [SerializeField, FoldoutGroup("�׽�Ʈ �����"), LabelText("���� �ִ� ü��"), ReadOnly] private float curMAXHP;
    [SerializeField, FoldoutGroup("�׽�Ʈ �����"), LabelText("���� ���ݷ�"), ReadOnly] private float curAttackDmg;
    [SerializeField, FoldoutGroup("�׽�Ʈ �����"), LabelText("���� ���ϰ�"), ReadOnly] private float curDeley;
    [SerializeField, FoldoutGroup("�׽�Ʈ �����"), LabelText("������ �ٷ� ��")] private float curPowerUp;
    [SerializeField, FoldoutGroup("�׽�Ʈ �����"), LabelText("���� ü�°�"), ReadOnly] private float curHP;

    [SerializeField, LabelText("���� �ð� ����")] float mins;

    private float test_power;

    private float time;

    [SerializeField, LabelText("�ҿ�ð�"), ReadOnly]private float curTime;

    // Start is called before the first frame update
    void Start()
    {
        //curMAXHP = DataBase_Manager.Instance.GetTable_Define.level_StartHP;
        //curAttackDmg = curPowerUp / DataBase_Manager.Instance.GetTable_Define.level_PlusAttackDmg;

        //test_power = DataBase_Manager.Instance.GetTable_Define.level_PlusAttackDmg;

        //curDeley = curMAXHP / DataBase_Manager.Instance.GetTable_Define.level_Deley;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            curHP += curAttackDmg / curMAXHP;
            this.hpBar.fillAmount = curHP;
        }

        time += Time.deltaTime;

        if(mins <= time)
        {
            time = 0.0f;
            curHP -= curDeley / curMAXHP;

            if (curHP <= 0.0f)
                curHP = 0.0f;

            this.hpBar.fillAmount = curHP;
        }


        if (0.95f <= curHP)
        {
            this.curHP = 0.0f;
            this.hpBar.fillAmount = 0.0f;
            Test_Plus_Func();
        }
    }

    private void Test_Plus_Func()
    {
        //curMAXHP += DataBase_Manager.Instance.GetTable_Define.level_PlusHP;
        //curDeley = curMAXHP / DataBase_Manager.Instance.GetTable_Define.level_Deley;
        //curTime = 0.0f;

        //curCount++;
    }
}
