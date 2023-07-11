using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    private UIController _boardController;

    AudioSource audioSource1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource1 = GameObject.Find("UIClick").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
