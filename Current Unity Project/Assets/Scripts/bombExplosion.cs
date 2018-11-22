using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombExplosion : MonoBehaviour {

	public int bombDamage = 3;
	GameObject deathAnimation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<MoveEnemy> ().health = col.gameObject.GetComponent<MoveEnemy> ().health - bombDamage;

			if (col.gameObject.GetComponent<MoveEnemy> ().health <= 0) {
				GameObject localPlayer1 = GameObject.Find ("localPlayer1");
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

				deathAnimation = Resources.Load ("Death Anim/" + col.gameObject.GetComponent<MoveEnemy>().deathAnim) as GameObject;
				Instantiate(deathAnimation, col.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 180f)));
				Destroy (col.gameObject.GetComponent<MoveEnemy> ().newHealthBar);
				Destroy (col.gameObject);
			}
		}

		if(col.gameObject.tag == "groundEnemy")
		{
			col.gameObject.GetComponent<MoveOutsideEnemy> ().health = col.gameObject.GetComponent<MoveOutsideEnemy> ().health - bombDamage;
			if (col.gameObject.GetComponent<MoveOutsideEnemy>().health <= 0)
			{
				SpawnEnemy.instance.outsideEnemiesList.Remove(col.gameObject);
				GameObject localPlayer1 = GameObject.Find ("localPlayer1");
				col.gameObject.GetComponent<MoveOutsideEnemy>().newHealthBar.GetComponent<enemyHealthBar> ().objectToFollow = null;
				Destroy (col.gameObject.GetComponent<MoveOutsideEnemy>().newHealthBar);
				Destroy(col.gameObject);
				localPlayer1.GetComponent<networkPlayerScript> ().resourcesAdd = 2;
				localPlayer1.GetComponent<networkPlayerScript> ().updateResources = true;
			}
		}
	}
}
