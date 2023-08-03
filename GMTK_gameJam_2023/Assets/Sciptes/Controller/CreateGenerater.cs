using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGenerater : MonoBehaviour
{
    public GameObject flowerGenerater;
    public Transform generaterTranform;
    GameObject flowerGenerateClone = null;
    private void Awake()
    {
        flowerGenerateClone = GameObject.Find("FlowerGenerater(Clone)");
    }
    private void Start()
    {
        if(flowerGenerateClone == null)
        {
            createGenerater();
        }
    }
    public void createGenerater()
    {
        Instantiate(flowerGenerater, generaterTranform);
        flowerGenerateClone = GameObject.Find("FlowerGenerater(Clone)");
    }

    public void destoryGenerater()
    {
        if(flowerGenerateClone != null)
        {
            Destroy(flowerGenerateClone);
        }
    }
}
