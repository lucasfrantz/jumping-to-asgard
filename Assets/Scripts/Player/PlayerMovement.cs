using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    private float moveInput;
    public bool isGrounded;
    private Rigidbody2D rb;
    private Animator anim;

    public LayerMask groundMask;

    public PhysicsMaterial2D bounceMat, normalMat;
    public bool canJump = true;
    // public float jumpValue = 0.0f;
    private long startCharge = -1;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (startCharge == -1 && isGrounded)
        {
            // rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

            //Flip player when moving left-right
            if( moveInput > 0.01f ){
                gameObject.transform.localScale = new Vector3(5,5,5);
            }
            else if( moveInput < -0.01f){
                gameObject.transform.localScale = new Vector3(-5,5,5);
            }
            anim.SetBool("run", moveInput != 0);
        }

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 2f),
        new Vector2(2f, 0.4f), 0f, groundMask);

        anim.SetBool("grounded", isGrounded);

        if(startCharge >= 0)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }

        // if(Input.GetKey("space") && isGrounded && canJump)
        // {
        //     jumpValue += 0.05f;
        // }

        if(Input.GetKeyDown("space") && isGrounded && canJump && startCharge == -1)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            startCharge = milliseconds;
        }

        if(startCharge >= 0 && milliseconds - startCharge > 3000 && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = (milliseconds - startCharge)/3000f * 30f;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        if(Input.GetKeyUp("space"))
        {
            
            if(isGrounded && startCharge >= 0)
            {
                float jumpValue = (milliseconds - startCharge)/3000f * 30f;
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpValue);
                jumpValue = 0.0f;
                startCharge = -1;
            }
            canJump = true;
        }

        anim.SetBool("isChargingJump", startCharge >= 0);
    }

    void ResetJump()
    {
        canJump = false;
        startCharge = -1;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f));
    }
}
