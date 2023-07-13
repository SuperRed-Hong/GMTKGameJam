using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMap : MonoBehaviour
{
    public Transform map1;
    public Transform map2;
    public Transform map3;
    public Transform doctor;
    public Image doctorImage;
    public Sprite selectedImage;
    AudioManager audioManager;
    // Start is called before the first frame update

    private void Start()
    {
        audioManager = GameObject.Find("audioManager").GetComponent<AudioManager>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Debug.Log(mousePos.x);
        if(mousePos.x >= 171f && mousePos.x <= 594f)
        {
            doctor.position = new Vector3(map1.position.x, doctor.position.y, 0);
        }
        if(mousePos.x >= 747f && mousePos.x <= 1173f)
        {
            doctor.position = new Vector3(map2.position.x, doctor.position.y, 0);
        }
        if(mousePos.x >= 1323f && mousePos.x <= 1749f)
        {
            doctor.position = new Vector3(map3.position.x, doctor.position.y, 0);
        }
    }
    public void Map1Click()
    {
        audioManager.AudioPlay(5);
        map1.transform.position = new Vector3(map1.position.x, 0,0);
        doctorImage.sprite = selectedImage;
    }
    public void Map2Click()
    {
        audioManager.AudioPlay(5);
        map2.transform.position = new Vector3(map2.position.x, 0, 0);
        doctorImage.sprite = selectedImage;
    }
    public void Map3Click()
    {
        audioManager.AudioPlay(5);
        map3.transform.position = new Vector3(map3.position.x, 0, 0);
        doctorImage.sprite = selectedImage;
    }
}
