using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Skill
{
    private float duration=3f;
    public Shield(PlayerController player){
        this.player = player;
    }
    public override int UseSkill(){
        player.ChangeSpeed(1.3f);
        player.onInvincible();

        return 0;
    }
}
