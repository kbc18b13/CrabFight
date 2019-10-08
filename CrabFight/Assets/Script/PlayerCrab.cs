using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrab : MonoBehaviour
{
    //事前設定パラメーター
    public float moveSpeed = 10.0f;
    public float brakeSpeed = 1.0f;
    public float maxSpeed = 10.0f;
    public float jumpPower = 10.0f;

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
        //歩行
        Vector3 moveVec;
        {
            //移動ベクトルを取得
            moveVec = new Vector3(0, 0, 0);
            {
                moveVec.x += Input.GetAxis("Horizontal") * moveSpeed;
                moveVec.z += Input.GetAxis("Vertical") * moveSpeed;
                Debug.Log(moveVec);
            }

            //Y軸方向を除いた現在の速度
            Vector3 xzVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

            //最大速度。既に最大速度を超えているときは、何か外からの力が加わった結果なので放置。
            float localMaxSpeed = Mathf.Max(xzVelocity.magnitude, maxSpeed);

            //速度を加える
            if (moveVec.sqrMagnitude > 0.1f)
            {
                xzVelocity += moveVec;
                Debug.Log("debug");
                Debug.Log("debug");
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
        //ジャンプ
        if (onGround)
        {
            if (Input.GetButton("Jump"))
            {
                _rigidbody.velocity = new Vector3(0, jumpPower, 0);
            }
        }

        //向きの回転
        moveVec.y = 0;
        if (moveVec.sqrMagnitude > 0.1f)
        {
            transform.LookAt(transform.position + moveVec, Vector3.up);
        }


        //場外判定
        if (transform.position.y < -100)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage1");
        }
    }
}
