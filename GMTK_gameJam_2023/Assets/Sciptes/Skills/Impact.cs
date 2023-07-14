using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : Skill{
    private PlayerManager manager;
    private float range=4f;
    private float prepareTime=0.3f;
    private int lifetime=250;

    public Impact(PlayerManager manager, PlayerController player){
        this.player=player;
        this.manager=manager;
    }
    public override int UseSkill(){
        player.StartCoroutine(burst());
        return 0;
    }
    public IEnumerator burst(){
        player.onStunned();
        yield return new WaitForSeconds(prepareTime);
        player.offStunned();
        if(manager.DetectDistance()<range){
<<<<<<< HEAD
            player.GetOpponent().ChangeSpeed(0.5f);
            //manager.AddLifeTime(new SlowLifeTime(player.GetOpponent(),lifetime));
=======
            player.GetOpponent().ChangeSpeed(0.4f);
            manager.AddLifeTime(new SlowLifeTime(player.GetOpponent(),lifetime));
>>>>>>> 0030b5f11509ba132a417d58870b1ccc1113a19c
        }
    }
}
