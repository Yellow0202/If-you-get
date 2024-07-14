using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cargold;

public class End_Script : MonoBehaviour
{
    public Image _bgImg;

    public List<Sprite> _spriteList;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(End_Cor());
    }

    private IEnumerator End_Cor()
    {
        this._bgImg.sprite = this._spriteList[0];
        yield return Coroutine_C.GetWaitForSeconds_Cor(2.0f);

        this._bgImg.sprite = this._spriteList[1];
        yield return Coroutine_C.GetWaitForSeconds_Cor(2.0f);

        this._bgImg.sprite = this._spriteList[2];
        yield return Coroutine_C.GetWaitForSeconds_Cor(3.0f);

        this._bgImg.sprite = this._spriteList[3];
        yield return Coroutine_C.GetWaitForSeconds_Cor(2.0f);

        Application.Quit();
    }
}
