using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    
    
    private void Awake()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        
    }
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("isGround", physicsCheck.isGround);
        animator.SetBool("isSlow", gameObject.GetComponent<PlayerController>().isSlowed);
        animator.SetBool("isflashing", gameObject.GetComponent<PlayerController>().isflashing);
        animator.SetBool("trapStun", gameObject.GetComponent<PlayerController>().trapStun);
    }
}
