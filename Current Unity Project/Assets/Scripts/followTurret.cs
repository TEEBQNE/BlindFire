using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTurret : MonoBehaviour {

	public GameObject toFollow;
	public float yOffset = 0f;
	public float xOffset = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (toFollow.transform.position.x + xOffset, toFollow.transform.position.y + yOffset, transform.position.z);
	}
}
