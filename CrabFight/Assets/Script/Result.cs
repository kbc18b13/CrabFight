using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //テスト用、適当にランキング。
        First = 3;
        Second = 1;
        Third = 2;
        //蟹を表彰台にセッティング。
        //一位。
        Vector3 position = new Vector3(0.0f, 3.2f, 0.0f);
        GameObject Kani1 = (GameObject)Resources.Load("kani Variant");
        Kani1 = Instantiate(Kani1, position, Quaternion.identity);
        Kani1.GetComponent<PlayerCrab>().SetPadNum(First - 1);

        //二位。
        position = new Vector3(2.0f, 1.5f, 0.0f);
        GameObject Kani2 = (GameObject)Resources.Load("kani Variant");
        Kani2 = Instantiate(Kani2, position, Quaternion.identity);
        Kani2.GetComponent<PlayerCrab>().SetPadNum(Second - 1);

        //三位。
        if (MaxPlayerCount >= 3)
        {
            position = new Vector3(-2.0f, 1.5f, 0.0f);
            GameObject Kani3 = (GameObject)Resources.Load("kani Variant");
            Kani3 = Instantiate(Kani3, position, Quaternion.identity);
            Kani3.GetComponent<PlayerCrab>().SetPadNum(Third - 1);

            //四位。
            if (MaxPlayerCount == 4)
            {


            }

        }


    }

    // Update is called once per frame
    void Update()
    {
       



    }
}
