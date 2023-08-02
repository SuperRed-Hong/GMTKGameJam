using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : Skill
{
    public Flash(PlayerController player){
        this.player=player;
    }
    public override int UseSkill(){
        Vector2 moveDirection=new Vector2(player.getMoveHorizontal(), player.getMoveVertical()).normalized;
        float moveDistance=0f;
        RaycastHit2D hit1 = Physics2D.Raycast(((Vector2)player.transform.position)+moveDirection*3f, moveDirection);
        if(hit1.fraction==0f){
            RaycastHit2D hit2 = Physics2D.Raycast(player.transform.position, moveDirection);
            moveDistance=hit2.distance;
        }else{
            moveDistance=3f;
        }
        player.transform.Translate(moveDirection*moveDistance);
        player.playflash();
        return 1;
    }
    
}
