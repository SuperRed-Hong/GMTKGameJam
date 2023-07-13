using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : Skill
{
    public Flash(PlayerController player){
        this.player=player;
    }
    public override int UseSkill(){
        if(player.getMoveHorizontal()!=0&&player.getMoveVertical()!=0){
            player.transform.Translate(new Vector2(player.getMoveHorizontal()*1.41f, player.getMoveVertical()*1.41f));
            return 1;
        }
        if(player.getMoveHorizontal()!=0){
            player.transform.Translate(new Vector2(player.getMoveHorizontal()*2f,0f));
            return 1;
        }
        if(player.getMoveVertical()!=0){
            player.transform.Translate(new Vector2(0f,player.getMoveVertical()*2f));
            return 1;
        }
        return 0;
    }
}
