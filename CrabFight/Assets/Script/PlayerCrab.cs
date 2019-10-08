using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrab : MonoBehaviour
{
    //事前設定パラメーター
    public float moveSpeed = 10.0f;
    public float brakeSpeed = 1.0f;
    public float maxSpeed = 10.0f;

    //自分のコンポーネント
    private Rigidbody _rigidbody;

    //内部パラメーター
    private bool onGround = true;

    public void Start()
    {
        //各種コンポーネントを変数に格納
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        //回転。回転が実行された場合、移動は実行されない。
        bool rotateIsDone = false;
        {
#if false
            Vector3 rotVec = new Vector3(0, 0, 0);
            rotVec.x += Input.GetAxis("RStick_X");
            rotVec.z += Input.GetAxis("RStick_Y");
            if(rotVec.sqrMagnitude > 0.1f)
            {
                rotateIsDone = true;
                float dot = Vector3.Dot(transform.forward, rotVec.normalized);
                dot = Mathf.Clamp(dot, -1, 1);
                float angle = Mathf.Acos(dot);
                Vector3 axis = Vector3.Cross(transform.forward, rotVec);
                transform.Rotate(new Vector3(0, axis.y, 0), Mathf.Min(5, Mathf.Rad2Deg * angle));
                Debug.Log(Mathf.Rad2Deg * angle);
            }
#else
            float rot = (Input.GetButton("LButton") ? -5 : 0 ) + (Input.GetButton("RButton") ? 5 : 0);
            if (Mathf.Abs(rot) > 0.1f)
            {
                transform.Rotate(new Vector3(0, 1, 0), rot);
                rotateIsDone = true;
            }
#endif
        }

        //歩行
        Vector3 moveVec;
        {
            //移動ベクトルを取得
            moveVec = new Vector3(0, 0, 0);
            if(!rotateIsDone){
                //横方向に動きを固定する
                moveVec.x += Input.GetAxis("LStick_X") * moveSpeed;
                moveVec.z += Input.GetAxis("LStick_Y") * moveSpeed;
                float length = moveVec.magnitude;
                float dot = Vector3.Dot(moveVec, transform.right);
                dot = (dot > 0) ? 1 : -1;
                moveVec = transform.right * dot * length;
            }

            //Y軸方向を除いた現在の速度
            Vector3 xzVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

            //最大速度。既に最大速度を超えているときは、何か外からの力が加わった結果なので放置。
            float localMaxSpeed = Mathf.Max(xzVelocity.magnitude, maxSpeed);

            //速度を加える
            if (moveVec.sqrMagnitude > 0.1f)
            {
                xzVelocity += moveVec;
            }

            //減速。地面に足がついているときのみ。
            if (onGround)
            {
                Vector3 before = xzVelocity;
                xzVelocity -= xzVelocity.normalized * brakeSpeed;
                if (Vector3.Dot(before, xzVelocity) < 0)
                {
                    xzVelocity = new Vector3(0, 0, 0);
                }
            }

            ////最大速度以内に収める
            if (xzVelocity.magnitude > localMaxSpeed)
            {
                xzVelocity = xzVelocity.normalized * localMaxSpeed;
            }

            //適用
            xzVelocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = xzVelocity;
        }

        //場外判定
        if (transform.position.y < -10)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
}
