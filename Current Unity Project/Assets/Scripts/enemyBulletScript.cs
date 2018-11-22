using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletScript : MonoBehaviour {
	public float timeToDelete = 10.0f;
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
			col.gameObject.GetComponent<MovementTest> ().health = col.gameObject.GetComponent<MovementTest> ().health - 5;
			Destroy (gameObject);
		}
	}
}