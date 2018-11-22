using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleSpawnScript : MonoBehaviour {

	public float xPercent = 0f;
	public float yPercent = 0f;
	public float xRes = 0f;
	public float yRes = 0f;

	public GameObject circle;

	public bool shouldSpawn = false;

	// Use this for initialization
	void Start () {
		xRes = 2f * gameObject.GetComponent<SpriteRenderer> ().bounds.extents.x;
		yRes = 2f * gameObject.GetComponent<SpriteRenderer> ().bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (shouldSpawn) {
			shouldSpawn = false;
			Vector2 toSpawn;
			Debug.Log ("SCREEN" + "X: " + xPercent + " Y: " + yPercent);
			toSpawn = new Vector2 ((xRes * xPercent), (yRes * yPercent));

			Instantiate (circle, toSpawn, transform.rotation);
		}*/
	}
}
