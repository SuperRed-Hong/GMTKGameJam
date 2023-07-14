using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowLifeTime : LifeTime
{
    private PlayerController player;
    public SlowLifeTime(PlayerController player, int lifeTime){
        this.lifeTime=lifeTime;
        this.player=player;
    }
    public override void Die(){
        player.ResetSpeed();
    }
}
