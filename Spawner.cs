using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


	public int timerSpawner = 20;
	public GameObject Cloud;
	public Camera cam;
	private bool spawn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timerSpawner > 0)
        {
			timerSpawner--;
			spawn = false;
        }
		if (timerSpawner < 1)
        {
			timerSpawner = 0;
			spawn = true;
        }

		if (spawn)
		{
			Instantiate(Cloud, new Vector3(transform.position.x, Random.Range(9.0f, -3.0f), transform.position.z), transform.rotation);
			timerSpawner = 20;
		}
	}


}
