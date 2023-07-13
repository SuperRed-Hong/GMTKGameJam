using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMap : MonoBehaviour
{
    public GameObject chooseMapPrefabs;
    public Transform canvasRoot;
    // Start is called before the first frame update


    public void chooseMapUI()
    {
        Instantiate(chooseMapPrefabs, canvasRoot);
    }
}
