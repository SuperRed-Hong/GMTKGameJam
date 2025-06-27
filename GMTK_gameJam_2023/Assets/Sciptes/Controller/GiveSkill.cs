using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GiveSkill : MonoBehaviour
{
    private PlayerController playerController;
    Image dcard;
    Image pcard;
    private PlayerManager manager;
    TextMeshProUGUI dcardname;
    TextMeshProUGUI pcardname;
    public GameObject barrierPrefab;
    private void Awake()
    {
        dcard = GameObject.Find("dcard1").GetComponent<Image>();
        pcard = GameObject.Find("pcard1").GetComponent<Image>();
        playerController = GetComponent<PlayerController>();
        dcardname = GameObject.Find("dcardname").GetComponent<TextMeshProUGUI>();
        pcardname = GameObject.Find("pcardname").GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        manager = playerController.GetManager();
        if (dcard.sprite != null)
        {
            if (playerController.GetCharacter())
            {
                giveSkill(dcard.sprite.name);
            }
        }
        if (pcard.sprite != null)
        {
            if (!playerController.GetCharacter())
            {
                giveSkill(pcard.sprite.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.GetCurrentSkill())
        {
            if (playerController.GetCharacter())
            {
                dcard.sprite = null;
                dcard.color = new Color(255, 255, 255, 0);
                dcardname.text = "您暂时没有卡牌~";
            }
            if(!playerController.GetCharacter())
            {
                pcard.sprite = null;
                pcard.color = new Color(255, 255, 255, 0);
                pcardname.text = "您暂时没有卡牌~";
            }

        }
    }
    public void giveSkill(string cardName)
    {
        switch (cardName)
        {
            case "金钟罩d":
                if (!playerController.GetRole())
                {
                    playerController.SetSkill(new Shield(playerController));
                }
                dcardname.text = "金钟罩";
                break;
            case "减速d":
                playerController.SetSkill(new Impact(manager, playerController));
                dcardname.text = "震荡波";
                break;
            case "闪现d":
                playerController.SetSkill(new Flash(playerController));
                dcardname.text = "闪现";
                break;
            case "障碍d":
                playerController.SetSkill(new Barrier(playerController, barrierPrefab));
                dcardname.text = "超级路障";
                break;
            case "伸手d":
                if (playerController.GetRole())
                {
                    playerController.SetSkill(new Hand(manager, playerController));
                }
                dcardname.text = "麒麟臂";
                break;
            case "金钟罩p":
                if (!playerController.GetRole())
                {
                    playerController.SetSkill(new Shield(playerController));
                }
                pcardname.text = "金钟罩";
                break;
            case "减速p":
                playerController.SetSkill(new Impact(manager, playerController));
                pcardname.text = "震荡波";
                break;
            case "闪现p":
                playerController.SetSkill(new Flash(playerController));
                pcardname.text = "闪现";
                break;
            case "障碍p":
                playerController.SetSkill(new Barrier(playerController, barrierPrefab));
                pcardname.text = "超级路障";
                break;
            case "伸手p":
                if (playerController.GetRole())
                {
                    playerController.SetSkill(new Hand(manager, playerController));
                }
                pcardname.text = "麒麟臂";
                break;
            default:
                break;
        }
    }
}
