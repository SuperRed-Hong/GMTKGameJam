using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    //[SerializeField] private CapsuleCollider2D hand;
    [SerializeField] private float growthRate;
    [SerializeField] private Renderer hand;
    [SerializeField] private PlayerController controller;
    private Transform player; // 对手玩家的Transform组件
    
    private Vector3 originalScale;
    private bool canCatch;
    private bool end = false;
    private AudioManager audioManager;
    
    public void SetPlayer(Transform p)
    {
        player = p;
        originalScale = transform.localScale;
    }

    void Start()
    {
        audioManager = GameObject.Find("UIManager").GetComponent<AudioManager>();
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
            audioManager.AudioPlay(4);
            if (!end)
            {
                controller.Win();
                end = true;
            }
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D collider){
        if(collider.transform==player){
            canCatch=false;
        }
    }*/
    public void GrowArmLength()
    {
        Vector3 newScale = transform.localScale;
        newScale.x += growthRate * Time.deltaTime;
        newScale.y += growthRate * Time.deltaTime;
        transform.localScale = newScale;
    }
    public void ResetArmLength()
    {
        transform.localScale = originalScale;
    }
    public void SetUsable(bool visible)
    {
        hand.enabled = visible;
        canCatch = visible;
    }
}
