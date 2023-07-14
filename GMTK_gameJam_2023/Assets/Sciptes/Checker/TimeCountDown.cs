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
        if(manager.GetTouched() && !manager.GetPlayer1().getInvincible() && !manager.GetPlayer2().getInvincible()){
            manager.whoWin(true);
            manager.EndGame(true);
        }
        if(RemainTime<=0){
            manager.RefreshTime(RemainTime);
            manager.whoWin(false);
            manager.EndGame(false);
        }
        return 1;
    }
    public override void Reset(){
        RemainTime=30f;
        timeCount=0;
    }
    public override void Pause(){
        enable=false;
    }
    public override void Resume(){
        enable=true;
    }
}
