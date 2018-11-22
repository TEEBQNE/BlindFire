using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockToPosition : MonoBehaviour {

	public GameObject toLockTo;

	// Use this for initialization
	void Start () {
		transform.position = Camera.main.WorldToScreenPoint(toLockTo.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
