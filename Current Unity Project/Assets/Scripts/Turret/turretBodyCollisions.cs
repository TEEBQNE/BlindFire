using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class turretBodyCollisions : MonoBehaviour {

	//GameObject mapBounds = null;

	// Use this for initialization
	void Start () {
		/*if (SceneManager.GetActiveScene ().name == "UIScene") {
			mapBounds = GameObject.Find ("mapBounds");
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		/*if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameObject localPlayer2;
			localPlayer2 = GameObject.Find ("localPlayer2");
			if (transform.parent.gameObject.GetComponent<turretShopSpawn> ().isClicked && transform.parent.gameObject.GetComponent<turretShopSpawn> ().followMouse) {
				//Debug.Log ("FOLLOWING");
				// toggle following and send data


				

			} else if(transform.parent.gameObject.GetComponent<turretShopSpawn> ().isClicked && !transform.parent.gameObject.GetComponent<turretShopSpawn> ().followMouse)  {
				//Debug.Log ("NOT FOLLOWING");
				// toggle following and send other data?
			}
		}*/
	}

	void OnMouseDown()
	{
		
		if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject != null && !GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretShopSpawn>().followMouse) {
			GameObject localPlayer2 = GameObject.Find ("localPlayer2");
			localPlayer2.GetComponent<networkPlayerScript> ().toggleViewField = false;
			localPlayer2.GetComponent<networkPlayerScript> ().turretID = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().turretID;
			localPlayer2.GetComponent<networkPlayerScript> ().gameObjectName = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.gameObject.name;
			localPlayer2.GetComponent<networkPlayerScript> ().setViewField = true;
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (false);
		}
		GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject = transform.parent.gameObject;
		if (!transform.parent.gameObject.GetComponent<turretShopSpawn> ().isClicked) {
			// when turret is first picked up
			GameObject localPlayer2;
			localPlayer2 = GameObject.Find ("localPlayer2");
			localPlayer2.GetComponent<networkPlayerScript> ().turretID = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().turretID;
			localPlayer2.GetComponent<networkPlayerScript> ().gameObjectName = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.name;
			localPlayer2.GetComponent<networkPlayerScript> ().pickedUp = true;
			transform.parent.gameObject.GetComponent<turretShopSpawn> ().isClicked = true;
			transform.parent.gameObject.GetComponent<turretShopSpawn> ().followMouse = true;
			GameManager.gameManager.GetComponent<GameManager> ().turret = transform.parent.gameObject;
			GameManager.gameManager.GetComponent<GameManager> ().following = true;
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (true);
			localPlayer2.GetComponent<networkPlayerScript> ().toggleViewField = true;
			localPlayer2.GetComponent<networkPlayerScript> ().turretID = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().turretID;
			localPlayer2.GetComponent<networkPlayerScript> ().gameObjectName = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.gameObject.name;
			localPlayer2.GetComponent<networkPlayerScript> ().setViewField = true;
		} else {
			if (transform.parent.gameObject.GetComponent<turretShopSpawn> ().followMouse) {
				if (!(EventSystem.current.IsPointerOverGameObject ())) {
					if (GameManager.gameManager.GetComponent<GameManager> ().CheckLocation (transform.parent.gameObject)){
						// when turret is first placed
						GameObject localPlayer2;
						localPlayer2 = GameObject.Find ("localPlayer2");
						localPlayer2.GetComponent<networkPlayerScript> ().pickedUp = true;
						localPlayer2.GetComponent<networkPlayerScript> ().toSetFollowing = false;
						localPlayer2.GetComponent<networkPlayerScript> ().turretResourceName = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().turretName;
						localPlayer2.GetComponent<networkPlayerScript> ().upgrade1Level = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1;
						localPlayer2.GetComponent<networkPlayerScript> ().upgrade2Level = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2;
						localPlayer2.GetComponent<networkPlayerScript> ().turretKills = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().kills;
						localPlayer2.GetComponent<networkPlayerScript> ().currentTargeting = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().target;

						localPlayer2.GetComponent<networkPlayerScript> ().shouldSetFollowing = true;
						GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (false);
						localPlayer2.GetComponent<networkPlayerScript> ().toggleViewField = false;
						localPlayer2.GetComponent<networkPlayerScript> ().turretID = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().turretID;
						localPlayer2.GetComponent<networkPlayerScript> ().gameObjectName = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.gameObject.name;
						localPlayer2.GetComponent<networkPlayerScript> ().setViewField = true;

						GameManager.gameManager.GetComponent<GameManager>().activelyPressedObject = null;

						transform.parent.gameObject.GetComponent<turretShopSpawn> ().followMouse = false;
						transform.parent.gameObject.GetComponent<ShootScript> ().enabled = true;
						GameManager.gameManager.GetComponent<GameManager> ().following = false;
					}
				}
			}

			if (GameManager.gameManager.GetComponent<GameManager> ().following == false && transform.parent.GetComponent<turretShopSpawn> ().followMouse == false) {
				/*if (upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret != null) {
				upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret.transform.Find ("ViewField").gameObject.SetActive (false);
			}*/
				// when turret is clicked on
				GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject = transform.parent.gameObject;
				if (GameManager.gameManager.GetComponent<GameManager> ().following == false) {
					GameObject localPlayer2;
					localPlayer2 = GameObject.Find ("localPlayer2");
					//upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = transform.parent.gameObject;
					//updateShopInfo ();
					//upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = true;
					// all of the stuff in here was localplayer1
					/*localPlayer2.GetComponent<networkPlayerScript> ().clickedTurret = true;
					localPlayer2.GetComponent<networkPlayerScript> ().turretResourceName = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().turretName;
					localPlayer2.GetComponent<networkPlayerScript> ().upgrade1Level = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1;
					localPlayer2.GetComponent<networkPlayerScript> ().upgrade2Level = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2;
					localPlayer2.GetComponent<networkPlayerScript> ().turretKills = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().kills;
					localPlayer2.GetComponent<networkPlayerScript> ().currentTargeting = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().target;
					localPlaye	r2.GetComponent<networkPlayerScript> ().toggleClicked = true;
					*/
					upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = transform.parent.gameObject;
					upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = true;

					GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (true);
					localPlayer2.GetComponent<networkPlayerScript> ().toggleViewField = true;
					localPlayer2.GetComponent<networkPlayerScript> ().turretID = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().turretID;
					localPlayer2.GetComponent<networkPlayerScript> ().gameObjectName = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.gameObject.name;
					localPlayer2.GetComponent<networkPlayerScript> ().setViewField = true;
				
					//shouldUpdateShop = true;
					//transform.parent.transform.Find ("ViewField").gameObject.SetActive (true);
				}
			}
		}
	}

	void OnMouseOver()
	{
		if (transform.parent.GetComponent<turretShopSpawn>().followMouse == false) {
			//gameObject.transform.parent.GetComponent<SpriteRenderer> ().material = outline;
			transform.parent.GetComponent<SpriteRenderer> ().material = transform.parent.GetComponent<turretUpgradeScript>().outline;
			//return;
		}
	}

	void OnMouseExit()
	{
		if (transform.parent.GetComponent<turretShopSpawn>().followMouse == false) {
			//gameObject.transform.parent.GetComponent<SpriteRenderer> ().material = outline;
			transform.parent.GetComponent<SpriteRenderer> ().material = transform.parent.GetComponent<turretUpgradeScript>().basic;
			//return;
		}
	}
}