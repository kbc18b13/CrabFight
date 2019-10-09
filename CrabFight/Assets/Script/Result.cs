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


        //蟹を表彰台にセッティング。

        //一位。
        Vector3 position = new Vector3(0.0f, 3.2f, 0.0f);
        GameObject Kani1 = (GameObject)Resources.Load("kani Variant");
        Instantiate(Kani1, position, Quaternion.identity);

        //二位。
        position = new Vector3(2.0f, 1.5f, 0.0f);
        GameObject Kani2 = (GameObject)Resources.Load("kani Variant");
        Instantiate(Kani2, position, Quaternion.identity);

        //三位。
        if (MaxPlayerCount >= 3)
        {
            position = new Vector3(-2.0f, 1.5f, 0.0f);
            GameObject Kani3 = (GameObject)Resources.Load("kani Variant");
            Instantiate(Kani3, position, Quaternion.identity);


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
