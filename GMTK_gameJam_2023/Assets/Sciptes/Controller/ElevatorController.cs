using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Rigidbody2D elevator;
    private int state;//运行状态
    private bool inElevator;//人物是否在电梯内
    [SerializeField] private float speed;
    [SerializeField] private float period;
    // Start is called before the first frame update
    void Start()
    {
        state=0;//初始状态（停在底部）
        inElevator=false;//初始无人在电梯内
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="Player"){
            inElevator=true;
            StartCoroutine(ElevatorUp());
        }
    }

    private IEnumerator ElevatorUp(){
        if(state==0 && inElevator){//从底部开始运行
            state=1;//上升状态
            elevator.velocity=new Vector2(0f, speed);
            yield return new WaitForSeconds(period);
            elevator.velocity=new Vector2(0f, 0f);
            state=2;//到站状态
            yield return ElevatorDown();
        }
        
    }
 
    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag=="Player"){
            inElevator=false;
            StartCoroutine(ElevatorDown());
        }
    }

    private IEnumerator ElevatorDown(){
        if(state==2 && !inElevator){//从顶部开始运行
            state=3;//下降状态
            elevator.velocity=new Vector2(0f, -speed);
            yield return new WaitForSeconds(period);
            elevator.velocity=new Vector2(0f, 0f);
            state=0;//初始状态
            yield return ElevatorUp();
        }
    }

}
