using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("状态")]
    public bool isGround;
    [Header("检测参数")]

    public float checkRadius;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    public Vector2 bottomOffset1;
    public Vector2 bottomOffset2;


    public float aspectRatio;

    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer)||
            Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset1, checkRadius, groundLayer)||
            Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset2, checkRadius, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset1, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset2, checkRadius);
    }

}
