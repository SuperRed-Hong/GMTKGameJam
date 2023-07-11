using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveSkillController : MonoBehaviour
{
    private PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager=GameObject.Find("Managers").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController playerCaught = other.gameObject.GetComponent<PlayerController>();
            //playerCaught.SetSkill(new Flash(playerCaught));
            playerCaught.SetSkill(new Impact(playerManager, playerCaught));
        }
    }
}
