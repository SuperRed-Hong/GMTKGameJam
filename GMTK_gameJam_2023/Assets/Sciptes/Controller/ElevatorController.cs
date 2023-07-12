using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D elevator;
    [SerializeField] private float speed;
    [SerializeField] private float upBound;
    [SerializeField] private float lowBound;
    private Vector2 originPos;
    private bool inElevator;//人物是否在电梯内

    // Start is called before the first frame update
    void Start()
    {
        inElevator=false;//初始无人在电梯内
        originPos=transform.position;
        GameObject.Find("UIManager").GetComponent<PlayerManager>().AddChecker(new Elevator(this, upBound, lowBound));
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="Player"){
            inElevator=true;
        }
    }

    public void ElevatorUp(){
        elevator.velocity=new Vector2(0f, speed);
    }
 
    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag=="Player"){
            inElevator=false;
        }
    }

    public void ElevatorStop(){
        elevator.velocity=new Vector2(0f, 0f);
    }

    public void ElevatorDown(){
        elevator.velocity=new Vector2(0f, -speed);
    }

    public void ElevatorReset(){
        transform.position=originPos;
    }

    public bool getInElevator(){
        return inElevator;
    }

}
