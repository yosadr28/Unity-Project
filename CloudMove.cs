using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {
	public float speedAwan = -1.2f;
	public GameObject awan;
	public Camera mainCam;
	public Vector2 widthThresold;
	public Vector2 heightThresold;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		transform.position = transform.position + new Vector3(speedAwan * Time.deltaTime, 0, 0);
		/*Vector2 screenPos = mainCam.WorldToScreenPoint(transform.position);
		if (screenPos.x < widthThresold.x || screenPos.x > widthThresold.y || screenPos.y <heightThresold.x || screenPos.y > heightThresold.y)
        {
			Destroy(awan);
        }*/
    }

	void OnBecameInvisible()
    {
		print("Invicible");
		Destroy(awan);
    }

}
