using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : Skill
{
    public Flash(PlayerController player){
        this.player=player;
    }
    public override int UseSkill(){
        player.transform.Translate(new Vector2(0f, 2f));
        return 0;
    }
}
