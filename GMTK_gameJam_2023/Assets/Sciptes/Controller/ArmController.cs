using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    //[SerializeField] private CapsuleCollider2D hand;
    [SerializeField] private float growthRate;
    [SerializeField] private Renderer hand;
    [SerializeField] private Renderer longhand;
    [SerializeField] private PlayerController controller;
    private CapsuleCollider2D handCollider;
    private Transform player; // 对手玩家的Transform组件
    
    private Vector2 originalScale;
    private bool canCatch;
    
    void Awake(){
        handCollider=gameObject.GetComponent<CapsuleCollider2D>();
    }
    
    public void SetPlayer(Transform p)
    {
        player = p;
        originalScale = handCollider.size;
    }
    private void Update()
    {
        // 计算玩家相对于箭头的方向
        Vector3 direction = player.position - transform.position;

        // 将箭头旋转到指向玩家的方向
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (canCatch && collider.transform == player)
        {
            controller.GetManager().SetTouched(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider){
        if(canCatch && collider.transform==player){
            controller.GetManager().SetTouched(false);
        }
    }
    public void GrowArmLength()
    {
        Vector3 newScale = originalScale;
        newScale.x += 1.6f;
        handCollider.size = newScale;
        longhand.enabled=true;
    }
    public void ResetArmLength()
    {
        handCollider.size = originalScale;
        longhand.enabled=false;
    }
    public void SetUsable(bool visible)
    {
        hand.enabled = visible;
        longhand.enabled = false;
        canCatch = visible;
    }
}
