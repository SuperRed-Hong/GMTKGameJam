using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryByTime : MonoBehaviour
{
    public float time = 6.0f;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <=0)
        {
            Destroy(gameObject);
        }
    }
}
