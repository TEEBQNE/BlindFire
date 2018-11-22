using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class toggleTurretSpawnCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "UIScene") {

			if (!IsPointerOverUIObject()) {
				if (GameManager.gameManager.GetComponent<GameManager> ().following == false) {
					gameObject.GetComponent<CircleCollider2D> ().enabled = false;
				} else if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject != null && GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.gameObject.GetComponent<turretShopSpawn> ().isClicked == true && GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.gameObject.GetComponent<turretShopSpawn> ().followMouse == true) {
					gameObject.GetComponent<CircleCollider2D> ().enabled = true;
				} else {
					gameObject.GetComponent<CircleCollider2D> ().enabled = true;
				}
			}
		}
	}

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}
