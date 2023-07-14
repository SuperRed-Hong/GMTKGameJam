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
            player.transform.Translate(new Vector2(player.getMoveHorizontal()*2.5f, player.getMoveVertical()*2.5f));
            player.playflash();

            return 1;

        }
        if (player.getMoveHorizontal()!=0){
            player.transform.Translate(new Vector2(player.getMoveHorizontal()*2f,0f));
            player.playflash();

            return 1;
        }
        if(player.getMoveVertical()!=0){
            player.transform.Translate(new Vector2(0f,player.getMoveVertical()*2f));
            player.playflash();

            return 1;
        }
        player.playflash();

        return 0;
    }
    
}
