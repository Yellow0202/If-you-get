using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
using Cargold;
using UnityEngine.SceneManagement;

public class UI_MeasurementScene_Script : MonoBehaviour
{
    //해당 씬 도착시 3대 측정 카운트 시작.
    //초기값 셋팅.

    //순서
    //데드리프트 -> 벤치 -> 스쿼트

    public static UI_MeasurementScene_Script Instance;

    [LabelText("현재 반복된 순서")] private int _curNumber = 0;

    [SerializeField, FoldoutGroup("스케쥴"), LabelText("게이지 이미지")] private Image _schedule_GageBar;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("게이지 스크롤 바")] private Scrollbar _schedule_Scrollbar;

    [SerializeField, FoldoutGroup("스케쥴"), LabelText("키 이미지")] private Image _schedule_KeyImg; public Image schedule_KeyImg => this._schedule_KeyImg;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("눌리기 전 스프라이트"), ReadOnly] private Sprite _schedule_Out_Sprite;
    [SerializeField, FoldoutGroup("스케쥴"), LabelText("눌리기 후 스프라이트"), ReadOnly] private Sprite _schedule_In_Sprite;

    [SerializeField, FoldoutGroup("결과지"), LabelText("결과 오브젝트")] private GameObject _resultObj;
    [SerializeField, FoldoutGroup("결과지"), LabelText("결과 텍스트")] private TextMeshProUGUI _resultText;
    [SerializeField, FoldoutGroup("결과지"), LabelText("다음씬으로 이동")] private Button _gotoMainBtn;

    [SerializeField, LabelText("UI용 애니메이션")] private Animation _uiAniml;
    [SerializeField, LabelText("종목 텍스트")] private TextMeshProUGUI _typeNameText;
    [SerializeField, LabelText("시작 텍스트")] private TextMeshProUGUI _curRecordText;
    [SerializeField, LabelText("카운트 텍스트")] private TextMeshProUGUI _countDownText;
    [SerializeField, LabelText("상단 표기 텍스트")] private TextMeshProUGUI _upUIText;
    [SerializeField, LabelText("시간 텍스트")] private TextMeshProUGUI _timerText;

    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("타이머"), ReadOnly] private float _schedule_Time;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("감소 타이머"), ReadOnly] private float _schedule_DeleteTime;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("최대 체력"), ReadOnly] private float _MaxHPGage;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("현재 체력"), ReadOnly] private float _curHPGage;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("현재 부하량"), ReadOnly] private float _curDeleyValue;
    [SerializeField, FoldoutGroup("운동 게이지 정보"), LabelText("현재 데미지"), ReadOnly] private float _curDamage;
    [LabelText("현재 운동 체력 정보")] private List<int> _curHealthHpData;

    [LabelText("관리용 코루틴 변수")] private CoroutineData _corData;

    private void Awake()
    {
        Instance = this;
        this._resultObj.SetActive(false);
        this._gotoMainBtn.onClick.AddListener(() => { SceneManager.LoadScene("0.MainScene_2"); });

        Sound_Script.Instance.Play_BGM(BGMListType.삼대측정BGM);
        this.Setting_Func();
    }

    public void Setting_Func()
    {
        if (this._curNumber == 0)
        {
            this._typeNameText.text = CONSTSTRIONG.STR_DEADLIFT;
            this._curRecordText.text = UserSystem_Manager.Instance.record.Get_BackMovement_Func() + "KG";
        }
        else if(this._curNumber == 1)
        {
            this._typeNameText.text = CONSTSTRIONG.STR_BENCHPRESS;
            this._curRecordText.text = UserSystem_Manager.Instance.record.Get_ChestExercises_Func() + "KG";
        }
        else if(this._curNumber == 2)
        {
            this._typeNameText.text = CONSTSTRIONG.STR_SQUAT;
            this._curRecordText.text = UserSystem_Manager.Instance.record.Get_LowerBodyExercises_Func() + "KG";
        }

        this._uiAniml.Play("Measurement_Start_Anim");
    }

    private void Set_Value_Func()
    {
        if (this._curNumber == 0)
        {
            this._MaxHPGage = DataBase_Manager.Instance.GetMeasure_info.Get_BackMovement_HP_Func(UserSystem_Manager.Instance.record.Get_BackMovement_Func());
            this._curHPGage = 0;
            this._curDeleyValue = DataBase_Manager.Instance.GetTable_Define.level_Back_DeleyValue / this._MaxHPGage;

            this._curDamage = UserSystem_Manager.Instance.status.Get_UserStatus_Func().backMovementSTR / DataBase_Manager.Instance.GetTable_Define.level_PlusAttackDmg;
        }
        else if (this._curNumber == 1)
        {
            this._MaxHPGage = DataBase_Manager.Instance.GetMeasure_info.Get_ChestExercises_HP_Func(UserSystem_Manager.Instance.record.Get_BackMovement_Func());
            this._curHPGage = 0;
            this._curDeleyValue = DataBase_Manager.Instance.GetTable_Define.level_Chest_DeleyValue / this._MaxHPGage;

            this._curDamage = UserSystem_Manager.Instance.status.Get_UserStatus_Func().chestExercisesSTR / DataBase_Manager.Instance.GetTable_Define.level_PlusAttackDmg;
        }
        else if (this._curNumber == 2)
        {
            this._MaxHPGage = DataBase_Manager.Instance.GetMeasure_info.Get_LowerBodyExercises_HP_Func(UserSystem_Manager.Instance.record.Get_BackMovement_Func());
            this._curHPGage = 0;
            this._curDeleyValue = DataBase_Manager.Instance.GetTable_Define.level_LowerBody_DeleyValue / this._MaxHPGage;

            this._curDamage = UserSystem_Manager.Instance.status.Get_UserStatus_Func().lowerBodyExercisesSTR / DataBase_Manager.Instance.GetTable_Define.level_PlusAttackDmg;
        }

        this.Set_HP_Func(0.0f);
        this.UpUITextUpdate_Func();
    }

    private void Set_ArrowSpriteChange_Func()
    {
        switch (this._curNumber)
        {
            case 0:
                //위키
                this._schedule_KeyImg.sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                this._schedule_In_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowIn;
                this._schedule_Out_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                break;

            case 1:
                //위키
                this._schedule_KeyImg.sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                this._schedule_In_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowIn;
                this._schedule_Out_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_UpArrowOut;
                break;

            case 2:
                //아래키
                this._schedule_KeyImg.sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_DownArrowOut;
                this._schedule_In_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_DownArrowIn;
                this._schedule_Out_Sprite = DataBase_Manager.Instance.GetTable_Define.ui_Icon_DownArrowOut;
                break;
        }
    }

    public void Arrow_InputSpriteChange_Func(bool is_Down)
    {
        if (is_Down == true)
            this._schedule_KeyImg.sprite = this._schedule_In_Sprite;
        else
            this._schedule_KeyImg.sprite = this._schedule_Out_Sprite;
    }

    public void CountDownStart_Func()
    {
        this._corData.StartCoroutine_Func(Update_Cor(), CoroutineStartType.StartWhenStop);
    }

    private IEnumerator Update_Cor()
    {
        this.Set_Value_Func();
        this.Set_ArrowSpriteChange_Func();

        //카운트
        this._countDownText.gameObject.SetActive(true);
        this._countDownText.text = "3";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        this._countDownText.text = "2";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        this._countDownText.text = "1";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        this._countDownText.text = "0";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        this._countDownText.text = "";
        this._countDownText.gameObject.SetActive(false);

        while (true)
        {
            this._schedule_Time += Time.deltaTime;
            this._schedule_DeleteTime += Time.deltaTime;

            switch (this._curNumber)
            {
                case 0: //데드 

                    if (Input.GetKeyDown(KeyCode.UpArrow) == true)
                    {
                        this.Arrow_InputSpriteChange_Func(true);
                        this.Set_HP_Func(this._curDamage);
                    }

                    if (Input.GetKeyUp(KeyCode.UpArrow) == true)
                    {
                        this.Arrow_InputSpriteChange_Func(false);
                    }

                    if (DataBase_Manager.Instance.GetTable_Define.level_Back_DeleteTime <= this._schedule_DeleteTime)
                    {
                        this._schedule_DeleteTime = 0.0f;
                        this.Set_HP_Func(-(this._curDeleyValue));
                    }

                    break;

                case 1: //벤치

                    if (Input.GetKeyDown(KeyCode.UpArrow) == true)
                    {
                        this.Arrow_InputSpriteChange_Func(true);
                        this.Set_HP_Func(this._curDamage);
                    }

                    if (Input.GetKeyUp(KeyCode.UpArrow) == true)
                    {
                        this.Arrow_InputSpriteChange_Func(false);
                    }

                    if (DataBase_Manager.Instance.GetTable_Define.level_Back_DeleteTime <= this._schedule_DeleteTime)
                    {
                        this._schedule_DeleteTime = 0.0f;
                        this.Set_HP_Func(-(this._curDeleyValue));
                    }

                    if (DataBase_Manager.Instance.GetTable_Define.level_Chest_DeleteTime <= this._schedule_DeleteTime)
                    {
                        this._schedule_DeleteTime = 0.0f;
                        this.Set_HP_Func(-(this._curDeleyValue));
                    }

                    break;

                case 2: //스쿼트

                    if (Input.GetKeyDown(KeyCode.DownArrow) == true)
                    {
                        this.Arrow_InputSpriteChange_Func(true);
                        this.Set_HP_Func(this._curDamage);
                    }

                    if (Input.GetKeyUp(KeyCode.DownArrow) == true)
                    {
                        this.Arrow_InputSpriteChange_Func(false);
                    }

                    if (DataBase_Manager.Instance.GetTable_Define.level_LowerBody_DeleteTime <= this._schedule_DeleteTime)
                    {
                        this._schedule_DeleteTime = 0.0f;
                        this.Set_HP_Func(-(this._curDeleyValue));
                    }

                    break;
            }

            if (CONSTSTRIONG.INT_TIMEMAX <= this._schedule_Time)
            {
                this._timerText.text = CONSTSTRIONG.INT_TIMEMAX.ToString("F2") + " 초";
                break;
            }

            this._timerText.text = this._schedule_Time.ToString("F2").ToString() + " 초";
            yield return null;
        }

        //종료. 최종 단계 확인

        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);

        this._curNumber++;
        this._corData.StopCorountine_Func();

        if (this._curNumber == 3)
        {
            //돌아가기.
            int a_TotalValue = 0;
            a_TotalValue += UserSystem_Manager.Instance.record.Get_BackMovement_Func();
            a_TotalValue += UserSystem_Manager.Instance.record.Get_ChestExercises_Func();
            a_TotalValue += UserSystem_Manager.Instance.record.Get_LowerBodyExercises_Func();

            this._resultText.text = "3 대 " + a_TotalValue.ToString() + "\n\n" +
                "[ " + UserSystem_Manager.Instance.record.Get_BackMovement_Func().ToString() + " / " + UserSystem_Manager.Instance.record.Get_ChestExercises_Func().ToString() + " / " +
                       UserSystem_Manager.Instance.record.Get_LowerBodyExercises_Func().ToString() + " ]";

            Sound_Script.Instance.Play_SFX(SFXListType.삼대측정결과SFX);
            this._resultObj.SetActive(true);
        }
        else
        {
            this.Reset_Func();
            this.Setting_Func();
        }


    }

    private void Set_HP_Func(float a_Value)
    {
        this._curHPGage += a_Value / this._MaxHPGage;

        if (this._curHPGage < 0.0f)
            this._curHPGage = 0.0f;

        if (1.0f < this._curHPGage)
            this._curHPGage = 1.0f;

        this._schedule_GageBar.fillAmount = this._curHPGage;
        this._schedule_Scrollbar.value = this._curHPGage;

        if(1.0f <= this._curHPGage)
        {
            this._schedule_Time = 0;
            this._schedule_DeleteTime = 0;

            if (this._curNumber == 0)
            {
                UserSystem_Manager.Instance.record.Set_ClearBackMovement_Func();
            }
            else if (this._curNumber == 1)
            {
                UserSystem_Manager.Instance.record.Set_ClearChestExercises_Func();
            }
            else if (this._curNumber == 2)
            {
                UserSystem_Manager.Instance.record.Set_ClearLowerBodyExercises_Func();
            }

            Sound_Script.Instance.Play_SFX(SFXListType.삼대증가시환호SFX);
            this.Set_Value_Func();
            this.Setting_Func();
        }
    }

    private void UpUITextUpdate_Func()
    {
        
        if (this._curNumber == 0)
        {
            this._upUIText.text = CONSTSTRIONG.STR_DEADLIFT + " [" + UserSystem_Manager.Instance.record.Get_BackMovement_Func().ToString() + "KG]";
        }
        else if (this._curNumber == 1)
        {
            this._upUIText.text = CONSTSTRIONG.STR_BENCHPRESS + " [" + UserSystem_Manager.Instance.record.Get_ChestExercises_Func().ToString() + "KG]";
        }
        else if (this._curNumber == 2)
        {
            this._upUIText.text = CONSTSTRIONG.STR_SQUAT + " [" + UserSystem_Manager.Instance.record.Get_LowerBodyExercises_Func().ToString() + "KG]";
        }
    }

    private void Reset_Func()
    {
        this._timerText.text = "0.00 초";
        this._typeNameText.text = "";
        this._curRecordText.text = "";

        this._schedule_Time = 0;
        this._schedule_DeleteTime = 0;

        this._MaxHPGage = 0;
        this._curHPGage = 0;
        this._curDeleyValue = 0;
    }
}
