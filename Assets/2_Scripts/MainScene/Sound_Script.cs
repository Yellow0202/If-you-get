using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum BGMListType
{
    치팅데이BGM,
    엔딩BGM,
    공용운동BGM,
    메인BGM,
    삼대측정BGM,
    휴식BGM,
    타이틀BGM,
    업무BGM,
    MAX
}
public enum SFXListType
{
    이벤트등장SFX,
    갯수증가SFX,
    삼대측정결과SFX,
    삼대증가시환호SFX,
    요일넘기는SFX,
    네모연출SFX,
    스케쥴등록삭제SFX,
    스케쥴시작SFX,
    스케쥴등록SFX,
    남성기합소리하나SFX,
    남성기합소리둘SFX,
    토탈애니메이션SFX,
    정산마이너스SFX,
    정산나플러스SFX,
    토탈총합등장SFX,
    중량선택SFX,
}

public class Sound_Script : MonoBehaviour
{
    public static Sound_Script Instance;

    [SerializeField, LabelText("BGM리스트")] private List<AudioClip> _bgmList;
    [LabelText("BGM딕")] private Dictionary<BGMListType, AudioClip> _bgmTypeToClipDataDic;
    [SerializeField, LabelText("SFX리스트")] private List<AudioClip> _sfxList;
    [LabelText("SFX딕")] private Dictionary<SFXListType, AudioClip> _sfxTypeToClipDataDic;

    [SerializeField, LabelText("BGM오디오 소스")] private AudioSource _bgmSource;
    [SerializeField, LabelText("SFX오디오 소스 리스트")] private List<AudioSource> _sfxSourceList;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        this.Setting_Func();
    }

    private void Setting_Func()
    {
        if(this._bgmTypeToClipDataDic == null)
        {
            this._bgmTypeToClipDataDic = new Dictionary<BGMListType, AudioClip>();

            for (int i = 0; i < this._bgmList.Count; i++)
            {
                this._bgmTypeToClipDataDic.Add((BGMListType)i, this._bgmList[i]);
            }
        }

        if (this._sfxTypeToClipDataDic == null)
        {
            this._sfxTypeToClipDataDic = new Dictionary<SFXListType, AudioClip>();

            for (int i = 0; i < this._sfxList.Count; i++)
            {
                this._sfxTypeToClipDataDic.Add((SFXListType)i, this._sfxList[i]);
            }
        }
    }

    public void Play_BGM(BGMListType a_BGMType)
    {
        if(this._bgmTypeToClipDataDic.TryGetValue(a_BGMType, out AudioClip a_Value) == true)
        {
            if (this._bgmSource.isPlaying == true)
                this._bgmSource.Stop();

            this._bgmSource.clip = a_Value;
            this._bgmSource.PlayOneShot(a_Value);
        }
    }

    public void Play_SFX(SFXListType a_SFXType)
    {
        if (this._sfxTypeToClipDataDic.TryGetValue(a_SFXType, out AudioClip a_Value) == true)
        {
            for (int i = 0; i < this._sfxSourceList.Count; i++)
            {
                if (this._sfxSourceList[i].isPlaying == false)
                {
                    this._sfxSourceList[i].clip = a_Value;
                    this._sfxSourceList[i].PlayOneShot(a_Value);
                    return;
                }
            }
        }
    }

}
