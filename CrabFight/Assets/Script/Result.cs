using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;//ＵＩコンポーネント使用。
public class Result : MonoBehaviour
{
    public int MaxPlayerCount;      //プレイヤーの最大プレイ人数。
    //ランキング。
    public static int First = 0;           //プレイヤーナンバーを入れる。
    public static int Second = 0;          //プレイヤーナンバーを入れる。
    public static int Third = 0;           //プレイヤーナンバーを入れる。
    public static int Fourth = 0;          //プレイヤーナンバーを入れる。


    //PlayerCrab[] players;           //プレイヤー。
    GameObject Kani1;
    GameObject Kani2;
    GameObject Kani3;
    GameObject Kani4;

    //ボタン判定
    bool Agein = true;
    bool Taitoru = false;
    KaniGenerator kanigene;
    Button button1;
    Button button2;

    //音;
    public AudioClip kettei;
    public AudioClip sentak;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {

        //プレイヤーの最大プレイ人数を取得。
        MaxPlayerCount = KaniGenerator.GetKaniKazu();

        //テストプレイ用。
        //MaxPlayerCount = 2;
        //First = 0;           //プレイヤーナンバーを入れる。
        //Second = 1;          //プレイヤーナンバーを入れる。
        //Third = 0;           //プレイヤーナンバーを入れる。
        //Fourth = 0;          //プレイヤーナンバーを入れる。

    //回転用、反転させてＮｅｗｇｏする
    Quaternion rot = new Quaternion();
        rot = Quaternion.Euler(0, 180.0f, 0);
        //蟹を表彰台にセッティング。
        //一位。
        Vector3 position = new Vector3(0.0f, 3.2f, 0.0f);
        Kani1 = (GameObject)Resources.Load("kani Variant");
        Kani1 = Instantiate(Kani1, position, rot);
        Kani1.GetComponent<PlayerCrab>().SetPadNum(First);

        //二位。
        position = new Vector3(2.0f, 1.5f, 0.0f);
        Kani2 = (GameObject)Resources.Load("kani Variant");
        Kani2 = Instantiate(Kani2, position, rot);
        Kani2.GetComponent<PlayerCrab>().SetPadNum(Second);

        //三位。
        if (MaxPlayerCount >= 3)
        {
            position = new Vector3(-2.0f, 1.5f, 0.0f);
            Kani3 = (GameObject)Resources.Load("kani Variant");
            Kani3 = Instantiate(Kani3, position, rot);
            Kani3.GetComponent<PlayerCrab>().SetPadNum(Third);

            //四位。
            if (MaxPlayerCount == 4)
            {
                position = new Vector3(-4.0f, 2.0f, 4.0f);
                Kani4 = (GameObject)Resources.Load("kani Variant");
                rot = Quaternion.Euler(0, -45.0f, 0);//そっぽ向かせる。
                Kani4 = Instantiate(Kani4, position, rot);
                Kani4.GetComponent<PlayerCrab>().SetPadNum(Fourth);

            }

        }

        //ボタンコンポーネントの取得。
        button1 = GameObject.Find("Canvas/Button1").GetComponent<Button>();
        button2 = GameObject.Find("Canvas/Button2").GetComponent<Button>();

        //最初に選択状態にしたいボタンの設定。
        Selectable sel = GetComponent<Selectable>();
        button1.Select();

        //oto
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //蟹が場外に行くのを防ぐ。
        Vector3 position = new Vector3(0.0f, 5.0f, 0.0f);
        //場外判定
        if (Kani1.transform.position.y < -0.5)
        {
            Kani1.transform.position = position;
        }
        if (Kani2.transform.position.y < -0.5)
        {
            Kani2.transform.position = position;
        }
        if (MaxPlayerCount >= 3)
        {
            if (Kani3.transform.position.y < -0.5)
            {
                Kani3.transform.position = position;
            }
            if (MaxPlayerCount == 4)
            {
                if (Kani4.transform.position.y < -0.5)
                {
                    Kani4.transform.position = position;
                }
            }
        }
        //ボタン選択
        //A決定判定
        if (Input.GetButtonDown("A_" + First)
            && Agein == true )
        {
            sound.PlayOneShot(kettei);
            SceneManager.LoadScene("GameScene");
        }
        if (Input.GetButtonDown("A_" + First)
            && Taitoru == true)
        {
            sound.PlayOneShot(kettei);
            SceneManager.LoadScene("ka");
        }
        //十字キー右左判定　セレクト状態
        if (Input.GetAxis("Horizontal") >=0.5f 
            && Agein == true)
        {
            sound.PlayOneShot(sentak);
            Agein = false;
            Taitoru = true;
            button1.Select();
            Debug.Log("deba1");
        }
        if (Input.GetAxis("Horizontal") <= -0.5f 
            && Taitoru == true)
        {
            sound.PlayOneShot(sentak);
            Agein = true;
            Taitoru = false;
            button2.Select();
            Debug.Log("deba2");
        }
        //ボタンセレクト言うこと聞かねぇ強制的にセレクト状態にしてやるぜ。
        if (Agein == true)
        {
            button1.Select();
            Debug.Log("1");
        }
        if (Taitoru == true)
        {
            button2.Select();
            Debug.Log("2");
        }
        
        
    }
}
