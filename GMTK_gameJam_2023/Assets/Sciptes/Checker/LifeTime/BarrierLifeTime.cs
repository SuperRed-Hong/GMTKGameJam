using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierLifeTime : LifeTime
{
    public GameObject barrier;
    public BarrierLifeTime(GameObject barrier, int lifeTime){
        this.barrier=barrier;
        this.lifeTime=lifeTime;
    }
    public override void Die(){
        Object.Destroy(barrier);
    }
}
