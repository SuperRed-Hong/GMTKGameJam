using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier1 : MonoBehaviour
{
    float time = 10f;
    Collider player1;
    private void Awake()
    {
        player1 = GameObject.Find("player1(Clone)").GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player1);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
