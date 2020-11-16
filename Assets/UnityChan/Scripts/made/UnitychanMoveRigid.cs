using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityChan
{
    // 必要なコンポーネントの列記
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class UnitychanMoveRigid : MonoBehaviour
    {
        public float speed = 3f;
        Vector3 Player_pos; //プレイヤーのポジション
        float moveX = 0f;
        float moveZ = 0f;
        Rigidbody rb;
        Animator anim;
        Transform trs;
        float f振り返り速度 = 10;
        void Start()
		{
            Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
            //Rigidbodyを取得し，回転しないように固定
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            anim = GetComponent<Animator>();
            anim.Play("Rest");
            trs = GetComponent<Transform>();
            trs.rotation = Quaternion.identity;
        }
        void Update()
        {
            moveX = Input.GetAxis("Horizontal") * speed;
            moveZ = Input.GetAxis("Vertical") * speed;
            anim.SetFloat("ForwardSpeed", moveZ);
        }
		private void FixedUpdate()
		{
            Vector3 vec = new Vector3(moveX, 0.0f, moveZ);
            rb.velocity = vec;
            //////高さ調整
            ////if (trs.position.y != 0)
            ////    trs.position = new Vector3(trs.position.x, 0.0f, trs.position.z);
            Vector3 diff = trs.position - Player_pos; //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得
            if (diff.magnitude > 0.01f) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
            {
                diff.y = 0;
                Console.WriteLine(vec.x);
                //transform.rotation = Quaternion.LookRotation(diff);  //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる
                //体の向きを変更
                Quaternion rotation = Quaternion.LookRotation(diff);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * f振り返り速度);
            }

            Player_pos = transform.position; //プレイヤーの位置を更新
        }
	}
}

