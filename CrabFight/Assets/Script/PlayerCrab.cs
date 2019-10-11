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

    //入力パラメーター
    private bool inGrab = false;
    private bool inSlash = false;


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
        private const float c_actionTime = 0.1f;
        private float actionTime = 0;
        private float actionWait = 0;
        private BoxCollider hasamiBox;

        //コンストラクタ
        public Hasami(BoxCollider col)
        {
            hasamiBox = col;
        }

        public void Release()
        {
            act = Action.None;
            hasamiBox.enabled = false;
            actionTime = 0.0f;
            actionWait = 0.0f;
        }

        public bool GrabCancel()
        {
            if (act == Action.Grab)
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
                        PlayerCrab other = col.GetComponent<PlayerCrab>();
                        if (other.grabbedKani == crab)
                        {
                            other.Release();
                            crab.grabbedTimer = 0.0f;
                            crab.grabbingKani = null;
                            crab._rigidbody.isKinematic = false;
                            crab.BeReleased();
                            other._rigidbody.velocity += crab.transform.forward * 10;
                            crab._rigidbody.velocity += other.transform.forward * 10;
                        }
                        else
                        {
                            other.BeGrabbed(crab);
                            crab.grabbedKani = other;
                            crab.grabbingTime = 0;
                        }
                        act = Action.None;
                        hasamiBox.enabled = false;
                        break;
                }
            }
        }

    }
    Hasami hasami;

    public void Rotate(Vector3 axis, float angle)
    {
        transform.Rotate(axis, angle);
        if (grabbedKani != null)
        {
            grabbedKani.Rotate(axis, angle);
        }
    }

    //パッドナンバーを指定、カニジェネレーターから呼ばれる
    public void SetPadNum(int num)
    {
        padNum = num;
        Renderer[] renders = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renders)
        {
            foreach (Material m in r.materials)
            {
                if (m.name == "KaniRed (Instance)")
                {
                    switch (num)
                    {
                        case 1:
                            m.color = new Vector4(0.3f, 0.3f, 1, 1);
                            break;
                        case 2:
                            m.color = new Vector4(0.3f, 1, 0.3f, 1);
                            break;
                        case 3:
                            m.color = new Vector4(1, 1, 0.3f, 1);
                            break;
                    }
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
                Transform asi = transform.Find("Leg" + (asiCount - 1));
                asi.parent = null;//親子関係を断って

                GameObject gmo = asi.gameObject;

                SkinnedMeshRenderer skin = gmo.GetComponent<SkinnedMeshRenderer>();
                skin.enabled = false;//スキンメッシュレンダーを無効化

                MeshFilter filter = gmo.AddComponent<MeshFilter>();
                filter.mesh = skin.sharedMesh;//メッシュフィルターを作成

                MeshRenderer renderer = gmo.AddComponent<MeshRenderer>();
                renderer.materials = skin.materials;//メッシュレンダーにマテリアルを設定


                asi.GetChild(0).gameObject.SetActive(true);//コライダーを有効化(コライダ担当の子供を有効化)

                gmo.GetComponent<Rigidbody>().isKinematic = false;//キネマティック解除

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

    public void Start()
    {
        //各種コンポーネントを変数に格納
        _rigidbody = GetComponent<Rigidbody>();
        hasami = new Hasami(GetComponents<BoxCollider>()[1]);
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        inSlash = inSlash || Input.GetButtonDown("X_" + padNum);
        inGrab = inGrab || Input.GetButtonDown("B_" + padNum);
    }

    public void FixedUpdate()
    {
        //場外判定
        if (transform.position.y < -1)
        {
            Vector3 v = _rigidbody.velocity;
            v.x = 0; v.z = 0;
            _rigidbody.velocity = v;
            Destroy(this);
            GameObject.Find("GameManager").GetComponent<GameManager>().Death(padNum);
        }

        //ハサミのアップデート
        hasami.Update();

        //回転。回転が実行された場合、移動は実行されない。
        bool rotateIsDone = false;
        float rot = 0;
        if (grabbedTimer <= 0)
        {
            rot = (Input.GetButton("LButton_" + padNum) ? -5 : 0) + (Input.GetButton("RButton_" + padNum) ? 5 : 0);
            if (Mathf.Abs(rot) > 0.1f)
            {
                transform.Rotate(new Vector3(0, 1, 0), rot * ((asiCount+1) / 9.0f));
                rotateIsDone = true;
            }
        }

        animator.SetBool("Grabbing", grabbedKani != null);
        //つかみ中
        bool nowRelease = false;
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
            grabbedKani.Rotate(new Vector3(0, 1, 0), rot * ((asiCount + 1) / 9.0f));

            //離す
            if (inGrab)
            {
                grabbedKani._rigidbody.velocity += transform.forward * 15;
                grabbedKani.BeReleased();
                hasami.Release();

                grabbedKani = null;
                nowRelease = true;
            }
        }

        //もし捕まれ中なら
        animator.SetBool("ZitaBata", grabbedTimer > 0);
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
            float localMaxSpeed = Mathf.Max(xzVelocity.magnitude, maxSpeed * ((asiCount + 1) / 9.0f));

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

        //つかみ中でない場合だけ
        if (grabbedKani == null && nowRelease == false)
        {
            if (Input.GetButton("A_" + padNum))
            {
                animator.SetBool("Guard", true);
            }
            else
            {
                animator.SetBool("Guard", false);

                //切断
                if (inSlash)
                {
                    hasami.doAct(Action.Cut);
                    animator.SetTrigger("Slash");
                }
                else
                //つかみ
                if (inGrab)
                {
                    hasami.doAct(Action.Grab);
                    animator.SetTrigger("Grab");
                }
            }
        }
        inSlash = false;
        inGrab = false;
    }

    public void OnTriggerStay(Collider c)
    {
        hasami.TriggerStay(c, this);
    }
}
