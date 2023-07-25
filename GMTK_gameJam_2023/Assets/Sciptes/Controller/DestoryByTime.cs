using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestoryByTime : MonoBehaviour
{
    public AudioManager audioManager;
    public float time = 6.0f;
    float countTime = 0;

    private void Awake()
    {
        audioManager = GameObject.Find("Canvas").GetComponent<AudioManager>();
    }
    private void Start()
    {
        if(time == 6.0f)
        {
            audioManager.MusicChange(2);
        }
        if(time == 5.0f)
        {
            audioManager.MusicChange(4);
        }
        if(time == 4.0f)
        {
            countTime = 4.0f;
            audioManager.MusicChange(5);
        }
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        //Debug.Log(time);
        if(time <=0)
        {
            if(countTime == 4.0f)
            {
                SceneManager.LoadScene("Start game");
            }
            audioManager.MusicChange(0);
            Destroy(gameObject);
        }
    }

}
