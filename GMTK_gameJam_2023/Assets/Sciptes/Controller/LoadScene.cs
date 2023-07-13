using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject comic1Prefab;

    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
    }
    public void back()
    {
        SceneManager.LoadScene("Start game");
    }
    public void again()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void firstStart()
    {
        Instantiate(comic1Prefab, gameObject.transform);
        audioManager.MusicChange(4);
        Invoke("again", 4.0f);
    }


    public void exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//编辑状态下退出
#else
        Application.Quit();//打包编译后退出
#endif
    }
}
