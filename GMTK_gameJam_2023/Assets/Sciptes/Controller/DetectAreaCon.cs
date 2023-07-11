using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DetectAreaCon : MonoBehaviour
{
    public GameObject Playerhand;
    private bool isDetecting=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDetecting)
        {
            Playerhand.GetComponent<ArmController>().GrowArmLength();
            Debug.Log(" detecting patient");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            isDetecting = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDetecting = false;
            Playerhand.GetComponent<ArmController>().ResetArmLength();
        }
    }

}
