using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetHealthBar : MonoBehaviour {

	Vector3 toSet;

	// Use this for initialization
	void Start () {
		toSet = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = toSet;
	}
}
