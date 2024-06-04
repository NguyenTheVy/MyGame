using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[AddComponentMenu("Vy/PlayerController")]

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 10f;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isRunning = false;
    private bool isJumping = false;

    private bool isFacingRight = true;
    private bool isGrounded = false;
    



    void Start()
    {
        // Lấy tham chiếu tới Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
       
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        

        // Kiểm tra đầu vào người chơi
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // Kiểm tra phím Space để nhảy
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }

        // Cập nhật vận tốc di chuyển
        float speed = isRunning ? runSpeed : moveSpeed;
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;

        // Cập nhật trạng thái của Animator
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsRunning", isRunning);
       

        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip(); // Nếu đang hướng trái và isFacingRight là false, quay sang phải
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip(); // Nếu đang hướng phải và isFacingRight là true, quay sang trái
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Khi nhân vật chạm đất, dừng trạng thái nhảy
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }




}
