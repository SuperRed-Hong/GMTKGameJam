using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject comic1Prefab;
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
        Invoke("again", 6.0f);
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
