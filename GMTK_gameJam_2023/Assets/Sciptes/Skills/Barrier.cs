using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Skill
{
    private GameObject barrierPrefeb;
    private int lifetime;
    public Barrier(PlayerController player, GameObject barrierPrefeb){
        this.player=player;
        this.barrierPrefeb=barrierPrefeb;
    }
    public override int UseSkill(){
        //TODO: 在player当前位置创建barrierPrefeb
        player.GetManager().AddLifeTime(new BarrierLifeTime(barrierPrefeb,lifetime));
        return 0;
    }
}
