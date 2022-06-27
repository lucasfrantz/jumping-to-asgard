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
    public float jumpValue = 0.0f;

    private GameObject[] players;
    

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (jumpValue == 0.0f && isGrounded)
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

        if(jumpValue > 0)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }

        if(Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.05f;
        }

        if(Input.GetKeyDown("space") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if(jumpValue >= 30f && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpValue * 0.8f;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        if(Input.GetKeyUp("space"))
        {
            if(isGrounded)
            {
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpValue * 0.8f);
                jumpValue = 0.0f;
            }
            canJump = true;
        }

        anim.SetBool("isChargingJump", jumpValue > 0);
    }

    void ResetJump()
    {
        canJump = false;
        jumpValue = 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f));
    }

    private void OnLevelWasLoaded(int level)
    {
        FindStartPos();

        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 1)
        {
            Destroy(players[1]);
        }
    }

    void FindStartPos()
    {
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
}
