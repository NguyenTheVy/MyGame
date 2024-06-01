using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[AddComponentMenu("Vy/PlayerController")]

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer; // Lớp nền để kiểm tra nhân vật có đang trên mặt đất không

    Rigidbody2D rb;
    Animator animator;
    [SerializeField] private float moveSpeed = 5f; // Tốc độ di chuyển của nhân vật
    [SerializeField] private float jumpForce = 10f; // Lực nhảy của nhân vật
    [SerializeField] Transform groundCheck;
    private bool isGrounded;
    SpriteRenderer playerFlip;
    private bool facingRight = false; // Biến để theo dõi hướng của nhân vật
    


    void Start()
    {
        // Lấy tham chiếu tới Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        playerFlip = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        move();
        // Kiểm tra xem nhân vật có trên mặt đất không
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            animator.SetBool("IsJump", true);
            /*if (isGrounded)
            {
                animator.SetBool("IsJump", false);

            }*/
        }
        if (Input.GetKeyUp(KeyCode.Space))

        {
            animator.SetBool("IsJump", false);

        }



    }
    public void move()
    {
        // Lấy input từ bàn phím
        float moveInput = Input.GetAxis("Horizontal");

        // Di chuyển nhân vật dựa trên input và moveSpeed
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        
        

        // Kiểm tra và quay nhân vật
        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            Flip();
        }

        if (math.abs(moveInput) > 0)
        {
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }
        
        //animator.SetFloat("Speed", Mathf.Abs(moveInput));
        /*else if 
        {
            Flip();
        }*/
    }

    void Flip()
    {
        // Quay nhân vật bằng cách đảo ngược hướng x của scale
        facingRight = !facingRight;
        playerFlip.flipX = !facingRight;
        /*Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;*/
    }

    

    
}
