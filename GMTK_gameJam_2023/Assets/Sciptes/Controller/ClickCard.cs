using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCard : MonoBehaviour
{
    ChooseCardUI chooseCardUI;
    UIController uiController;
    private PlayerManager playerManager;
    Image dcard;
    Image pcard;
    private List<Sprite> imagesd;
    private List<Sprite> imagesp;
    AudioManager audioManager;

    private void Start()
    {
        dcard = GameObject.Find("dcard1").GetComponent<Image>();
        pcard = GameObject.Find("pcard1").GetComponent<Image>();
        chooseCardUI = GameObject.Find("chooseCardUI(Clone)").GetComponent<ChooseCardUI>();
        playerManager = GameObject.Find("UIManager").GetComponent<PlayerManager>();
        uiController = GameObject.Find("UIManager").GetComponent<UIController>();
        audioManager = GameObject.Find("UIManager").GetComponent<AudioManager>();

        imagesd = new List<Sprite>();
        imagesd.Add(Resources.Load<Sprite>("Art/Cards/金钟罩d"));
        imagesd.Add(Resources.Load<Sprite>("Art/Cards/减速d"));
        imagesd.Add(Resources.Load<Sprite>("Art/Cards/闪现d"));
        imagesd.Add(Resources.Load<Sprite>("Art/Cards/障碍d"));
        imagesd.Add(Resources.Load<Sprite>("Art/Cards/伸手d"));

        imagesp = new List<Sprite>();
        imagesp.Add(Resources.Load<Sprite>("Art/Cards/金钟罩p"));
        imagesp.Add(Resources.Load<Sprite>("Art/Cards/减速p"));
        imagesp.Add(Resources.Load<Sprite>("Art/Cards/闪现p"));
        imagesp.Add(Resources.Load<Sprite>("Art/Cards/障碍p"));
        imagesp.Add(Resources.Load<Sprite>("Art/Cards/伸手p"));
    }

    public void SetCardPosition()
    {
        if (!playerManager.returnWinner())
        {
            audioManager.AudioPlay(0);
            setSkillSprite();
            dcard.color = new Color(255, 255, 255, 255);
            chooseCardUI.Close();
            uiController.nextTurn();
        }
        else
        {
            audioManager.AudioPlay(0);
            setSkillSprite();
            pcard.color = new Color(255, 255, 255, 255);
            chooseCardUI.Close();
            uiController.nextTurn();
        }
    }
    public void setSkillSprite()
    {
        switch (gameObject.name)
        {
            case "金钟罩d(Clone)":
                dcard.sprite = imagesd[0];
                break;
            case "减速d(Clone)":
                dcard.sprite = imagesd[1];
                break;
            case "闪现d(Clone)":
                dcard.sprite = imagesd[2];
                break;
            case "障碍d(Clone)":
                dcard.sprite = imagesd[3];
                break;
            case "伸手d(Clone)":
                dcard.sprite = imagesd[4];
                break;
            case "金钟罩p(Clone)":
                pcard.sprite = imagesp[0];
                break;
            case "减速p(Clone)":
                pcard.sprite = imagesp[1];
                break;
            case "闪现p(Clone)":
                pcard.sprite = imagesp[2];
                break;
            case "障碍p(Clone)":
                pcard.sprite = imagesp[3];
                break;
            case "伸手p(Clone)":
                pcard.sprite = imagesp[4];
                break;
            default:
                break;
        }
    }
}
