using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingAnimFollow : MonoBehaviour {

	Transform toFollow;
	public float yOffSet;
	public float xOffSet;

	// Use this for initialization
	void Start () {
		toFollow = GameObject.Find ("playerMove").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (toFollow.transform.position.x + xOffSet, toFollow.transform.position.y + yOffSet, gameObject.transform.position.z);
	}
}