using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpHand : Checker
{
    private float growMaxDistance=4f;
    private int growMaxTime=1000;
    private int growTime=0;
    private ArmController arm;
    public HelpHand(PlayerManager manager, ArmController arm){
        enable=false;
        this.manager=manager;
        this.arm=arm;
        growTime=0;
    }
    public override int Check(){
        if(!enable){
            return 0;
        }
        if(manager.DetectDistance()<growMaxDistance){
            if(growTime<growMaxTime){
                ++growTime;
                arm.GrowArmLength();
            }
        }else{
            growTime=0;
            arm.ResetArmLength();
        }
        return 1;
    }
    public override void Reset(){
        growTime=0;
        arm.ResetArmLength();
    }
    public override void Pause(){
        enable=false;
    }
    public override void Resume(){
        enable=true;
    }
}
