using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowers : MonoBehaviour
{
	public Vector2 velocity = new Vector2(0, -4);
	public float range = 2;
	float time = 5.0f;
	GameObject flowerGenerater;
	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = velocity;
		transform.position = new Vector3(transform.position.x - range * Random.value, transform.position.y , transform.position.z);
		Destroy(gameObject, 10);
		
	}
	void Update()
	{
		flowerGenerater = GameObject.Find("FlowerGenerater");
		time -= Time.deltaTime;
		if(time <= 0)
        {
			Destroy(gameObject);
        }
		if (flowerGenerater == null)
		{
			Destroy(gameObject);
		}
	}
}
