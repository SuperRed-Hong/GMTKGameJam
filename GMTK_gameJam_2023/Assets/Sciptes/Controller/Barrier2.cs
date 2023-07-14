using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier2 : MonoBehaviour
{
    float time = 10f;
    Collider player2;
    private void Awake()
    {
        player2 = GameObject.Find("player2(Clone)").GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player2);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
