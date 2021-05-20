using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KirbyController : MonoBehaviour
{
    Animator animator;
    bool shouldJump = false;
    bool isOnGround = true;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(-1, 1, 1);
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        this.transform.localScale = new Vector3(move > 0 ? -1 : 1, 1, 1);
        this.transform.Translate(new Vector3(move * Time.deltaTime, 0, 0));
        animator.SetBool("isWalking", move != 0);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            shouldJump = true;
            animator.SetBool("isJumping", true);
            isOnGround = false;
        }

    }

    void FixedUpdate()
    {
        if (shouldJump)
        {
            rigidbody2D.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            shouldJump = false;

        }
        if (isOnGround)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Platform")
        {
            isOnGround = true;
        }
    }



}
