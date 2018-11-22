using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resizeGooShot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(1f + (transform.parent.transform.Find("visionCollider").GetComponent<CircleCollider2D>().radius - 1.4f), 1f + (transform.parent.transform.Find("visionCollider").GetComponent<CircleCollider2D>().radius - 1.4f), 1f);
	}
}
