using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Skill
{
    private GameObject barrierPrefeb;
    private int lifetime = 500;
    private GameObject InstantiateBarrier;
    public Barrier(PlayerController player, GameObject barrierPrefeb){
        this.player=player;
        this.barrierPrefeb=barrierPrefeb;
    }
    public override int UseSkill(){
        //TODO: 在player当前位置创建barrierPrefeb
        InstantiateBarrier=Object.Instantiate(barrierPrefeb,player.transform.position,barrierPrefeb.transform.rotation);
        player.GetManager().AddLifeTime(new BarrierLifeTime(InstantiateBarrier,lifetime));
        
        Physics2D.IgnoreCollision(player.GetCollider(),InstantiateBarrier.GetComponent<Collider2D>());
        return 0;
    }
}
