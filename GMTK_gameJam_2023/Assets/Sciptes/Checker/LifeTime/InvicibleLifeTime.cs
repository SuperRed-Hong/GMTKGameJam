using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvicibleLifeTime : LifeTime
{
    private PlayerController player;
    public InvicibleLifeTime(PlayerController player, int lifeTime){
        this.lifeTime=lifeTime;
        this.player=player;
    }
    public override void Die(){
        player.offInvincible();
        player.ResetSpeed();
    }
}
