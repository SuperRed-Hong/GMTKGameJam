using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject comic1Prefab;
    GameObject flowerGenerater;

    private void Start()
    {
        audioManager = GameObject.Find("Canvas").GetComponent<AudioManager>();
        flowerGenerater = GameObject.Find("FlowerGenerater");
    }
    public void back()
    {
        SceneManager.LoadScene("Start game");
    }
    public void map1()
    {
        SceneManager.LoadScene("MainScene1");
    }
    public void map2()
    {
        SceneManager.LoadScene("MainScene2");
    }
    public void map3()
    {
        SceneManager.LoadScene("MainScene3");
    }
    public void firstStartmap1()
    {
        if (flowerGenerater != null)
        {
            Destroy(flowerGenerater);
        }
        Invoke("comic1Play", 0.5f);
        Invoke("map1", 4.5f);
    }
    public void firstStartmap2()
    {
        Invoke("comic1Play", 0.5f);
        Invoke("map2", 4.5f);
    }
    public void firstStartmap3()
    {
        Invoke("comic1Play", 0.5f);
        Invoke("map3", 4.5f);
    }
    public void comic1Play()
    {
        Instantiate(comic1Prefab, gameObject.transform);
        audioManager.MusicChange(4);
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
