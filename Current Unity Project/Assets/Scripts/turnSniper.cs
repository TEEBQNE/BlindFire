using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class turnSniper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").gameObject.activeSelf || !gameObject.GetComponent<followTurret> ().toFollow.gameObject.activeSelf) {
			gameObject.SetActive (false);
		} 

		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 5f;

			Vector2 v = Camera.main.ScreenToWorldPoint (mousePosition);

			List<Collider2D> colList = new List<Collider2D> (); 

			//Collider2D[] col = Physics2D.OverlapPointAll(v);
			Collider2D[] col = Physics2D.OverlapCircleAll (v, gameObject.GetComponent<CircleCollider2D>().radius / 2.0f);

			for (int x = 0; x < col.Length; x++) {
				if (col [x].name == gameObject.name) {

					Vector3 difference = transform.position - gameObject.GetComponent<followTurret>().toFollow.transform.transform.Find("turretGun").position;
					float rotationZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;

					GameObject localPlayer2 = GameObject.Find ("localPlayer2");
					localPlayer2.GetComponent<networkPlayerScript>().zAngle = rotationZ;
					localPlayer2.GetComponent<networkPlayerScript> ().turretToChange = gameObject.GetComponent<followTurret>().toFollow.GetComponent<ShootScript> ().turretID;
					localPlayer2.GetComponent<networkPlayerScript>().changeTurretDirection = true;

					gameObject.GetComponent<followTurret>().toFollow.transform.Find("turretGun").transform.rotation = Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f);
					Debug.Log ("CLICKED");
					return;
				}
			}

			if (colList.Count > 0) {
				//activeTurret.transform.Find ("ViewField").GetComponent<SpriteRenderer> ().color = new Color (1f, 0f, 0f, 101f / 255f);
				//Debug.Log ("CANT PLACE");
				Debug.Log ("DIDN'T CLICK");
			} 
		} 
	}
}