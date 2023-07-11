using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGenerater : MonoBehaviour
{
	public GameObject[] Flowers;


	// Use this for initialization
	void Start()
	{
		InvokeRepeating("CreateFlower", 0.5f, 1f);
	}


	void CreateFlower()
	{
		Instantiate(Flowers[Random.Range(0, Flowers.Length)]);
	}



}
