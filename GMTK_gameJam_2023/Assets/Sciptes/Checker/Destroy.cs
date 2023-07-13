using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : Checker
{
    private List<LifeTime> lifeTimeList;

    public Destroy(){
        lifeTimeList=new List<LifeTime>();
    }
    
    public override int Check(){
        if(!enable){
            return 0;
        }
        foreach(LifeTime lt in lifeTimeList){
            if(--lt.lifeTime<=0){
                lt.Die();
                lifeTimeList.Remove(lt);
            }
        }
        return 1;
    }
    public override void Reset(){
        lifeTimeList.Clear();
    }
    public override void Pause(){
        enable=false;
    }
    public override void Resume(){
        enable=true;
    }
    public void AddLifeTime(LifeTime lifetime){
        lifeTimeList.Add(lifetime);
    }
}
