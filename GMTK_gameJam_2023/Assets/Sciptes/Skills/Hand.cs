using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Skill
{
    private PlayerManager manager;
    private int lifeTime=500;
    public Hand(PlayerManager manager, PlayerController player){
        this.player=player;
        this.manager=manager;
    }
    public override int UseSkill(){
        player.GetArm().GrowArmLength();
        manager.AddLifeTime(new HandLifeTime(player.GetArm(),lifeTime));
        return 0;
    }
}
