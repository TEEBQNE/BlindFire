using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionColliderCollisions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Enemy") || col.gameObject.layer == 21)
		{
			transform.parent.GetComponent<ShootScript>().enemiesInRange.Add(col.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Enemy"))
		{
			if (gameObject.transform.parent.name == "Glue Turret(Clone)") {
				col.gameObject.GetComponent<MoveEnemy> ().newSpeed = col.gameObject.GetComponent<MoveEnemy> ().normalSpeed;
			}
			transform.parent.GetComponent<ShootScript>().enemiesInRange.Remove(col.gameObject);
		}
	}
}
