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
        this.manager=manager;
        RefreshCount=(int)(1f/0.02f);
        timeCount=0;
    }
    public override int Check(){
        RemainTime-=0.02f;
        if((timeCount++)%RefreshCount==0){
            manager.RefreshTime(RemainTime);
        }
        if(RemainTime<=0){
            manager.whoWin(false);
            manager.EndGame(false);
        }
        return 0;
    }
}
