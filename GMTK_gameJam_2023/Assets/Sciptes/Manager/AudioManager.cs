
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] music;
    public AudioClip[] audios;
    public AudioSource musicPlayer;
    public AudioSource audioPlayer;

    public Slider musicSlider;
    public Slider audioSlider;

    private void Start()
    {


        musicSlider.value = musicPlayer.volume;
        audioSlider.value = audioPlayer.volume;

        musicSlider.onValueChanged.AddListener((value) =>
        {
            musicPlayer.volume = value;
        });

        audioSlider.onValueChanged.AddListener((value) =>
        {
            audioPlayer.volume = value;
        });
    }


    public void MusicChange(int i)
    {
        float timeNow = musicPlayer.time;
        musicPlayer.Pause();
        musicPlayer.clip = music[i];
        musicPlayer.time = timeNow;
        musicPlayer.Play();
    }


    public void AudioPlay(int i)
    {
        //Debug.Log("播放" + i);
        audioPlayer.Pause();
        audioPlayer.clip = audios[i];
        audioPlayer.time = 0.0f;
        audioPlayer.Play();
    }
}
