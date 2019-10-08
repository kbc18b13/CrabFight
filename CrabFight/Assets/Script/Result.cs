using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public int MaxPlayerCount;      //プレイヤーの最大プレイ人数。
    //ランキング。
    static int First = 0;           //プレイヤーナンバーを入れる。
    static int Second = 0;          //プレイヤーナンバーを入れる。
    static int Third = 0;           //プレイヤーナンバーを入れる。
    static int Fourth = 0;          //プレイヤーナンバーを入れる。
   

    PlayerCrab[] players;           //プレイヤー。

    // Start is called before the first frame update
    void Start()
    {

        //プレイヤーの最大プレイ人数を取得。


        //プレイヤー取得。
        players = new PlayerCrab[MaxPlayerCount]; //初期化。
        players = GameObject.FindObjectsOfType<PlayerCrab>();
       
    }

    // Update is called once per frame
    void Update()
    {
       //蟹を表彰台にセッティング。
       //一位。


       //二位。


       //三位。
       if(MaxPlayerCount >= 3){



            //四位。
            if (MaxPlayerCount == 4){


            }

       }

        //カメラ。
        //検索取得。
        GameObject game_object = GameObject.Find("Main Camera");
        //メインカメラを取得。
        Camera camera = Camera.main;




    }
}
