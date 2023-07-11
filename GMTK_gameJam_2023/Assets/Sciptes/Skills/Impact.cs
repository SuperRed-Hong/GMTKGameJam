using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : Skill{
    private PlayerManager manager;
    [SerializeField] private float range=5f;
    [SerializeField] private float prepareTime=1f;
    [SerializeField] private float influenceTime=5f;

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
            player.GetOpponent().ChangeSpeed(0.7f);
            yield return new WaitForSeconds(influenceTime);
            player.GetOpponent().ResetSpeed();
        }
    }
}
