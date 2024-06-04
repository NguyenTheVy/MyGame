using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[AddComponentMenu("DangSon/EnemyAI")]
public class EnemyAI : MonoBehaviour
{
    #region Public variable
    public Transform pointA;
    public Transform pointB;
    public float speedMove = 1f;
    public float speedPatrol = 1f;
    public float speedChasing = 1.5f;
    public float MinDistance = 0.2f;
    public float idleTime;
    public float baseIdleTime = 2f;
    public float chaseRange = 4f;
    #endregion
    #region Private Variable
    private int isIdleAnimtionId = Animator.StringToHash("isIdle");
    private bool isWalk = false;
    private Rigidbody2D rb;
    private Transform target;
    private Animator anim;
    private GameObject player;
   private  bool isChasing=false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        transform.position = pointA.position;
        target = pointB;
        idleTime = baseIdleTime;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        float distancePlayer = Vector2.Distance(transform.position, player.transform.position);
        if(distancePlayer < chaseRange) 
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
        if(isChasing)
        {
            Chasing();
        }
        else
        {
            Partrol();
        }

        Vector2 scale = transform.localScale;
        float x = player.transform.position.x - transform.position.x;
        if (x < 0 && isChasing)
        {
            scale.x = -(math.abs(scale.x));
        }
        else if (x > 0 && isChasing)
        {
            scale.x = (math.abs(scale.x));
        }
        if ((transform.position.x - pointA.position.x < 0) && (transform.position.x - pointB.position.x < 0) && !isChasing)
        {
            scale.x = (math.abs(scale.x));
        }
        else if ((transform.position.x - pointA.position.x > 0) && (transform.position.x - pointB.position.x > 0) && !isChasing)
        {
            scale.x = -(math.abs(scale.x));
        }
        transform.localScale = scale;
        IdleAnimation();
    }
    private void Chasing()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction* speedChasing;
        anim.SetBool(isIdleAnimtionId, false);
       
    }
    private void Partrol()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;
        if (Vector2.Distance(transform.position, target.position) < MinDistance)
        {
            target = target == pointB ? pointA : pointB;
            anim.SetBool(isIdleAnimtionId, true);

            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            speedMove = 0;
            isWalk = false;
        }
        rb.velocity = direction * speedMove;
    }
    void IdleAnimation()
    {
        if (!isWalk)
        {
            idleTime -= Time.deltaTime;
            if (idleTime <= 0)
            {
                anim.SetBool(isIdleAnimtionId, false);
                idleTime = baseIdleTime;
                speedMove = speedPatrol;
                isWalk = true;
            }
        }
    }
}
