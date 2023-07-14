using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : Skill{
    private PlayerManager manager;
    private float range=4f;
    private float prepareTime=0.3f;
    private float influenceTime=5f;

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
            Debug.Log("Hit");
            player.GetOpponent().ChangeSpeed(0.5f);
            yield return new WaitForSeconds(influenceTime);
            player.GetOpponent().ResetSpeed();
        }
    }
}
