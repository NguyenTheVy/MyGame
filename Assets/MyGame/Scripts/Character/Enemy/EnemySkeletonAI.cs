using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAI : MonoBehaviour
{
    public Transform pointA;  // Điểm A
    public Transform pointB;  // Điểm B
    public float speed = 2f;  // Tốc độ di chuyển
    public Transform player;  // Người chơi
    public float detectionRange = 2f;  // Phạm vi phát hiện người chơi

    private Transform targetPoint;
    private bool isWaiting = false;  // Biến để kiểm tra trạng thái chờ
    private bool isFacingRight = true;
    private bool isAttacking = false; // Biến để kiểm tra trạng thái tấn công
    Animator Animator;

    void Start()
    {
        targetPoint = pointA;  // Bắt đầu từ điểm A
        Animator = GetComponentInChildren<Animator>();
        Animator.SetBool("IsWalkBool", true);  // Bắt đầu với trạng thái đi bộ
    }

    void Update()
    {
        if (!isWaiting && !isAttacking)
        {
            // Kiểm tra khoảng cách tới người chơi
            if (Vector2.Distance(transform.position, player.position) < detectionRange)
            {
                StartCoroutine(StartAttack());
            }
            else
            {
                // Di chuyển kẻ thù về phía targetPoint
                transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

                // Kiểm tra nếu kẻ thù đã đến targetPoint
                if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
                {
                    StartCoroutine(WaitAtPoint());  // Bắt đầu Coroutine chờ
                }
            }
        }
    }

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true;  // Bắt đầu trạng thái chờ
        Animator.SetBool("IsWalkBool", false);  // Chuyển sang trạng thái idle
        Flip();  // Flip hướng của kẻ thù
        yield return new WaitForSeconds(2f);  // Chờ 2 giây
        targetPoint = targetPoint == pointA ? pointB : pointA;  // Chuyển đổi targetPoint giữa A và B
        Animator.SetBool("IsWalkBool", true);  // Chuyển sang trạng thái đi bộ
        isWaiting = false;  // Kết thúc trạng thái chờ
    }
    private IEnumerator StartAttack()
    {
        isAttacking = true;
        Animator.SetBool("IsWalkBool", false);  // Chuyển sang trạng thái idle
        yield return new WaitForSeconds(1f);  // Thời gian chờ trước khi tấn công
        Animator.SetTrigger("IsAttackTrigger");  // Chuyển sang trạng thái tấn công
        yield return new WaitForSeconds(2f);  // Thời gian tấn công
        isAttacking = false;
        Animator.SetBool("IsWalkBool", true);  // Trở lại trạng thái đi bộ
    }
    void Flip()
    {
        // Đổi hướng của kẻ thù
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
