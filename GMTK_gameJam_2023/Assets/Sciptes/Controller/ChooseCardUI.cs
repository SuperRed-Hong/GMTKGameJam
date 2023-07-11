using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCardUI : MonoBehaviour
{
    public Button left;
    public Button right;
    public Image image;
    public Image maskBg;

    private List<Sprite> _pics;

    private int _curPic;

    AudioSource audioSource1;

    private void Awake()
    {
        _pics = new List<Sprite>();
        _pics.Add(Resources.Load<Sprite>("t1"));
        _pics.Add(Resources.Load<Sprite>("t2"));
        _pics.Add(Resources.Load<Sprite>("t3"));
        _pics.Add(Resources.Load<Sprite>("t4"));
        _pics.Add(Resources.Load<Sprite>("t5"));
        _pics.Add(Resources.Load<Sprite>("t6"));

        _curPic = 0;

        audioSource1 = GameObject.Find("UIClick").GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource1.Play();
        UpdateImage();
    }

    public void OnLeftClick()
    {
        audioSource1.Play();
        _curPic--;
        UpdateImage();
    }

    public void OnRightClick()
    {
        audioSource1.Play();
        _curPic++;
        UpdateImage();
    }

    private void UpdateImage()
    {
        image.sprite = _pics[_curPic];
        if (_curPic == 0)
        {
            left.gameObject.SetActive(false);
        }
        else if (_curPic == _pics.Count - 1)
        {
            right.gameObject.SetActive(false);
        }
        else
        {
            left.gameObject.SetActive(true);
            right.gameObject.SetActive(true);
        }
    }

    public void Close()
    {
        audioSource1.Play();
        Destroy(gameObject);
    }
}
