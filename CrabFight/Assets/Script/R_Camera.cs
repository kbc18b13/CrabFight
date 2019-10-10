using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Camera : MonoBehaviour
{
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //カメラをＸ軸上回転しながら一位のところへ移動。
        if (this.transform.position.x <= -0.1f)
        {
            //回転軸オブジェクト。
            Transform target;
            target = GameObject.Find("Camerapoint").transform;
            //回転処理。
            float angle = 120.0f;
            transform.RotateAround(target.position, Vector3.up, angle * Time.deltaTime);
            
        }

        //カメラをＹ軸↓へ移動。
        if (this.transform .position.y >= 4.0f)
        {
            //移動処理。
            move = new Vector3(0.0f, 0.05f, 0.0f);
            this.transform.position -= move;
        }

        //視点ターゲット。一位を見続ける。
        Vector3 position = new Vector3(0.0f, 3.0f, 0.0f);
        this.transform.LookAt(position);

    }
}
