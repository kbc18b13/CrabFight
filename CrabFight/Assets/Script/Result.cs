using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Result : MonoBehaviour
{
    public int MaxPlayerCount;      //プレイヤーの最大プレイ人数。
                                    //ランキング。
    public static int First = 0;           //プレイヤーナンバーを入れる。
    public static int Second = 0;          //プレイヤーナンバーを入れる。
    public static int Third = 0;           //プレイヤーナンバーを入れる。
    public static int Fourth = 0;          //プレイヤーナンバーを入れる。
   

    PlayerCrab[] players;           //プレイヤー。

  

    // Start is called before the first frame update
    void Start()
    {

        //プレイヤーの最大プレイ人数を取得。
        MaxPlayerCount = 3;

        //プレイヤー取得。
        players = new PlayerCrab[MaxPlayerCount]; //初期化。
        players = GameObject.FindObjectsOfType<PlayerCrab>();

        //回転用、反転させてＮｅｗｇｏする

        Quaternion rot = new Quaternion();
        rot = Quaternion.Euler(0, 180.0f, 0);
        //蟹を表彰台にセッティング。
        //一位。
        Vector3 position = new Vector3(0.0f, 3.2f, 0.0f);
        GameObject Kani1 = (GameObject)Resources.Load("kani Variant");
        Kani1 = Instantiate(Kani1, position, rot);
        Kani1.GetComponent<PlayerCrab>().SetPadNum(First);

        //二位。
        position = new Vector3(2.0f, 1.5f, 0.0f);
        GameObject Kani2 = (GameObject)Resources.Load("kani Variant");
        Kani2 = Instantiate(Kani2, position, rot);
        Kani2.GetComponent<PlayerCrab>().SetPadNum(Second);

        //三位。
        if (MaxPlayerCount >= 3)
        {
            position = new Vector3(-2.0f, 1.5f, 0.0f);
            GameObject Kani3 = (GameObject)Resources.Load("kani Variant");
            Kani3 = Instantiate(Kani3, position, rot);
            Kani3.GetComponent<PlayerCrab>().SetPadNum(Third);

            //四位。
            if (MaxPlayerCount == 4)
            {
                position = new Vector3(0.0f, 0.0f, -2.0f);
                GameObject Kani4 = (GameObject)Resources.Load("kani Variant");
                Kani4 = Instantiate(Kani3, position, rot);
                Kani4.GetComponent<PlayerCrab>().SetPadNum(Fourth);

            }

        }


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("B_" + First))
        {
            SceneManager.LoadScene("GameScene");
        }


    }
}
