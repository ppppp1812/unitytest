using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanMoveScript : MonoBehaviour
{
    public float speed = 3f;
    public float jumpSpeed = 3f;

    private Rigidbody rb;
    private float h, v;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        //Rigidbodyを取得し，回転しないように固定
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            moveDirection = speed * new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            rb.velocity = moveDirection;
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }
}
