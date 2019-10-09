using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    Button start;
    Button sousa;
    // Start is called before the first frame update
    void Start()
    {
        //startbuttunのボタンオブジェクトの取得。
        start=GameObject.Find("/Canvas/start").GetComponent<Button>();
        //sousabuttunのボタンオブジェクトの取得。
        sousa = GameObject.Find("/Canvas/sousa").GetComponent<Button>();

        start.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // startボタンが押された場合
    public void StartClick()
    {
        //ゲーム開始
        KaniGenerator.SetKaniKazu(2);
        SceneManager.LoadScene("GameScene");
    }
    //sousaボタンが押された場合
    public void SousaClick()
    {
        SceneManager.LoadScene("OperationScene");

    }

}
