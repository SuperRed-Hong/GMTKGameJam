using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Checker
{
    private ElevatorController elevator;
    private int state;
    private float upBound;
    private float lowBound;

    public Elevator(ElevatorController elevator, float upBound, float lowBound){
        this.elevator=elevator;
        this.upBound=upBound;
        this.lowBound=lowBound;
        enable=true;
        state=0;
    }
    public override int Check(){
        if(!enable){
            return 0;
        }
        switch(state){
            case 0:
                if(elevator.getInElevator()){
                    elevator.ElevatorUp();
                    state=1;
                }
                break;
            case 1:
                if(elevator.transform.position.y>upBound){
                    elevator.ElevatorStop();
                    state=2;
                }
                break;
            case 2:
                if(!elevator.getInElevator()){
                    elevator.ElevatorDown();
                    state=3;
                }
                break;
            case 3:
                if(elevator.transform.position.y<lowBound){
                    elevator.ElevatorStop();
                    state=0;
                }
                break;
        }
        return 1;
    }
    public override void Reset(){
        elevator.ElevatorReset();
        state=0;
    }
    public override void Pause(){
        enable=false;
        elevator.ElevatorStop();
    }
    public override void Resume(){
        enable=true;
        switch(state){
            case 1:
                elevator.ElevatorUp();
                break;
            case 3:
                elevator.ElevatorDown();
                break;
        }
    }
}
