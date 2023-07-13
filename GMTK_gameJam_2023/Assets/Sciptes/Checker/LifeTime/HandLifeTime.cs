using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLifeTime : LifeTime
{
    private ArmController arm;
    public HandLifeTime(ArmController arm, int lifeTime){
        this.arm=arm;
        this.lifeTime=lifeTime;
    }
    public override void Die(){
        arm.ResetArmLength();
    }
}
