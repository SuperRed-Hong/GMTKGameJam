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
        player.StartCoroutine(Protect());
        return 0;
    }
    public IEnumerator Protect(){
        player.ChangeSpeed(1.3f);
        player.onInvincible();
        yield return new WaitForSeconds(duration);
        player.offInvincible();
        player.ResetSpeed();
    }
}
