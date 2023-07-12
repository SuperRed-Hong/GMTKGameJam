using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCountDown : Checker
{
    private float RemainTime=30f;
    private int timeCount=0;
    private int RefreshCount;
    private ArmController arm;
    
    
    public TimeCountDown(PlayerManager manager){
        enable=true;
        this.manager=manager;
        RefreshCount=(int)(1f/0.02f);
        timeCount=0;
    }
    public override int Check(){
        if(!enable){
            return 0;
        }
        RemainTime-=0.02f;
        if((timeCount++)%RefreshCount==0){
            manager.RefreshTime(RemainTime);
        }
        if(RemainTime<=0){
            manager.EndGame(false);
        }
        return 1;
    }
    public override void Reset(){
        RefreshCount=(int)(1f/0.02f);
        timeCount=0;
    }
    public override void Pause(){
        enable=false;
    }
    public override void Resume(){
        enable=true;
    }
}
