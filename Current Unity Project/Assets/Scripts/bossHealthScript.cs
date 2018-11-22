using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealthScript : MonoBehaviour {

	public int health = 200;
	bool notMoving = false;
	public GameObject bossBar;
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		GameObject healthBar;
		healthBar = Instantiate (bossBar, transform.position, transform.rotation);
		healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
		healthBar.GetComponent<bossHealthBar> ().objectToFollow = this.gameObject.transform;
		animator.SetBool ("isMoving", true);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y != 0) {
			float step = 4 * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (0, 0, 0), step);
		} else if (!notMoving) {
			notMoving = true;
			animator.SetBool ("isMoving", false);
		}

		if (health <= 0) {
			GameObject localPlayer1 = GameObject.Find ("localPlayer1");
			localPlayer1.GetComponent<networkPlayerScript> ().winGame = true;
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "playerBullet") {
			health--;
			Destroy (col.gameObject);
		}

		/*if (col.gameObject.tag == "turret") {
			Instantiate (Resources.Load("turretExplosion"));
			Debug.Log ("HHAHAHAE");
			Destroy (col.gameObject);
		}*/
	}
}