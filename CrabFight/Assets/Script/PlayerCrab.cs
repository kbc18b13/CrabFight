using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrab : MonoBehaviour {
    //事前設定パラメーター
    public float moveSpeed = 10.0f;
    public float brakeSpeed = 1.0f;
    public float maxSpeed = 10.0f;

    //自分のコンポーネント
    private Rigidbody _rigidbody;
    private BoxCollider hasamiBox;
    private Animator animator;

    //内部パラメーター
    public int padNum = 0;
    private bool onGround = true;
    private float grabbedTimer = 0.0f;//捕まれている残り時間
    public int asiCount = 8;       //足の数。ヒットポイントと似ている。
    float grabbingTime = 0.0f;//つかんでいる時間

    private PlayerCrab grabbedKani = null;//自分がつかんでいるカニ
    private PlayerCrab grabbingKani = null;//自分をつかんでいるカニ


    enum Action {
        None,
        Cut,
        Grab,
        Guard
    }

    //ハサミとアクションのクラス
    class Hasami {
        private Action act = Action.None;
        private const float c_actionTime = 0.2f;
        private float actionTime = 0;
        private float actionWait = 0;
        private BoxCollider hasamiBox;

        //コンストラクタ
        public Hasami(BoxCollider col)
        {
            hasamiBox = col;
        }

        public bool GrabCancel() {
            if(act == Action.Grab)
            {
                act = Action.None;
                actionTime = 0;
                actionWait = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Action GetAct()
        {
            return act;
        }

        //アクション実行
        public void doAct(Action a)
        {
            if (actionWait + actionTime <= 0)
            {
                act = a;
                switch (a)
                {
                    case Action.Cut:
                        actionWait = 0.1f;
                        break;
                    case Action.Grab:
                        actionWait = 0.5f;
                        break;
                }
                actionTime = c_actionTime;
            }
        }

        //毎Updateごとに呼ぶ
        public void Update()
        {
            actionWait -= Time.fixedDeltaTime;
            if (actionWait < 0)
            {
                if (actionTime == c_actionTime)
                {
                    hasamiBox.enabled = true;
                }

                if (actionTime > 0)
                {
                    actionTime -= Time.fixedDeltaTime;
                }
                else
                {
                    hasamiBox.enabled = false;
                }
            }
        }

        //OnTriggerStayで呼ぶ
        public void TriggerStay(Collider col, PlayerCrab crab)
        {
            if (col.tag == "Kani")
            {
                switch (act)
                {
                    case Action.Cut:
                        if (col.GetComponent<PlayerCrab>().CutLeg())
                        {
                            act = Action.None;
                            hasamiBox.enabled = false;
                        }
                        else
                        {
                            crab._rigidbody.velocity = crab.transform.forward * -20;
                        }
                        break;

                    case Action.Grab:
                        crab.grabbedKani = col.GetComponent<PlayerCrab>();
                        crab.grabbedKani.BeGrabbed(crab);
                        crab.grabbingTime = 0;
                        act = Action.None;
                        hasamiBox.enabled = false;
                        break;
                }
            }
        }

    }
    Hasami hasami;


    //全カニ共通のカニカウンター
    static private int KaniCount = 0;
    static public int GetKaniCount()
    {
        return KaniCount;
    }

    //パッドナンバーを指定、カニジェネレーターから呼ばれる
    public void SetPadNum(int num)
    {
        padNum = num;
        Renderer[] renders = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renders)
        {
            if (r.material.name == "KaniRed (Instance)")
            {
                switch (num)
                {
                    case 1:
                        r.material.color = new Vector4(0.3f, 0.3f, 1, 1);
                        break;
                    case 2:
                        r.material.color = new Vector4(0.3f, 1, 0.3f, 1);
                        break;
                    case 3:
                        r.material.color = new Vector4(1, 1, 0.3f, 1);
                        break;
                }
            }
        }
    }

    //足を切り落とされる
    public bool CutLeg()
    {
        if (!animator.GetBool("Guard"))
        {
            if (hasami.GrabCancel())
            {
                animator.Play("Idle", 1);
            }
            if (asiCount != 0)
            {
                asiCount--;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    //捕まれる。
    public void BeGrabbed(PlayerCrab grapper)
    {
        grabbedTimer = 3.0f;
        grabbingKani = grapper;
        _rigidbody.isKinematic = true;
    }

    //解放する
    public void Release()
    {
        grabbedKani = null;
    }

    //解放される
    public void BeReleased()
    {
        grabbedTimer = 0.0f;
        grabbingKani = null;
        _rigidbody.isKinematic = false;
    }

    public void Awake()
    {
        KaniCount++;
    }

    public void Start()
    {
        //各種コンポーネントを変数に格納
        _rigidbody = GetComponent<Rigidbody>();
        hasami = new Hasami(GetComponents<BoxCollider>()[1]);
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        //場外判定
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            GameObject.Find("GameManager").GetComponent<GameManager>().Death(padNum);
        }

        //もし捕まれ中なら
        if (grabbedTimer > 0)
        {
            //捕まれタイマーを減らす。
            grabbedTimer -= Time.fixedDeltaTime;
            if (grabbedTimer < 0)
            {
                grabbingKani.Release();
                grabbedTimer = 0.0f;
                grabbingKani = null;
                _rigidbody.isKinematic = false; BeReleased();
            }
            return;
        }


        //回転。回転が実行された場合、移動は実行されない。
        bool rotateIsDone = false;
        float rot = 0;
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
            rot = (Input.GetButton("LButton_" + padNum) ? -5 : 0) + (Input.GetButton("RButton_" + padNum) ? 5 : 0);
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
            if (!rotateIsDone)
            {
                //横方向に動きを固定する
                moveVec.x += Input.GetAxis("LStick_X_" + padNum) * moveSpeed;
                moveVec.z += Input.GetAxis("LStick_Y_" + padNum) * moveSpeed;
                float length = moveVec.magnitude;
                float dot = Vector3.Dot(moveVec, transform.right);
                dot = (dot > 0) ? 1 : -1;
                moveVec = transform.right * dot * length;
            }

            //Y軸方向を除いた現在の速度
            Vector3 xzVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

            //最大速度。既に最大速度を超えているときは、何か外からの力が加わった結果なので放置。
            //最大速度に足の数による減衰をかける
            float localMaxSpeed = Mathf.Max(xzVelocity.magnitude, maxSpeed * (asiCount / 8.0f));

            //速度を加える
            if (moveVec.sqrMagnitude > 0.1f)
            {
                xzVelocity += moveVec;
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
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

        animator.SetBool("Grabbing", grabbedKani != null);
        //つかみ中
        if (grabbedKani != null)
        {
            grabbingTime += Time.fixedDeltaTime;
            if (grabbingTime > 1)
            {
                grabbingTime = 1;
            }

            //つかんでいるカニの位置を設定
            Vector3 pos = transform.position;
            pos += transform.forward * 1.5f + transform.up * 1.2f;
            grabbedKani.transform.position = Vector3.Lerp(grabbedKani.transform.position, pos, grabbingTime);
            grabbedKani.transform.Rotate(new Vector3(0, 1, 0), rot);

            //離す
            if (Input.GetButtonDown("B_" + padNum))
            {
                grabbedKani._rigidbody.velocity += transform.forward * 15;
                grabbedKani.BeReleased();
                grabbedKani = null;
            }
        }
        else
        {
            if (Input.GetButton("A_" + padNum))
            {
                animator.SetBool("Guard", true); transform.Rotate(new Vector3(0, 1, 0), rot);
            }
            else
            {
                animator.SetBool("Guard", false);

                //切断
                if (Input.GetButtonDown("X_" + padNum))
                {
                    hasami.doAct(Action.Cut);
                    animator.SetTrigger("Slash");
                }
                else
                //つかみ
                if (Input.GetButtonDown("B_" + padNum))
                {
                    hasami.doAct(Action.Grab);
                    animator.SetTrigger("Grab");
                }
            }
        }
        //ハサミのアップデート
        hasami.Update();
    }

    public void OnTriggerStay(Collider c)
    {
        hasami.TriggerStay(c, this);
    }
}
