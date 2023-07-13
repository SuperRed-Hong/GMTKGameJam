using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCard : MonoBehaviour
{
    ChooseCardUI chooseCardUI;
    private PlayerManager playerManager;
    public Transform dcard;
    public Transform pcard;


    private void Start()
    {
        chooseCardUI = GetComponent<ChooseCardUI>();
        playerManager = GetComponent<PlayerManager>();

    }

    public void SetCardPosition()
    {
        if (!playerManager.returnWinner())
        {
            gameObject.transform.position = dcard.transform.position;
            gameObject.tag = "selectedCard";
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Cards");
            foreach(GameObject card in cards)
            {
                Destroy(card);
            }
            chooseCardUI.Close();
        }
        else
        {
            gameObject.transform.position = pcard.transform.position;
            gameObject.tag = "selectedCard";
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Cards");
            foreach (GameObject card in cards)
            {
                Destroy(card);
            }
            chooseCardUI.Close();
        }
    }
}
