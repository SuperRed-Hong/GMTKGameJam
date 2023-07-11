using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAndContinue : MonoBehaviour
{
    public void close()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

}
