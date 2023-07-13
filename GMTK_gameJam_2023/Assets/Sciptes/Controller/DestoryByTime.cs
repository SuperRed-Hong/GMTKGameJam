using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryByTime : MonoBehaviour
{
    public AudioManager audioManager;
    public float time = 6.0f;

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
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        Debug.Log(time);
        if(time <=0)
        {
            audioManager.MusicChange(0);
            Destroy(gameObject);
        }
    }

}
