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
    bool kanimove = true;
    bool move;
    bool seni1;
    bool seni2;
    bool seni3 = false;

    private int timer1 = 0;
    private int timer2 = 0;
    private int timer3 = 0;

    //ボタン
    private Button stbutton;
    private Button sobutton;
    private Button pl2button;
    private Button pl3button;
    private Button pl4button;

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

        stbutton = GameObject.Find("/Canvas/start").GetComponent<Button>();
        sobutton = GameObject.Find("/Canvas/sousa").GetComponent<Button>();
        pl2button = GameObject.Find("/Canvas/player2").GetComponent<Button>();
        pl3button = GameObject.Find("/Canvas/player3").GetComponent<Button>();
        pl4button = GameObject.Find("/Canvas/player4").GetComponent<Button>();
        stbutton.Select();
        pl2button.enabled = false;
        pl3button.enabled = false;
        pl4button.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        {
            float hori = Input.GetAxis("Horizontal");
            if (hori < -0.5f && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(
                    pl2button.enabled ? pl2button.gameObject : stbutton.gameObject
                );
            }
            else if(hori > 0.5f && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(
                    pl4button.enabled ? pl4button.gameObject : sobutton.gameObject
                );
            }
        }

        //カニの左右移動
        if (kanimove)
        {
            if (move == false && kani1.localPosition.x < 150 && kani2.localPosition.x > -150)
            {
                kani1.localPosition += new Vector3((float)0.2f, 0, 0);
                kani2.localPosition += new Vector3((float)-0.2f, 0, 0);
                if (kani1.localPosition.x >= 150 && kani2.localPosition.x <= -150)
                {
                    move = true;

                }
            }
            else if (kani1.localPosition.x > -70 && move == true && kani2.localPosition.x < 70)
            {
                kani1.localPosition += new Vector3((float)-1.0f, 0, 0);
                kani2.localPosition += new Vector3((float)1.0f, 0, 0);
                if (kani1.localPosition.x < -70 && kani2.localPosition.x > -150)
                {
                    move = false;

                }
            }


        }
        //背景以外全部消す
        if (startCrick)
        {
            if (title.localPosition.y < 500&& kani1.localPosition.y > -700 && kani2.localPosition.y > -700
                && stage.localPosition.y > -600 && startbutton.localPosition.y > -300 && sousabutton.localPosition.y > -300)
            {
                title.localPosition += new Vector3(0, 5, 0);
                kani1.localPosition += new Vector3(0, -7, 0);
                kani2.localPosition += new Vector3(0, -7, 0);
                stage.localPosition += new Vector3(0, -6, 0);
                startbutton.localPosition += new Vector3(0, -3, 0);
                sousabutton.localPosition += new Vector3(0, -3, 0);

            }
            else
            {
                startCrick = false;
                playerselect = true;
            }
        }
        //人数選択画面の表示
        if (playerselect)
        {
            if (playersele.localPosition.y > 0 && player2.localPosition.y < -50 && player3.localPosition.y < -50 && player4.localPosition.y < -50)
            {
                //人数選択画面の表示
                playersele.localPosition += new Vector3(0, -2, 0);
                player2.localPosition += new Vector3(0, 4, 0);
                player3.localPosition += new Vector3(0, 4, 0);
                player4.localPosition += new Vector3(0, 4, 0);

            }
            else
            {
                stbutton.enabled = false;
                sobutton.enabled = false;
                pl2button.enabled = true;
                pl3button.enabled = true;
                pl4button.enabled = true;
                playerselect = false;
                pl2button.Select();
            }

        }
        //ゲーム開始
        if (playerselect == false)
        {
            //二人プレイ 
            if (seni1 == true)
            {
                timer1++;
                if (timer1 > 35)
                {
                    KaniGenerator.SetKaniKazu(2);
                    SceneManager.LoadScene("GameScene");
                }

            }
            //三人プレイ 
            if (seni2 == true)
            {
                timer2++;
                if (timer2 > 35)
                {
                    KaniGenerator.SetKaniKazu(3);
                    SceneManager.LoadScene("GameScene");
                }

            }
            //四人プレイ 
            if (seni3 == true)
            {
                timer3++;
                if (timer3 > 35)
                {
                    KaniGenerator.SetKaniKazu(4);
                    SceneManager.LoadScene("GameScene");
                }

            }
        }

    }

    // startボタンが押された場合
    public void StartClick()
    {
        startCrick = true;
        kanimove = false;
    }
    //sousaボタンが押された場合
    public void SousaClick()
    {
        //操作画面に切り替え
        SceneManager.LoadScene("OperationScene");
    }
    public void Pl2Click()
    {
        //二人プレイ用
        if (playerselect == false)
        {
            seni1 = true;
        }
    }
    public void Pl3Click()
    {
        //三人プレイ用
        if (playerselect == false)
        {
            seni2 = true;
        }
    }
    public void Pl4Click()
    {
        //四人プレイ用
        if (playerselect == false)
        {
            seni3 = true;
        }
    }

}
