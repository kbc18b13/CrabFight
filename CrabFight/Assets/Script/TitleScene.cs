using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{

    private RectTransform title;
    private RectTransform kani1;
    private RectTransform kani2;
    private RectTransform stage;
    private RectTransform startbutton;
    private RectTransform sousabutton;
    private RectTransform playersele;
    private RectTransform player2;
    private RectTransform player3;
    private RectTransform player4;

    bool startCrick;
    bool playerselect;
    // Start is called before the first frame update
    void Start()
    {
        title = GameObject.Find("/Canvas/title").GetComponent<RectTransform>();
        kani1 = GameObject.Find("/Canvas/kani1").GetComponent<RectTransform>();
        kani2 = GameObject.Find("/Canvas/kani2").GetComponent<RectTransform>();
        stage = GameObject.Find("/Canvas/stage").GetComponent<RectTransform>();
        startbutton = GameObject.Find("/Canvas/start").GetComponent<RectTransform>();
        sousabutton = GameObject.Find("/Canvas/sousa").GetComponent<RectTransform>();

        playersele = GameObject.Find("/Canvas/plselect").GetComponent<RectTransform>();
        player2 = GameObject.Find("/Canvas/player2").GetComponent<RectTransform>();
        player3 = GameObject.Find("/Canvas/player3").GetComponent<RectTransform>();
        player4 = GameObject.Find("/Canvas/player4").GetComponent<RectTransform>();

    }

// Update is called once per frame
void Update()
    {
        if (startCrick)
        {
            if (title.localPosition.y < 400&& kani1.localPosition.y > -500 && kani2.localPosition.y > -500
                && stage.localPosition.y > -400 && startbutton.localPosition.y > -200 && sousabutton.localPosition.y > -200)
            {
                //背景以外全部消す
                title.localPosition += new Vector3(0, 4, 0);
                kani1.localPosition += new Vector3(0, -5, 0);
                kani2.localPosition += new Vector3(0, -5, 0);
                stage.localPosition += new Vector3(0, -4, 0);
                startbutton.localPosition += new Vector3(0, -2, 0);
                sousabutton.localPosition += new Vector3(0, -2, 0);

            }
            else
            {
                startCrick = false;
                playerselect = true;
            }
        }
        if (playerselect)
        {
            if (playersele.localPosition.y > -10 && player2.localPosition.y < -50 && player3.localPosition.y < -50 && player4.localPosition.y < -50)
            {
                //人数選択画面の表示
                playersele.localPosition += new Vector3(0, -2, 0);
                player2.localPosition += new Vector3(0, 4, 0);
                player3.localPosition += new Vector3(0, 4, 0);
                player4.localPosition += new Vector3(0, 4, 0);

            }
            else
            {
                playerselect = false;
            }

        }
    }

    // startボタンが押された場合
    public void StartClick()
    {
        startCrick = true;
    }
    //sousaボタンが押された場合
    public void SousaClick()
    {
        SceneManager.LoadScene("OperationScene");
    }
    public void Pl2Click()
    {
        //ゲーム開始
        KaniGenerator.SetKaniKazu(2);
        SceneManager.LoadScene("GameScene");

    }
    public void Pl3Click()
    {
        //ゲーム開始
        KaniGenerator.SetKaniKazu(3);
        SceneManager.LoadScene("GameScene");

    }
    public void Pl4Click()
    {
        //ゲーム開始
        KaniGenerator.SetKaniKazu(4);
        SceneManager.LoadScene("GameScene");

    }

}
