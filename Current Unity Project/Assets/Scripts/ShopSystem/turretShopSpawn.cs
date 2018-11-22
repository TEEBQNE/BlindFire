using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class turretShopSpawn : MonoBehaviour {

	public bool followMouse = true;
	public bool isClicked = false;

	public Material basic;
	public Material outline;

	GameObject mapObj = null;


	void Awake()
	{
		followMouse = false;
		isClicked = false;
		GameManager.gameManager.GetComponent<GameManager> ().turret = this.gameObject;
		gameObject.GetComponent<ShootScript> ().enabled = false;
		transform.Find ("ViewField").gameObject.SetActive (false);

		if (SceneManager.GetActiveScene ().name == "UIScene") {
			mapObj = GameObject.Find ("mapBounds");
		}
	}

	void Update()
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			if (gameObject.name == "Glue Turret(Clone)") {
				gameObject.transform.Find ("GlueAnim").gameObject.SetActive (false);
			}
			if (followMouse) {
				GameObject localPlayer2;
				localPlayer2 = GameObject.Find ("localPlayer2");
				transform.Find ("ViewField").gameObject.GetComponent<SpriteRenderer> ().gameObject.SetActive (true);
				transform.Find ("ViewField").gameObject.GetComponent<SpriteRenderer> ().color = new Color (161f / 255f, 160f / 255f, 160f / 255f, 101f / 255f);
				if ((EventSystem.current.IsPointerOverGameObject ()) || !GameManager.gameManager.GetComponent<GameManager> ().CheckLocation (this.gameObject)) {
					transform.Find ("ViewField").gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 0f, 0f, 101f / 255f);
				} else {
					transform.Find ("ViewField").gameObject.GetComponent<SpriteRenderer> ().color = new Color (161f / 255f, 160f / 255f, 160f / 255f, 101f / 255f);
				}
				Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				temp.z = 0f;
				transform.position = temp;
				localPlayer2.GetComponent<networkPlayerScript> ().xPercent = (gameObject.transform.position.x) / mapObj.GetComponent<convertMouseClick> ().xRes;
				localPlayer2.GetComponent<networkPlayerScript> ().yPercent = (gameObject.transform.position.y + mapObj.GetComponent<convertMouseClick>().yMin) / mapObj.GetComponent<convertMouseClick>().yRes;

					//Debug.Log ("CLICK" + "X: " + (mouseClick.x) / xRes + "Y: " + ((mouseClick.y + yMin) / yRes));
			}
		}
	}
}