using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class rocketTarget : MonoBehaviour {

	bool isClicked = false;
	bool toggleIsClicked = false;
	GameObject mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find ("Main Camera");
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").gameObject.activeSelf || !gameObject.GetComponent<followTurret> ().toFollow.gameObject.activeSelf) {
			gameObject.SetActive (false);
		} 

		if (Input.GetMouseButtonUp (0) && toggleIsClicked) {
			isClicked = true;
			toggleIsClicked = false;
		}

		// allows the target to follow a cursor -- does not work on phone (no cursor)
		if (isClicked && !toggleIsClicked) {
			float zPos = gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position.z;
			gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
			gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position = new Vector3 (gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position.x, gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position.y, zPos);

		}

		if (Input.GetMouseButtonDown (0) && !isClicked) {
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 5f;

			Vector2 v = Camera.main.ScreenToWorldPoint (mousePosition);

			List<Collider2D> colList = new List<Collider2D> (); 

			Collider2D[] col = Physics2D.OverlapCircleAll (v, gameObject.GetComponent<CircleCollider2D>().radius / 2.0f);

			for (int x = 0; x < col.Length; x++) {
				if (col [x].name == gameObject.name) {
					toggleIsClicked = true;
					return;
				}
			}

			if (colList.Count > 0) {
				toggleIsClicked = false;
			} 
		} else if (isClicked && Input.GetMouseButtonDown (0) && !IsPointerOverUIObject() /*&& !IsOverSquare()*/) {
			isClicked = false;
			GameObject localPlayer2 = GameObject.Find ("localPlayer2");
			GameObject mapObj = GameObject.Find ("mapBounds");

			/*localPlayer2.GetComponent<networkPlayerScript> ().xPercent = (gameObject.transform.position.x) / mapObj.GetComponent<convertMouseClick> ().xRes;
			localPlayer2.GetComponent<networkPlayerScript> ().yPercent = (gameObject.transform.position.y + mapObj.GetComponent<convertMouseClick>().yMin) / mapObj.GetComponent<convertMouseClick>().yRes;
			*/
			Vector2 mousePos = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
			localPlayer2.GetComponent<networkPlayerScript>().targetXPos = (mousePos.x)/mapObj.GetComponent<convertMouseClick>().xRes;
			localPlayer2.GetComponent<networkPlayerScript> ().targetYPos = (mousePos.y + mapObj.GetComponent<convertMouseClick> ().yMin) / mapObj.GetComponent<convertMouseClick> ().yRes;
			localPlayer2.GetComponent<networkPlayerScript> ().theTurretID = gameObject.GetComponent<followTurret> ().toFollow.GetComponent<ShootScript> ().turretID;
			localPlayer2.GetComponent<networkPlayerScript> ().changeTargetPos = true;
			float zPos = gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position.z;
			gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
			gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position = new Vector3 (gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position.x, gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position.y, zPos);
		}
	}

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	private bool IsOverSquare()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 5f;

		Vector2 v = Camera.main.ScreenToWorldPoint (mousePosition);

		Collider2D[] col = Physics2D.OverlapCircleAll (v, gameObject.GetComponent<followTurret> ().toFollow.transform.Find("visionCollider").GetComponent<CircleCollider2D>().radius / 4.0f);

		for (int x = 0; x < col.Length; x++) {
			if (col [x].gameObject.tag == "square") {
				gameObject.GetComponent<followTurret> ().toFollow.transform.Find ("ViewField").transform.position = gameObject.GetComponent<followTurret> ().toFollow.transform.position;
				GameObject localPlayer2 = GameObject.Find ("localPlayer2");
				GameObject mapObj = GameObject.Find ("mapBounds");
				localPlayer2.GetComponent<networkPlayerScript>().targetXPos = (gameObject.GetComponent<followTurret> ().toFollow.transform.position.x)/mapObj.GetComponent<convertMouseClick>().xRes;
				localPlayer2.GetComponent<networkPlayerScript> ().targetYPos = (gameObject.GetComponent<followTurret> ().toFollow.transform.position.y + mapObj.GetComponent<convertMouseClick> ().yMin) / mapObj.GetComponent<convertMouseClick> ().yRes;
				localPlayer2.GetComponent<networkPlayerScript> ().theTurretID = gameObject.GetComponent<followTurret> ().toFollow.GetComponent<ShootScript> ().turretID;
				localPlayer2.GetComponent<networkPlayerScript> ().changeTargetPos = true;
				isClicked = false;
				toggleIsClicked = false;
				return false;
			}
		}
		return true;
	}
}