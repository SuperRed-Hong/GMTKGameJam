using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Skill
{
    private int duration=150;
    public Shield(PlayerController player){
        this.player = player;
    }
    public override int UseSkill(){
        player.ChangeSpeed(1.3f);
        player.onInvincible();
        player.GetManager().AddLifeTime(new InvicibleLifeTime(player, duration));
        return 0;
    }
}
