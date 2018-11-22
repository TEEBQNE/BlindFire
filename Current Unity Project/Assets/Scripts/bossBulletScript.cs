using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBulletScript : MonoBehaviour {

	public float timeToDelete = 5.0f;
	float currentTime = 0.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime >= timeToDelete) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<MovementTest> ().health--;
			Destroy (gameObject);
		}
	}
}
