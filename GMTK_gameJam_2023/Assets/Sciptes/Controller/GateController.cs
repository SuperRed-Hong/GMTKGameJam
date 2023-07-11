using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;//自己的刚体
    [SerializeField] private float startTime;
    [SerializeField] private float timeCost;
    [SerializeField] private float period;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GateUp",startTime,period*2);
        InvokeRepeating("GateStop",startTime+timeCost,period);
        InvokeRepeating("GateDown",startTime+period,period*2);
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
