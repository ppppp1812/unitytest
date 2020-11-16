using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanMotionScript : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 velocity;
    [SerializeField]

    private Animator animator;
    private Rigidbody rb;
    // キャラクターコントローラ（カプセルコライダ）の参照
    private CapsuleCollider col;
    // ジャンプ威力
    public float jumpPower = 3.0f;
    public float animSpeed = 1.5f;				// アニメーション再生速度設定
    // 以下キャラクターコントローラ用パラメタ
    // 前進速度
    public float forwardSpeed = 6.0f;
    // 後退速度
    public float backwardSpeed = 3.0f;
    // 旋回速度
    public float rotateSpeed = 5.0f;
    // アニメーター各ステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Move");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int restState = Animator.StringToHash("Base Layer.Rest");
    private AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");              // 入力デバイスの水平軸をhで定義
        float v = Input.GetAxis("Vertical");				// 入力デバイスの垂直軸をvで定義
        if(v > 0.1)
            animator.SetFloat("ForwardSpeed", v);
        else if(v < -0.1)
            animator.SetFloat("BackwardSpeed", v);
        animator.speed = animSpeed;
        currentBaseState = animator.GetCurrentAnimatorStateInfo(0);	// 参照用のステート変数にBase Layer (0)の現在のステートを設定する
        rb.useGravity = true;//ジャンプ中に重力を切るので、それ以外は重力の影響を受けるようにする
        // 以下、キャラクターの移動処理
        velocity = new Vector3(h, 0, v);        // 上下のキー入力の移動量を取得
        if (velocity.magnitude > 0.1f)
        {
            animator.SetFloat("ForwardSpeed", velocity.magnitude);
            transform.LookAt(transform.position + velocity);
        }
        else
        {
            animator.SetFloat("ForwardSpeed", 0f);
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * forwardSpeed * Time.deltaTime);
        //接地しているか
        if (characterController.isGrounded)
        {

        }
        ////接地しているか
        //if (characterController.isGrounded)
        //{

        //velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //if (velocity.magnitude > 0.1f)
        //{
        //    animator.SetFloat("ForwardSpeed", velocity.magnitude);
        //    transform.LookAt(transform.position + velocity);
        //}
        //else
        //{
        //    animator.SetFloat("ForwardSpeed", 0f);
        //}
        //}
        //velocity.y += Physics.gravity.y * Time.deltaTime;
        //characterController.Move(velocity * Time.deltaTime);
    }
}
