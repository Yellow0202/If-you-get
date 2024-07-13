using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Title_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sound_Script.Instance.Play_BGM(BGMListType.≈∏¿Ã∆≤BGM);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown == true)
        {
            SceneManager.LoadScene("0.MainScene_2");
        }
    }
}
