
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

        musicPlayer.volume = PlayerPrefs.GetFloat("musicVolume", 1f);
        musicSlider.value = musicPlayer.volume;
        audioPlayer.volume = PlayerPrefs.GetFloat("audioVolume", 1f);
        audioSlider.value = audioPlayer.volume;

        musicSlider.onValueChanged.AddListener((value) =>
        {

            musicPlayer.volume = value;
            PlayerPrefs.SetFloat("musicVolume", value);
        });

        audioSlider.onValueChanged.AddListener((value) =>
        {

            audioPlayer.volume = value;
            PlayerPrefs.SetFloat("audioVolume", value);
        });
    }


    public void MusicChange(int i)
    {
        musicPlayer.Pause();
        musicPlayer.clip = music[i];
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
