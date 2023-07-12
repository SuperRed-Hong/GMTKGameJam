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
        audioManager.MusicChange(2);
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <=0)
        {
            audioManager.MusicChange(0);
            Destroy(gameObject);
        }
    }

}
