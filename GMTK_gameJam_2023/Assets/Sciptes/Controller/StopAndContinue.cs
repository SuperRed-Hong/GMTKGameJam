using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAndContinue : MonoBehaviour
{
    UIController uiController;
    AudioManager audioManager;
       private void Start()
    {
        uiController = GameObject.Find("UIManager").GetComponent<UIController>();
        audioManager = GameObject.Find("UIManager").GetComponent<AudioManager>();
    }
    public void close()
    {
        audioManager.AudioPlay(4);
        Time.timeScale = 1;
        uiController.resetStop();
        Destroy(gameObject);
    }

}
