using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public void timeStop()
    {
        Time.timeScale = 0;
    }

    public void timeFlow()
    {
        Time.timeScale = 1;
    }
}
