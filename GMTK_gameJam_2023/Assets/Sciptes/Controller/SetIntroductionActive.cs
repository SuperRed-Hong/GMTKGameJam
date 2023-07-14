using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetIntroductionActive : MonoBehaviour
{
    public GameObject image;
    public void setActive()
    {
        image.SetActive(true);
    }
    public void setUnActive()
    {
        image.SetActive(false);
    }

}
