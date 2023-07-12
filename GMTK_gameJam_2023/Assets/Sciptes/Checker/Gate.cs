using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Checker
{
    private int period;
    private GateController gate;
    private int timeCount;
    private bool direction;
    private float upBound;
    private float lowBound;

    public Gate(GateController gate, int period, float upBound, float lowBound){
        this.period=period;
        this.gate=gate;
        direction=true;
        enable=true;
        timeCount=0;
    }
    public override int Check(){
        if(!enable){
            return 0;
        }
        if(timeCount++%period==0){
            if(direction){
                gate.GateUp();
            }else{
                gate.GateDown();
            }
        }
        if(gate.transform.position.y>upBound||gate.transform.position.y<lowBound){
            gate.GateStop();
            direction=!direction;
        }
        return 1;
    }
    public override void Reset(){
        timeCount=0;
        gate.GateReset();
    }
    public override void Pause(){
        enable=false;
        gate.GateStop();
    }
    public override void Resume(){
        enable=true;
        if(timeCount++%period==0){
            if(direction){
                gate.GateUp();
            }else{
                gate.GateDown();
            }
        }
    }
}
