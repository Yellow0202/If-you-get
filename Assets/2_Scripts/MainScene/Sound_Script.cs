using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum BGMListType
{
    ġ�õ���BGM,
    ����BGM,
    ����BGM,
    ����BGM,
    �������BGM,
    �޽�BGM,
    Ÿ��ƲBGM,
    ����BGM,
    MAX
}
public enum SFXListType
{
    �̺�Ʈ����SFX,
    ��������SFX,
    ����������SFX,
    ���������ȯȣSFX,
    ���ϳѱ��SFX,
    �׸���SFX,
    �������ϻ���SFX,
    ���������SFX,
    ��������SFX,
    �������ռҸ��ϳ�SFX,
    �������ռҸ���SFX,
    ��Ż�ִϸ��̼�SFX,
    ���긶�̳ʽ�SFX,
    ���곪�÷���SFX,
    ��Ż���յ���SFX,
    �߷�����SFX,
}

public class Sound_Script : MonoBehaviour
{
    public static Sound_Script Instance;

    [SerializeField, LabelText("BGM����Ʈ")] private List<AudioClip> _bgmList;
    [LabelText("BGM��")] private Dictionary<BGMListType, AudioClip> _bgmTypeToClipDataDic;
    [SerializeField, LabelText("SFX����Ʈ")] private List<AudioClip> _sfxList;
    [LabelText("SFX��")] private Dictionary<SFXListType, AudioClip> _sfxTypeToClipDataDic;

    [SerializeField, LabelText("BGM����� �ҽ�")] private AudioSource _bgmSource;
    [SerializeField, LabelText("SFX����� �ҽ� ����Ʈ")] private List<AudioSource> _sfxSourceList;

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
