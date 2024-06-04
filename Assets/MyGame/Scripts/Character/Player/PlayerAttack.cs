using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    public Transform attackPoint;
    public float radiusAtk = 0.8f;
    private bool canAttack = true;  // Thêm biến này
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && canAttack)
        {
            //StartCoroutine(Attack());
            animator.SetTrigger("IsAttack");

        }



    }

    /*private IEnumerator Attack()
    {
        canAttack = false;  // Ngăn chặn tấn công ngay lập tức
        animator.SetTrigger("IsAttack");
        yield return new WaitForSeconds(1f);  // Thời gian chờ 1 giây
        canAttack = true;  // Cho phép tấn công lại sau thời gian chờ
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if(attackPoint != null)
        {
            Gizmos.DrawSphere(attackPoint.position, radiusAtk);
        }
    }
}
