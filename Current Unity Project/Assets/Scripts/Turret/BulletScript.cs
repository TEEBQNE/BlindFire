using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	// have this be a public string set in turret that is passed to here when the bullet is fired from the turret
	//string deathAnimName = "fastEnemyDeath";
	GameObject deathAnimation;
	public float bulletSpeed = 0.5f;
	public GameObject targetObject;
	public GameObject target;
	public Vector3 startPos;
	public Vector3 targetPos;
	public float maxDistance = 5f;
	public int damage = 1;
	public GameObject turretFired;
	public Sprite spriteToSet;
	public Transform angleToFire;
	public float angle;

	//private float distance;
	private float startTime;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = spriteToSet;
		startPos = transform.position;
		startTime = Time.time;
		//distance = Vector2.Distance(startPos, targetPos);
		//targetPos.x -= 1;
	}

	// Update is called once per frame
	void Update () {
		if (turretFired.name == "Shotgun Turret(Clone)") {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * (angleToFire.eulerAngles.z+90f)), Mathf.Sin (Mathf.Deg2Rad * (angleToFire.eulerAngles.z+90f)), 0f) * (bulletSpeed);
			if (Mathf.Abs (transform.position.x - startPos.x) >= turretFired.transform.Find("ViewField").GetComponent<SpriteRenderer>().bounds.extents.x || Mathf.Abs (transform.position.y - startPos.y) >= turretFired.transform.Find("ViewField").GetComponent<SpriteRenderer>().bounds.extents.y) {
				Destroy (gameObject);
			}
		}
		else if (!(turretFired.name.CompareTo ("Sniper Turret(Clone)") == 0)) {
			if (target != null) {
				/*Vector3 difference = target.transform.position - transform.position;
			float rotationZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0.0f, 0.0f, rotationZ - 180f), 100f);*/
			}
			if (targetObject != null) {
				//distance = Vector2.Distance (transform.position, targetObject.transform.position);
				float timeInterval = Time.time - startTime;
				gameObject.transform.position = Vector3.Lerp (startPos, targetObject.transform.position, timeInterval * bulletSpeed /*/ distance*/);
				// if it reaches the target and hits nothing
			} else {
				Destroy (this.gameObject);
			}
		} else {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * (angle+90f)), Mathf.Sin (Mathf.Deg2Rad * (angle+90f)), 0f) * (bulletSpeed);
			if (Mathf.Abs (transform.position.x - startPos.x) >= turretFired.transform.Find("ViewField").GetComponent<SpriteRenderer>().bounds.extents.x || Mathf.Abs (transform.position.y - startPos.y) >= turretFired.transform.Find("ViewField").GetComponent<SpriteRenderer>().bounds.extents.y) {
				Destroy (gameObject);
			}
		}

		/*
		 * Going to have to rethink how bullets target
		 * Can't use the position of the object for snipers
		 * Will need to figure out directional max distance that is
		 * in the direction of the object but the end target
		 * is equal to the current vision radius
		 * if (turretFired.gameObject.name == "Sniper Turret(Clone)") {
		 * *****Just use velocity and when it reaches a certain distance destroy it
			if (targetObject != null) {
				float timeInterval = Time.time - startTime;
				gameObject.transform.position = Vector3.Lerp (startPos, targetPos, timeInterval * bulletSpeed);
			}
			if (Mathf.Sqrt (((startPos.x - transform.position.x) * (startPos.x - transform.position.x)) + ((startPos.y - transform.position.y) * (startPos.y - transform.position.y))) > turretFired.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().radius * 2f) {
				Destroy (this.gameObject);
				Debug.Log ("reached range");
			}
		}*/
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.layer == 21) {
			col.gameObject.GetComponent<bossHealthScript> ().health -= damage;
			Destroy (gameObject);
		}
		/*if (col.gameObject.tag == "square") {
			Debug.Log ("HIT WALL");
			Instantiate (Resources.Load ("dust"), transform.position, Quaternion.Euler (0f, 0f, Random.Range (0f, 180f)));
			Destroy (gameObject);
			// spawn a dust object at a random rotation
		}*/

		GameObject localPlayer1 = GameObject.Find ("localPlayer1");
		if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<MoveEnemy> ().Health -= damage;
			if (col.gameObject.GetComponent<MoveEnemy> ().Health <= 0) {
				if (col.gameObject.name == "droneEnemy(Clone)") {
					SpawnEnemy.instance.droneEnemiesList.Remove (col.gameObject);
				} else if (col.gameObject.name == "spiderEnemy(Clone)") {
					SpawnEnemy.instance.spiderEnemiesList.Remove (col.gameObject);
				} else if (col.gameObject.name == "tankEnemy(Clone)") {
					SpawnEnemy.instance.tankEnemiesList.Remove (col.gameObject);
				}

				localPlayer1.GetComponent<networkPlayerScript> ().resourcesAdd = col.gameObject.GetComponent<MoveEnemy> ().resourceAdd;
				localPlayer1.GetComponent<networkPlayerScript> ().updateResources = true;

				// set local player network script to add resource to true
				// then pull resource int from specific gameobject
				turretFired.GetComponent<ShootScript> ().kills++;
				deathAnimation = Resources.Load ("Death Anim/" + col.gameObject.GetComponent<MoveEnemy> ().deathAnim) as GameObject;
				Instantiate (deathAnimation, col.transform.position, Quaternion.Euler (0f, 0f, Random.Range (0f, 180f)));
				Destroy (col.gameObject.GetComponent<MoveEnemy> ().newHealthBar);
				Destroy (col.gameObject);
			}

			if (!(turretFired.name.CompareTo ("Sniper Turret(Clone)") == 0)) {
				Destroy (gameObject);
			}
		} 
	}
}