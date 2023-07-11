using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int doctorScore;
    private int patientScore;
    private PlayerManager playerManager;
    public void Awake(){
        DontDestroyOnLoad(this);
    }
    public void Reset(){
        doctorScore=0;
        patientScore=0;
    }
    public void EndGame(){
        return;
    }
    public void SelfDestroy(){
        Destroy(this.gameObject);
    }
}
