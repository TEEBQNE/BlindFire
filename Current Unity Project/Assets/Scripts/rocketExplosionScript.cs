using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketExplosionScript : MonoBehaviour {
	GameObject deathAnimation;
	public int damage = 1;
	public GameObject turretFired;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		GameObject localPlayer1 = GameObject.Find ("localPlayer1");
		if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<MoveEnemy> ().Health -= damage;
			if (col.gameObject.GetComponent<MoveEnemy> ().Health <= 0) {
				if (col.gameObject.name == "droneEnemy(Clone)") {
					SpawnEnemy.instance.droneEnemiesList.Remove(col.gameObject);
				}
				else if (col.gameObject.name == "spiderEnemy(Clone)") {
					SpawnEnemy.instance.spiderEnemiesList.Remove(col.gameObject);
				}
				else if (col.gameObject.name == "tankEnemy(Clone)") {
					SpawnEnemy.instance.tankEnemiesList.Remove(col.gameObject);
				}

				localPlayer1.GetComponent<networkPlayerScript> ().resourcesAdd = col.gameObject.GetComponent<MoveEnemy> ().resourceAdd;
				localPlayer1.GetComponent<networkPlayerScript> ().updateResources = true;

				// set local player network script to add resource to true
				// then pull resource int from specific gameobject
				turretFired.GetComponent<ShootScript> ().kills++;
				deathAnimation = Resources.Load ("Death Anim/" + col.gameObject.GetComponent<MoveEnemy>().deathAnim) as GameObject;
				Instantiate(deathAnimation, col.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 180f)));
				Destroy (col.gameObject.GetComponent<MoveEnemy> ().newHealthBar);
				Destroy (col.gameObject);
			}
		}
	}
}