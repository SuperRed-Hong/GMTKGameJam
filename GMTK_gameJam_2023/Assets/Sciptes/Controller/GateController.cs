using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;//自己的刚体
    [SerializeField] private int period;
    [SerializeField] private float speed;
    private Vector2 originPos;//记录初始位置，重置用
    public float upBound;//最高点
    public float lowBound;//最低点
    // Start is called before the first frame update
    void Start()
    {
        originPos=transform.position;
        GameObject.Find("UIManager").GetComponent<PlayerManager>().AddChecker(new Gate(this, period, upBound, lowBound));
    }

    public void GateUp(){
        rb2D.velocity=new Vector2(0f, speed);
    }

    public void GateDown(){
        rb2D.velocity=new Vector2(0f, -speed);
    }

    public void GateStop(){
        rb2D.velocity=new Vector2(0f,0f);
    }
    public void GateReset(){
        transform.position=originPos;
    }

}
