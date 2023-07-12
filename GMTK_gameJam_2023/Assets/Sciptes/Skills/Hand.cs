using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Skill
{
    public Hand(PlayerController player){
        this.player=player;
    }
    public override int UseSkill(){
        return 0;
    }
}
